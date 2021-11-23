namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    /// <summary>
    /// Команда удаления.
    /// </summary>
    public class DeleteCommand : EditCommandBase
    {
        /// <summary>
        /// Команда удаления.
        /// </summary>
        /// <param name="people"> Контакты. </param>
        /// <param name="groups"> Группы контактов. </param>
        /// <param name="unitOfWork"> Единица работы. </param>
        /// <param name="updateFunction"> Функция обновления UI. </param>
        public DeleteCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork, Func<Task> updateFunction) : base(people, groups, unitOfWork, updateFunction)
        {
        }

        protected override async Task EditGroupAsync(GroupModel selectedGroup)
        {
            var result = await GroupService.DeleteAsync(selectedGroup);
            UpdateAsync(result);
        }

        protected override async Task EditPersonAsync(PersonModel selectedPerson)
        {
            var result = await PeopleService.DeleteAsync(selectedPerson);
            UpdateAsync(result);
        }
    }
}