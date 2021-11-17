namespace GoogleContacts.Domain
{
    using System.Collections.ObjectModel;

    public class ContactModel : ViewModelBase
    {
        private string _name;

        public ContactModel(string error)
        {
            Error = error;
            Contacts = new ObservableCollection<ContactModel>();
        }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public bool IsSelected { get; set; }

        public string Error { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}