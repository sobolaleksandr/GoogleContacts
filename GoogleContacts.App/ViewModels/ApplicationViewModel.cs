namespace GoogleContacts.App.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Commands;
    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    public class ApplicationViewModel
    {
        public ObservableCollection<ContactModel> Contacts { get; } = new ObservableCollection<ContactModel>();
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

            Contacts.Clear();
            Contacts.Add(new ContactModel(string.Empty)
            {
                Name = "Контакты",
                Contacts = people
            });
            Contacts.Add(new ContactModel(string.Empty)
            {
                Name = "Группы",
                Contacts = groups
            });

            DeleteCommand = new DeleteCommand(people, groups, unitOfWork, () => UploadData(unitOfWork));
            EditCommand = new EditCommand(people, groups, unitOfWork, () => UploadData(unitOfWork));
            CreatePersonCommand = new CreatePersonCommand(people, groups, unitOfWork, () => UploadData(unitOfWork));
            CreateGroupCommand = new CreateGroupCommand(people, groups, unitOfWork, () => UploadData(unitOfWork));
        }
    }
}