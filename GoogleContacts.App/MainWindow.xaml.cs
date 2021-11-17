namespace GoogleContacts.App
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.Domain;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var people = Enumerable.Range(0, 10)
                .Select(item => (ContactModel)new PersonModel($"Test{item}", $"Test{item}", $"Test{item}", $"Test{item}")).ToList();

            var groups = Enumerable.Range(100, 10)
                .Select(item => (ContactModel)(new GroupModel($"TetsGroup{item}"))).ToList();

            DataContext = new ApplicationViewModel(people, groups);
        }
    }
}