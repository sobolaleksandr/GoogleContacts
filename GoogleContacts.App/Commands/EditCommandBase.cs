namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    /// <summary>
    /// Базовая команда изменения.
    /// </summary>
    public abstract class EditCommandBase : BaseCommand
    {
        /// <summary>
        /// Базовая команда изменения.
        /// </summary>
        /// <param name="people"> Контакты. </param>
        /// <param name="groups"> Группы контактов. </param>
        /// <param name="unitOfWork"> Единица работы. </param>
        /// <param name="updateFunction"> Функция обновления UI. </param>
        protected EditCommandBase(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork, Func<Task> updateFunction) : base(
            people, groups, unitOfWork, updateFunction)
        {
        }

        public override async void Execute(object parameter)
        {
            var selectedContact = People.FirstOrDefault(person => person.IsSelected) ??
                                  Groups.FirstOrDefault(group => group.IsSelected);

            switch (selectedContact)
            {
                case PersonModel selectedPerson:
                {
                    await EditPersonAsync(selectedPerson);
                    return;
                }
                case GroupModel selectedGroup:
                {
                    await EditGroupAsync(selectedGroup);
                    return;
                }
            }
        }

        /// <summary>
        /// Изменить группу.
        /// </summary>
        /// <param name="selectedGroup"> Выбранная группа. </param>
        protected abstract Task EditGroupAsync(GroupModel selectedGroup);

        /// <summary>
        /// Изменить контакт.
        /// </summary>
        /// <param name="selectedPerson"> Выбранный контакт. </param>
        protected abstract Task EditPersonAsync(PersonModel selectedPerson);
    }
}