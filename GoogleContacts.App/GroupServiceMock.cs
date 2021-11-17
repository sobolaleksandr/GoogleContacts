namespace GoogleContacts.App
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    internal class GroupServiceMock : IGroupService
    {
        public async Task<ContactModel> CreateGroup(GroupModel model)
        {
            return new ContactModel(string.Empty);
        }

        public async Task<string> DeleteGroup(GroupModel model)
        {
            return string.Empty;
        }

        public async Task<List<ContactModel>> GetGroups()
        {
            return Enumerable.Range(100, 10)
                .Select(item => (ContactModel)new GroupModel($"TetsGroup{item}", string.Empty)).ToList();
        }

        public async Task<ContactModel> UpdateGroup(GroupModel model)
        {
            return model;
        }

        public void Stop()
        {
        }
    }
}