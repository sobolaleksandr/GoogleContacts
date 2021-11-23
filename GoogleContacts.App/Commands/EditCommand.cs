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
    /// Команда изменения.
    /// </summary>
    public class EditCommand : EditCommandBase
    {
        /// <summary>
        /// Команда изменения.
        /// </summary>
        /// <param name="people"> Контакты. </param>
        /// <param name="groups"> Группы контактов. </param>
        /// <param name="unitOfWork"> Единица работы. </param>
        /// <param name="updateFunction"> Функция обновления UI. </param>
        public EditCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork, Func<Task> updateFunction) : base(people, groups, unitOfWork, updateFunction)
        {
        }

        protected override async Task EditGroupAsync(GroupModel selectedGroup)
        {
            var vm = new GroupViewModel(selectedGroup.FormattedName);
            var window = new EditGroupView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            selectedGroup.ApplyFrom(vm.Name);
            var result = await GroupService.UpdateAsync(selectedGroup);
            UpdateAsync(result);
        }

        protected override async Task EditPersonAsync(PersonModel selectedPerson)
        {
            var vm = new PersonViewModel(selectedPerson, Groups);

            var window = new EditPersonView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            selectedPerson.ApplyFrom(vm);
            var result = await PeopleService.UpdateAsync(selectedPerson);
            UpdateAsync(result);
        }
    }
}