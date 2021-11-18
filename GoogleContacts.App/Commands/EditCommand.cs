﻿namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    /// <summary>
    /// Команда изменения модели примитива.
    /// </summary>
    public class EditCommand : EditCommandBase
    {
        public EditCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups) : base(
            people, groups)
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
            var groupService = NinjectKernel.Get<IGroupService>();
            var result = await groupService.Update(selectedGroup);
            UpdateContact(selectedGroup, result);
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
            var peopleService = NinjectKernel.Get<IPeopleService>();
            var result = await peopleService.Update(selectedPerson);
            UpdateContact(selectedPerson, result);

            var groupService = NinjectKernel.Get<IGroupService>();
            var updatedGroup = await groupService.Get(group.ModelResourceName);
            group.ApplyFrom(updatedGroup);
        }

        private static void UpdateContact(ContactModel model, ContactModel result)
        {
            var error = result.Error;
            if (string.IsNullOrEmpty(error))
                model.ApplyFrom(result);
            else
                MessageBox.Show(error);
        }
    }
}