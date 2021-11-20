namespace GoogleContacts.App
{
    using System;
    using System.Threading.Tasks;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    internal static class Program
    {
        private const bool DEBUG = false;

        //TODO:
        //1. Create UnitOfWork instead of DI services
        //2. Make 3 projects : domain(models), UI(vms and views), Repo (services and repository)
        [STAThread]
        private static void Main()
        {
            using var unitOfWork = new UnitOfWork(DEBUG);
            while (true)
            {
                var viewModel = new ApplicationViewModel();
                Task.Run(async () => await viewModel.UploadData(unitOfWork)).GetAwaiter().GetResult();

                var window = new MainWindow
                {
                    DataContext = viewModel
                };

                if (window.ShowDialog() != true)
                    return;
            }
        }
    }
}