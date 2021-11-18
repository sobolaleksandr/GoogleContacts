namespace GoogleContacts.App.Commands
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;

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
            DeleteContact(selectedGroup, result, Groups);
        }

        protected override async Task EditPerson(PersonModel selectedPerson)
        {
            var peopleService = NinjectKernel.Get<IPeopleService>();
            var result = await peopleService.Delete(selectedPerson);
            DeleteContact(selectedPerson, result, People);
        }

        private static void DeleteContact(ContactModel model, string result, ICollection<ContactModel> contacts)
        {
            if (string.IsNullOrEmpty(result))
                contacts.Remove(model);
            else
                MessageBox.Show(result);
        }
    }
}