﻿namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    public class CreatePersonCommand : BaseCommand
    {
        public CreatePersonCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork) : base(people, groups, unitOfWork)
        {
        }

        public override async void Execute(object parameter)
        {
            var vm = new PersonViewModel(Groups);
            var window = new EditPersonView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            var group = Groups.FirstOrDefault(item => item.IsSelected);
            var personModel = new PersonModel(vm.GivenName, vm.FamilyName, vm.Email,
                vm.PhoneNumber, group, string.Empty);

            var result = await PeopleService.Create(personModel);
            if (!ValidateResult(result))
                return;

            People.Add(result);
            await UpdateGroups();
        }
    }
}