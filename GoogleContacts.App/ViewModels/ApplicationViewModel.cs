namespace GoogleContacts.App.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Commands;
    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    public class ApplicationViewModel
    {
        private readonly ObservableCollection<ContactModel> _groups;
        private readonly ObservableCollection<ContactModel> _people;
        private readonly UnitOfWork _unitOfWork;

        public ApplicationViewModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _people = new ObservableCollection<ContactModel>();
            _groups = new ObservableCollection<ContactModel>();
            Contacts = new ObservableCollection<ContactModel>
            {
                new ContactModel(string.Empty) { Name = "Контакты", Contacts = _people },
                new ContactModel(string.Empty) { Name = "Группы", Contacts = _groups }
            };

            DeleteCommand = new DeleteCommand(_people, _groups, unitOfWork, UploadData);
            EditCommand = new EditCommand(_people, _groups, unitOfWork, UploadData);
            CreatePersonCommand = new CreatePersonCommand(_people, _groups, unitOfWork, UploadData);
            CreateGroupCommand = new CreateGroupCommand(_people, _groups, unitOfWork, UploadData);
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

        public async Task UploadData()
        {
            var peopleService = _unitOfWork.PeopleService;
            var groupService = _unitOfWork.GroupService;

            var people = await peopleService.Get();
            var groups = await groupService.Get();

            _groups.Clear();
            foreach (var group in groups)
            {
                _groups.Add(group);
            }

            _people.Clear();
            foreach (var person in people)
            {
                _people.Add(person);
            }
        }
    }
}