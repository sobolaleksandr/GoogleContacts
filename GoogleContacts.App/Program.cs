namespace GoogleContacts.App
{
    using System;
    using System.Threading.Tasks;

    using GoogleContacts.App.Services;
    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;

    internal static class Program
    {
        private const bool DEBUG = false;

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