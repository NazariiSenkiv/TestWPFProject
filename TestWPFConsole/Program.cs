using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestWPFConsole
{
    class Program
    {
        private const string dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(dataUrl, HttpCompletionOption.ResponseHeadersRead);

            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var dataStream = GetDataStream().Result;
            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea -").Replace("Bonaire,", "Bonaire");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(x => DateTime.Parse(x, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var countryName = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (countryName, province, counts);
            }
        }

        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var httpResponse = client.GetAsync(dataUrl).Result;
            //var csvStr = httpResponse.Content.ReadAsStringAsync().Result;

            //foreach(var dataLine in GetDataLines())
            //{
            //    Console.WriteLine(dataLine);
            //}

            //var dates = GetDates();
            //Console.WriteLine(string.Join("\n", dates));

            var ukrainData = GetData().First(x => x.Country.Equals("Ukraine", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\n", GetDates().Zip(ukrainData.Counts, (date, count) => $"{date} - {count}")));

            Console.ReadLine();
        }
    }
}
