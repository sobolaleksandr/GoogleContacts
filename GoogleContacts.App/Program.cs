namespace GoogleContacts.App
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    internal static class Program
    {
        // TODO: Implement disposable pattern for GroupService and People Service
        [STAThread]
        private static void Main()
        {
            NinjectKernel.RebindSingleton<IPeopleService, PeopleServiceMock>();
            //NinjectKernel.RebindSingleton<IGroupService, GroupServiceMock>();
            NinjectKernel.RebindSingleton<IGroupService, GroupService>();
            var peopleService = NinjectKernel.Get<IPeopleService>();
            var groupService = NinjectKernel.Get<IGroupService>();

            while (true)
            {
                var people = Task.Run(async () => await peopleService.GetContacts()).GetAwaiter().GetResult();
                var groups = Task.Run(async () => await groupService.GetGroups()).GetAwaiter().GetResult();

                if (groups.All(group => string.IsNullOrEmpty(group.Error)))
                {
                    var window = new MainWindow
                    {
                        DataContext = new ApplicationViewModel(new ObservableCollection<ContactModel>(people),
                            new ObservableCollection<ContactModel>(groups))
                    };

                    if (window.ShowDialog() == true)
                        continue;

                    groupService.Stop();
                    return;
                }

                foreach (var group in groups.Where(group => !string.IsNullOrEmpty(group.Error)))
                {
                    MessageBox.Show(group.Error);
                }
            }
        }
    }
}