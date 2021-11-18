namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using GoogleContacts.Domain;

    public class DeleteCommand : ICommand
    {
        public DeleteCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups)
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

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            var selectedContact = People.FirstOrDefault(person => person.IsSelected) ??
                                  Groups.FirstOrDefault(group => group.IsSelected);

            switch (selectedContact)
            {
                case PersonModel selectedPerson:
                {
                    var peopleService = NinjectKernel.Get<IPeopleService>();
                    var result = await peopleService.Delete(selectedPerson);
                    if (string.IsNullOrEmpty(result))
                        People.Remove(selectedPerson);
                    else
                        MessageBox.Show(result);

                    break;
                }
                case GroupModel selectedGroup:
                {
                    var groupService = NinjectKernel.Get<IGroupService>();
                    var result = await groupService.Delete(selectedGroup);
                    if (string.IsNullOrEmpty(result))
                        Groups.Remove(selectedGroup);
                    else
                        MessageBox.Show(result);

                    break;
                }
            }
        }
    }
}