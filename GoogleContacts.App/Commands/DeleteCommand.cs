namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;

    public class DeleteCommand : EditCommandBase
    {
        public DeleteCommand(ObservableCollection<ContactModel> people, ObservableCollection<ContactModel> groups,
            UnitOfWork unitOfWork, Func<Task> updateFunction) : base(people, groups, unitOfWork, updateFunction)
        {
        }

        protected override async Task EditGroup(GroupModel selectedGroup)
        {
            var result = await GroupService.Delete(selectedGroup);
            Update(result);
        }

        protected override async Task EditPerson(PersonModel selectedPerson)
        {
            var result = await PeopleService.Delete(selectedPerson);
            Update(result);
        }
    }
}