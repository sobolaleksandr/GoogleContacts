namespace GoogleContacts.App.Commands
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    public class DeleteCommand : EditCommandBase
    {
        public DeleteCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups) :
            base(people, groups)
        {
        }

        protected override async Task EditGroup(GroupModel selectedGroup)
        {
            var groupService = NinjectKernel.Get<IGroupService>();
            var result = await groupService.Delete(selectedGroup);
            if (ValidateError(result))
                Groups.Remove(selectedGroup);
        }

        protected override async Task EditPerson(PersonModel selectedPerson)
        {
            var peopleService = NinjectKernel.Get<IPeopleService>();
            var result = await peopleService.Delete(selectedPerson);

            if (!ValidateError(result))
                return;

            People.Remove(selectedPerson);
            await UpdateGroups();
        }
    }
}