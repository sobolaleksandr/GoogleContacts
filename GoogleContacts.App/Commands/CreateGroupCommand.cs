namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    public class CreateGroupCommand : BaseCommand
    {
        public CreateGroupCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork) : base(people, groups, unitOfWork)
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

            var groupModel = new GroupModel(vm.Name, string.Empty);
            var result = await GroupService.Create(groupModel);
            if (ValidateResult(result))
                await UpdateGroups();
        }
    }
}