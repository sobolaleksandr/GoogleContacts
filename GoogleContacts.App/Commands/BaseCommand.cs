namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    public abstract class BaseCommand : ICommand
    {
        private readonly Func<Task> _updateFunction;
        protected readonly ObservableCollection<ContactModel> Groups;
        protected readonly IService<GroupModel> GroupService;
        protected readonly ObservableCollection<ContactModel> People;
        protected readonly IService<PersonModel> PeopleService;

        protected BaseCommand(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork, Func<Task> updateFunction)
        {
            GroupService = unitOfWork.GroupService;
            PeopleService = unitOfWork.PeopleService;
            People = people;
            Groups = groups;
            _updateFunction = updateFunction;
        }

        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        public abstract void Execute(object parameter);

        protected async void Update(ContactModel result)
        {
            if (ValidateResult(result))
                await _updateFunction();
        }

        protected async void Update(string error)
        {
            if (ValidateError(error))
                await _updateFunction();
        }

        private static bool ValidateError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return true;

            MessageBox.Show(error);
            return false;
        }

        private static bool ValidateResult(ContactModel result)
        {
            var error = result.Error;
            return ValidateError(error);
        }
    }
}