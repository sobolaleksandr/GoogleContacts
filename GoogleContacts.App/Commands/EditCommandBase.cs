namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    public abstract class EditCommandBase : BaseCommand
    {
        protected EditCommandBase(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork) : base(people, groups, unitOfWork)
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