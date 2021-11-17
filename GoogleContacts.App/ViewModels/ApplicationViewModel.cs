namespace GoogleContacts.App.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    using GoogleContacts.App.Commands;
    using GoogleContacts.Domain;

    public class ApplicationViewModel
    {
        public ApplicationViewModel(IEnumerable<ContactModel> people, IEnumerable<ContactModel> groups)
        {
            CommitCommand = new CommitCommand();
            Contacts = new ObservableCollection<ContactModel>
            {
                new ContactModel
                {
                    Name = "Контакты",
                    Contacts = new ObservableCollection<ContactModel>(people)
                },
                new ContactModel
                {
                    Name = "Группы",
                    Contacts = new ObservableCollection<ContactModel>(groups)
                }
            };

            EditCommand = new EditCommand(Contacts);

        }

        public CommitCommand CommitCommand { get; set; }

        public EditCommand EditCommand { get; set; }

        public List<GroupModel> Groups { get; set; }

        public List<PersonModel> People { get; set; }

        public ObservableCollection<ContactModel> Contacts { get; set; }

        public string Name => "Autocad Plugin";

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";
    }
}