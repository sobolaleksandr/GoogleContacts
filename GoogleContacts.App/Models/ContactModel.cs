namespace GoogleContacts.App.Models
{
    using System.Collections.ObjectModel;

    using GoogleContacts.App.ViewModels;

    public class ContactModel : ViewModelBase
    {
        private string _name;
        protected string ModelEtag;

        public ContactModel(string error)
        {
            Error = error;
            Contacts = new ObservableCollection<ContactModel>();
        }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public string Error { get; }

        public bool IsSelected { get; set; }

        public string ModelResourceName { get; protected set; }

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