namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    public class CreatePersonCommand : CreateCommand
    {
        public CreatePersonCommand(ObservableCollection<ContactModel> contacts) : base(contacts)
        {
        }

        public override async void Execute(object parameter)
        {
            var vm = new PersonViewModel(new ObservableCollection<ContactModel>());
            var window = new EditPersonView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            var peopleService = NinjectKernel.Get<IPeopleService>();
            var personModel = new PersonModel(vm.GivenName, vm.FamilyName, vm.Email,
                vm.PhoneNumber, string.Empty);

            var result = await peopleService.Create(personModel);
            UpdateContacts(result);
        }
    }
}