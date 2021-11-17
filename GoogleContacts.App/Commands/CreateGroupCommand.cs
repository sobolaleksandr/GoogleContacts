namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Windows;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    public class CreateGroupCommand : CreateCommand
    {
        public CreateGroupCommand(ObservableCollection<ContactModel> contacts) : base(contacts)
        {
        }

        public override async void Execute(object parameter)
        {
            var vm = new GroupViewModel();
            var window = new EditGroupView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            var groupService = NinjectKernel.Get<IGroupService>();
            var createdContact = new GroupModel(vm.Name, string.Empty);
            var result = await groupService.CreateGroup(createdContact);
            var error = result.Error;
            if (string.IsNullOrEmpty(error))
                Contacts.Add(result);
            else
                MessageBox.Show(error);
        }
    }
}