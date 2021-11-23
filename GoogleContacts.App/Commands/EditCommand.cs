namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;
    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;

    /// <summary>
    /// Команда изменения модели примитива.
    /// </summary>
    public class EditCommand : EditCommandBase
    {
        public EditCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork, Func<Task> updateFunction) : base(people, groups, unitOfWork, updateFunction)
        {
        }

        protected override async Task EditGroup(GroupModel selectedGroup)
        {
            var vm = new GroupViewModel(selectedGroup);
            var window = new EditGroupView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            selectedGroup.ApplyFrom(vm.Name);
            var result = await GroupService.Update(selectedGroup);
            Update(result);
        }

        protected override async Task EditPerson(PersonModel selectedPerson)
        {
            var vm = new PersonViewModel(selectedPerson, Groups);

            var window = new EditPersonView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            selectedPerson.ApplyFrom(vm);
            var result = await PeopleService.Update(selectedPerson);
            Update(result);
        }
    }
}