namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    /// <summary>
    /// Базовый класс команды.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Функция обновления UI.
        /// </summary>
        private readonly Func<Task> _updateFunction;

        /// <summary>
        /// Группы контактов.
        /// </summary>
        protected readonly ObservableCollection<ContactModel> Groups;

        /// <summary>
        /// Сервис для работы с <see cref="GroupModel"/>.
        /// </summary>
        protected readonly IService<GroupModel> GroupService;

        /// <summary>
        /// Контакты.
        /// </summary>
        protected readonly ObservableCollection<ContactModel> People;

        /// <summary>
        /// Сервис для работы с <see cref="PersonModel"/>.
        /// </summary>
        protected readonly IService<PersonModel> PeopleService;

        /// <summary>
        /// Базовый класс команды.
        /// </summary>
        /// <param name="people"> Контакты. </param>
        /// <param name="groups"> Группы контактов. </param>
        /// <param name="unitOfWork"> Единица работы. </param>
        /// <param name="updateFunction"> Функция обновления UI. </param>
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

        /// <summary>
        /// Обновить UI.
        /// </summary>
        /// <param name="result"> Результат операции. </param>
        protected async void UpdateAsync(ContactModel result)
        {
            if (ValidateResult(result))
                await _updateFunction();
        }

        /// <summary>
        /// Обновить UI.
        /// </summary>
        /// <param name="error"> Сообщение об ошибке. </param>
        protected async void UpdateAsync(string error)
        {
            if (ValidateError(error))
                await _updateFunction();
        }

        /// <summary>
        /// Проверка ошибки результата.
        /// </summary>
        /// <param name="error"> Сообщение об ошибке. </param>
        /// <returns> True, если ошибка пуста, иначе вывод сообщения на экран. </returns>
        private static bool ValidateError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return true;

            MessageBox.Show(error);
            return false;
        }

        /// <summary>
        /// Проверка полученных результатов. 
        /// </summary>
        /// <param name="result"> Результат операции. </param>
        /// <returns> True, если проверка пройдена. </returns>
        private static bool ValidateResult(ContactModel result)
        {
            var error = result.Error;
            return ValidateError(error);
        }
    }
}