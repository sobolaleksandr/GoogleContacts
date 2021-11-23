namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    public abstract class EditCommandBase : BaseCommand
    {
        protected EditCommandBase(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork, Func<Task> updateFunction) : base(
            people, groups, unitOfWork, updateFunction)
        {
        }

        /// <summary>
        /// Изменение модели примитива.
        /// </summary>
        /// <param name="parameter"> Вызывающий примитив. </param>
        public override async void Execute(object parameter)
        {
            var selectedContact = People.FirstOrDefault(person => person.IsSelected) ??
                                  Groups.FirstOrDefault(group => group.IsSelected);

            switch (selectedContact)
            {
                case PersonModel selectedPerson:
                    {
                        await EditPerson(selectedPerson);
                        return;
                    }
                case GroupModel selectedGroup:
                    {
                        await EditGroup(selectedGroup);
                        return;
                    }
            }
        }

        protected abstract Task EditGroup(GroupModel selectedGroup);

        protected abstract Task EditPerson(PersonModel selectedPerson);
    }
}