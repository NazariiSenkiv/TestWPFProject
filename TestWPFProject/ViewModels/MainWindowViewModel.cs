using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TestWPFProject.Infrastructure.Commands;
using TestWPFProject.Models;
using TestWPFProject.ViewModels.Base;

namespace TestWPFProject.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region TestDataPoints

        private IEnumerable<DataPoint> _TestDataPoints;

        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        } 

        #endregion


        #region Window Title
        private string _Title = "No WPF?";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status

        private string _Status = "Ready";
        /// <summary>Program status</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        #region Commands

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        } 

        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion

            const double step = 0.1;
            var dataPoints = new List<DataPoint>((int)(360 / step));
            for (var x = 0d; x < 360; x += step)
            {
                const double radsInDeg = Math.PI / 180;
                var y = Math.Sin(2 * Math.PI * x * radsInDeg);

                dataPoints.Add(new DataPoint { X = x, Y = y });
            }

            TestDataPoints = dataPoints;
        }
    }
}
