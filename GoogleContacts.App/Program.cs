namespace GoogleContacts.App
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    internal static class Program
    {
        private static readonly bool Debug = false;

        [STAThread]
        private static void Main()
        {
            if (Debug)
            {
                NinjectKernel.RebindSingleton<IPeopleService, PeopleServiceMock>();
                NinjectKernel.RebindSingleton<IGroupService, GroupServiceMock>();
            }
            else
            {
                NinjectKernel.RebindSingleton<IPeopleService, PeopleService>();
                NinjectKernel.RebindSingleton<IGroupService, GroupService>();
            }

            var peopleService = NinjectKernel.Get<IPeopleService>();
            var groupService = NinjectKernel.Get<IGroupService>();

            while (true)
            {
                var people = Task.Run(async () => await peopleService.Get()).GetAwaiter().GetResult();
                var groups = Task.Run(async () => await groupService.GetAll()).GetAwaiter().GetResult();

                var window = new MainWindow
                {
                    DataContext = new ApplicationViewModel(new ObservableCollection<ContactModel>(people),
                        new ObservableCollection<ContactModel>(groups))
                };

                if (window.ShowDialog() == true)
                    continue;

                if (groupService is IDisposable disposableGroup && peopleService is IDisposable disposablePeople)
                {
                    disposablePeople.Dispose();
                    disposableGroup.Dispose();
                    return;
                }

                MessageBox.Show(groupService.GetType() + peopleService.GetType().ToString());
            }
        }
    }
}