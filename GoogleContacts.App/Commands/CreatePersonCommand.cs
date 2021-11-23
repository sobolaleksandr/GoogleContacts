namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;
    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;

    /// <summary>
    /// Команда создания контакта.
    /// </summary>
    public class CreatePersonCommand : BaseCommand
    {
        /// <summary>
        /// Команда создания контакта.
        /// </summary>
        /// <param name="people"> Контакты. </param>
        /// <param name="groups"> Группы контактов. </param>
        /// <param name="unitOfWork"> Единица работы. </param>
        /// <param name="updateFunction"> Функция обновления UI. </param>
        public CreatePersonCommand(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork, Func<Task> updateFunction) : base(people,
            groups, unitOfWork, updateFunction)
        {
        }

        public override async void Execute(object parameter)
        {
            var vm = new PersonViewModel(Groups);
            var window = new EditPersonView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            var personModel = new PersonModel(vm, string.Empty);
            var result = await PeopleService.CreateAsync(personModel);
            UpdateAsync(result);
        }
    }
}