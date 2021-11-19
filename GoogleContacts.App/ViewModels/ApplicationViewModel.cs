namespace GoogleContacts.App.ViewModels
{
    using System.Collections.ObjectModel;

    using GoogleContacts.App.Commands;
    using GoogleContacts.Domain;

    public class ApplicationViewModel
    {
        public ApplicationViewModel(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups)
        {
            Contacts = new ObservableCollection<ContactModel>
            {
                new ContactModel(string.Empty)
                {
                    Name = "Контакты",
                    Contacts = people
                },
                new ContactModel(string.Empty)
                {
                    Name = "Группы",
                    Contacts = groups
                }
            };

            DeleteCommand = new DeleteCommand(people, groups);
            EditCommand = new EditCommand(people, groups);
            CreatePersonCommand = new CreatePersonCommand(people, groups);
            CreateGroupCommand = new CreateGroupCommand(people, groups);
        }

        public ObservableCollection<ContactModel> Contacts { get; }
        public CreateGroupCommand CreateGroupCommand { get; }
        public CreatePersonCommand CreatePersonCommand { get; }
        public DeleteCommand DeleteCommand { get; }
        public EditCommand EditCommand { get; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";
    }
}