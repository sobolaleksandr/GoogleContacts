namespace GoogleContacts.Domain
{
    using System.Collections.ObjectModel;

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

        public virtual void ApplyFrom([NotNull] ContactModel model)
        {

        }
    }
}