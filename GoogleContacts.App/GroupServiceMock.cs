namespace GoogleContacts.App
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    internal class GroupServiceMock : IGroupService
    {
        public async Task<ContactModel> Create(GroupModel model)
        {
            return new ContactModel(string.Empty);
        }

        public async Task<string> Delete(GroupModel model)
        {
            return string.Empty;
        }

        public async Task<List<ContactModel>> Get()
        {
            return Enumerable.Range(100, 10)
                .Select(item => (ContactModel)new GroupModel($"TetsGroup{item}", string.Empty)).ToList();
        }

        public async Task<ContactModel> Update(GroupModel model)
        {
            return model;
        }
    }
}