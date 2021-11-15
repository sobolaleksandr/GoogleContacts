namespace GoogleContacts.App
{
    using System.Collections.Generic;
    using System.Windows;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.Domain;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var people = new List<ContactModel>
            {
                new PersonModel("Test1","Test1","Test1","Test1"),
                new PersonModel("Test2","Test2","Test2","Test2")
            };

            var groups = new List<ContactModel>
            {
                new GroupModel("TestGroup1"),
                new GroupModel("TestGroup2"),
            };

            DataContext = new ApplicationViewModel(people,groups);
        }

        private void SetSelectedNodeVM(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TODO: Try prisma framework
        }
    }
}