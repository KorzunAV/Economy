using System;
using System.Windows;
using System.Windows.Threading;
using AutoMapper;
using CQRS.Logic;
using Economy.AutomapperMappings;
using Economy.DataAccess.BlToolkit.AutomapperMappings;
using Economy.DataAccess.BlToolkit.IoC;
using Economy.Dtos;
using Economy.IoC;
using Economy.Logic.AutomapperMappings;
using Economy.Logic.Commands.SaveCommands;
using Economy.Logic.IoC;
using Economy.Logic.Queries;
using Economy.Models;
using Economy.ViewModels;
using Ninject;

namespace Economy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static readonly IKernel Kernel = new StandardKernel(new DataAccessModule(), new LogicModule(), new ConvertersModule());
        public static UserPrincipal User;

        private static ICommandQueryDispatcher _commandQueryDispatcher;
        public static ICommandQueryDispatcher CommandQueryDispatcher
        {
            get { return _commandQueryDispatcher ?? (_commandQueryDispatcher = Kernel.Get<ICommandQueryDispatcher>()); }
        }

        public App()
        {
            Mapper.Initialize(cfg =>
            {
                ViewModelMappings.Initialize(cfg);
                DtoMappings.Initialize(cfg);
                EntityDtoMappings.Initialize(cfg);
            });

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage("Ru-ru")));

            var user = CommandQueryDispatcher.ExecuteQuery<SystemUserDto>(new GetSystemUserByLoginQuery("KorzunAV@gmail.com"));
            User = UserPrincipal.CurrentUser = UserPrincipal.CreatePrincipal(user.Data);

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
