namespace GoogleContacts.App
{
    using System.Collections.ObjectModel;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.Domain;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var people = new ObservableCollection<ContactModel>
            {
                new PersonModel("Test1", "Test1", "Test1", "Test1"),
                new PersonModel("Test2", "Test2", "Test2", "Test2")
            };

            var groups = new ObservableCollection<ContactModel>
            {
                new GroupModel("TestGroup1"),
                new GroupModel("TestGroup2"),
            };

            DataContext = new ApplicationViewModel(people, groups);
        }
    }
}