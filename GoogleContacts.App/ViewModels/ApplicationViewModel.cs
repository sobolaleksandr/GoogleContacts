namespace GoogleContacts.App.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Commands;
    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    /// <summary>
    /// Вью-модель приложения. 
    /// </summary>
    public class ApplicationViewModel
    {
        /// <summary>
        /// Группы.
        /// </summary>
        private readonly ObservableCollection<ContactModel> _groups;

        /// <summary>
        /// Контакты.
        /// </summary>
        private readonly ObservableCollection<ContactModel> _people;

        /// <summary>
        /// Единица работы. 
        /// </summary>
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Вью-модель приложения.
        /// </summary>
        /// <param name="unitOfWork"> Единица работы. </param>
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

            DeleteCommand = new DeleteCommand(_people, _groups, unitOfWork, UpdateAsync);
            EditCommand = new EditCommand(_people, _groups, unitOfWork, UpdateAsync);
            CreatePersonCommand = new CreatePersonCommand(_people, _groups, unitOfWork, UpdateAsync);
            CreateGroupCommand = new CreateGroupCommand(_people, _groups, unitOfWork, UpdateAsync);
        }

        /// <summary>
        /// Контакты. 
        /// </summary>
        public ObservableCollection<ContactModel> Contacts { get; }

        /// <summary>
        /// Команда создания группы.
        /// </summary>
        public CreateGroupCommand CreateGroupCommand { get; }

        /// <summary>
        /// Команда создания контакта.
        /// </summary>
        public CreatePersonCommand CreatePersonCommand { get; }

        /// <summary>
        /// Команда удаления.
        /// </summary>
        public DeleteCommand DeleteCommand { get; }

        /// <summary>
        /// Команда изменения. 
        /// </summary>
        public EditCommand EditCommand { get; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";

        /// <summary>
        /// Обновить UI.
        /// </summary>
        public async Task UpdateAsync()
        {
            var peopleService = _unitOfWork.PeopleService;
            var groupService = _unitOfWork.GroupService;

            var people = await peopleService.GetAsync();
            var groups = await groupService.GetAsync();

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