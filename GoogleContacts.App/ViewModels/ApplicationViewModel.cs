namespace GoogleContacts.App.ViewModels
{
    using System.Collections.Generic;
    using System.Windows;

    using GoogleContacts.App.Commands;
    using GoogleContacts.Domain;

    public class ApplicationViewModel
    {
        public ApplicationViewModel(List<ContactModel> people, List<ContactModel> groups)
        {
            CommitCommand = new CommitCommand();
            Entities = new List<Entity>()
            {
                new Entity("Контакты",people),
                new Entity("Группы",people),
            };
            EditCommand = new EditCommand(new List<PersonModel>());
        }

        public List<Entity> Entities { get; set; }

        public CommitCommand CommitCommand { get; set; }

        public EditCommand EditCommand { get; set; }

        public List<GroupModel> Groups { get; set; }

        public List<PersonModel> People { get; set; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Autocad Plugin";
    }

    public class Entity
    {
        public Entity(string header, List<ContactModel> contacts)
        {
            Header = header;
            Contacts = contacts;
        }

        public string Header { get; set; }

        public List<ContactModel> Contacts { get; set; }
    }
}