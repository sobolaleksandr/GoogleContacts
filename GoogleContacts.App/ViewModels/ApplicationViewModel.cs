namespace GoogleContacts.App.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Commands;
    using GoogleContacts.Domain;

    public class ApplicationViewModel
    {
        public ObservableCollection<ContactModel> Contacts { get; private set; }
        public CreateGroupCommand CreateGroupCommand { get; private set; }
        public CreatePersonCommand CreatePersonCommand { get; private set; }
        public DeleteCommand DeleteCommand { get; private set; }
        public EditCommand EditCommand { get; private set; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";

        public async Task UploadData(UnitOfWork unitOfWork)
        {
            var peopleService = unitOfWork.PeopleService;
            var groupService = unitOfWork.GroupService;

            var people = new ObservableCollection<ContactModel>(await peopleService.Get());
            var groups = new ObservableCollection<ContactModel>(await groupService.Get());

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

            DeleteCommand = new DeleteCommand(people, groups, unitOfWork);
            EditCommand = new EditCommand(people, groups, unitOfWork);
            CreatePersonCommand = new CreatePersonCommand(people, groups, unitOfWork);
            CreateGroupCommand = new CreateGroupCommand(people, groups, unitOfWork);
        }
    }
}