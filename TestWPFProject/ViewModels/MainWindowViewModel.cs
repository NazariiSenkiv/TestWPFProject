using System;
using System.Collections.Generic;
using System.Text;
using TestWPFProject.ViewModels.Base;

namespace TestWPFProject.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Window Title
        private string _Title = "No WPF?";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        } 
        #endregion
    }
}
