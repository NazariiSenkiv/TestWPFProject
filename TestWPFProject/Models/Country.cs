using System.Collections.Generic;

namespace TestWPFProject.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProvinceInfo> Provinces { get; set; }
    }
}
