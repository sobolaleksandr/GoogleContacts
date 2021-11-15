namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;
    using GoogleContacts.Domain;

    /// <summary>
    /// Команда изменения модели примитива.
    /// </summary>
    public class EditCommand : ICommand
    {
        public EditCommand(ObservableCollection<ContactModel> contacts)
        {
            Contacts = contacts;
        }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        /// <summary>
        /// Изменение модели примитива.
        /// </summary>
        /// <param name="parameter"> Вызывающий примитив. </param>
        public void Execute(object parameter)
        {
            var selectedContact = Contacts.SelectMany(contact => contact.Contacts)
                .FirstOrDefault(person => person.IsSelected);
            if (!(selectedContact is PersonModel selectedPerson))
                return;

            var vm = new PersonViewModel(selectedPerson);
            var window = new EditPersonView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;
        }
    }
}