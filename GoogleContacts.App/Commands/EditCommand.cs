﻿namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    /// <summary>
    /// Команда изменения модели примитива.
    /// </summary>
    public class EditCommand : EditCommandBase
    {
        public EditCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork) : base(people, groups, unitOfWork)
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
            if (ValidateResult(result))
                await UpdateGroups();
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

            var group = Groups.FirstOrDefault(item => item.IsSelected);
            selectedPerson.ApplyFrom(vm.GivenName, vm.FamilyName, vm.Email, vm.PhoneNumber, group);
            var result = await PeopleService.Update(selectedPerson);
            if (!ValidateResult(result))
                return;

            selectedPerson.ApplyFrom(result);
            await UpdateGroups();
        }
    }
}