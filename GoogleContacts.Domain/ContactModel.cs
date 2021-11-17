namespace GoogleContacts.Domain
{
    using System.Collections.ObjectModel;

    public class ContactModel
    {
        public ContactModel()
        {
            Contacts = new ObservableCollection<ContactModel>();
        }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public bool IsSelected { get; set; }

        public string Name { get; set; }
    }
}