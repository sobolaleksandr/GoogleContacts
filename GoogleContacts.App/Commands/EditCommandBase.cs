namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using GoogleContacts.Domain;

    public abstract class EditCommandBase : ICommand
    {
        protected readonly ObservableCollection<ContactModel> Groups;

        protected readonly ObservableCollection<ContactModel> People;

        protected EditCommandBase(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups)
        {
            People = people;
            Groups = groups;
        }

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
                    await EditPerson(selectedPerson);
                    return;
                }
                case GroupModel selectedGroup:
                {
                    await EditGroup(selectedGroup);
                    return;
                }
            }
        }

        protected abstract Task EditGroup(GroupModel selectedGroup);

        protected abstract Task EditPerson(PersonModel selectedPerson);
    }
}