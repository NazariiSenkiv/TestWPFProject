using System.Windows;
using System.Windows.Input;
using TestWPFProject.Infrastructure.Commands;
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
        }
    }
}
