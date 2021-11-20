namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    public class DeleteCommand : EditCommandBase
    {
        public DeleteCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork) : base(people, groups, unitOfWork)
        {
        }

        protected override async Task EditGroup(GroupModel selectedGroup)
        {
            var result = await GroupService.Delete(selectedGroup);
            if (ValidateError(result))
                Groups.Remove(selectedGroup);
        }

        protected override async Task EditPerson(PersonModel selectedPerson)
        {
            var result = await PeopleService.Delete(selectedPerson);
            if (!ValidateError(result))
                return;

            People.Remove(selectedPerson);
            await UpdateGroups();
        }
    }
}