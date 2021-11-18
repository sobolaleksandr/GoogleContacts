namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;

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
            var groupModel = new GroupModel(vm.Name, string.Empty);
            var result = await groupService.Create(groupModel);
            UpdateContacts(result);
        }
    }
}