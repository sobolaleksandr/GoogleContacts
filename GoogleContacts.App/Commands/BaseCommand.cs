namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using GoogleContacts.Domain;

    public abstract class BaseCommand : ICommand
    {
        protected readonly ObservableCollection<ContactModel> Groups;
        protected readonly IService<GroupModel> GroupService;
        protected readonly ObservableCollection<ContactModel> People;
        protected readonly IService<PersonModel> PeopleService;

        protected BaseCommand(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork)
        {
            GroupService = unitOfWork.GroupService;
            PeopleService = unitOfWork.PeopleService;
            People = people;
            Groups = groups;
        }

        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        public abstract void Execute(object parameter);

        protected async Task UpdateGroups()
        {
            var groups = await GroupService.Get();

            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(group);
            }
        }

        protected static bool ValidateError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return true;

            MessageBox.Show(error);
            return false;
        }

        protected static bool ValidateResult(ContactModel result)
        {
            var error = result.Error;
            return ValidateError(error);
        }
    }
}