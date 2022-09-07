using System.Windows;
using TestWPFProject.Infrastructure.Commands.Base;

namespace TestWPFProject.Infrastructure.Commands
{
    internal class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
