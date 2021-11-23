namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;
    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;

    public class CreateGroupCommand : BaseCommand
    {
        public CreateGroupCommand(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork, Func<Task> updateFunction) : base(
            people, groups, unitOfWork, updateFunction)
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
            Update(result);
        }
    }
}