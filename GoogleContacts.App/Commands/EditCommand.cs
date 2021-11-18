namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    /// <summary>
    /// Команда изменения модели примитива.
    /// </summary>
    public class EditCommand : ICommand
    {
        public EditCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups)
        {
            People = people;
            Groups = groups;
        }

        public ObservableCollection<ContactModel> Groups { get; set; }

        public ObservableCollection<ContactModel> People { get; set; }

        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        /// <summary>
        /// Изменение модели примитива.
        /// </summary>
        /// <param name="parameter"> Вызывающий примитив. </param>
        public async void Execute(object parameter)
        {
            var selectedContact = People.FirstOrDefault(person => person.IsSelected) ??
                                  Groups.FirstOrDefault(group => group.IsSelected);

            switch (selectedContact)
            {
                case PersonModel selectedPerson:
                {
                    var vm = new PersonViewModel(selectedPerson);
                    var window = new EditPersonView
                    {
                        DataContext = vm
                    };

                    if (window.ShowDialog() != true)
                        return;

                    selectedPerson.ApplyFrom(vm.GivenName, vm.FamilyName, vm.Email, vm.PhoneNumber);
                    var peopleService = NinjectKernel.Get<IPeopleService>();
                    var updatedContact = await peopleService.Update(selectedPerson);
                    selectedPerson.ApplyFrom((PersonModel)updatedContact);

                    break;
                }
                case GroupModel selectedGroup:
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
                    var error = result.Error;
                    if (string.IsNullOrEmpty(error))
                        selectedGroup.ApplyFrom((GroupModel)result);
                    else
                        MessageBox.Show(error);

                    break;
                }
            }
        }
    }
}