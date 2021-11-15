namespace GoogleContacts.Domain
{
    public abstract class ContactModel
    {
        public bool IsSelected { get; set; }

        public string Name { get; set; }

        public static string WindowTitle => "GoogleContacts";
    }
}