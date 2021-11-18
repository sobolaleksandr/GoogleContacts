namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    using GoogleContacts.Domain;

    public abstract class CreateCommand : ICommand
    {
        public CreateCommand(ObservableCollection<ContactModel> contacts)
        {
            Contacts = contacts;
        }

        public ObservableCollection<ContactModel> Contacts { get; }

        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);

        protected void UpdateContacts(ContactModel result)
        {
            var error = result.Error;
            if (string.IsNullOrEmpty(error))
                Contacts.Add(result);
            else
                MessageBox.Show(error);
        }
    }
}