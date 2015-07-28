using System;
using System.Windows;
using System.Windows.Threading;
using Economy.ViewModels;

namespace Economy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is ArithmeticException)
            {
                ViewModelLocator.Information.MessageText = e.Exception.Message;
                ViewModelLocator.Information.ShowErrorCommand.Execute(null);

                e.Handled = true;
            }
        }
    }
}
