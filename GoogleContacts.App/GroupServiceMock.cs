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
            return await Task.FromResult(new ContactModel(string.Empty));
        }

        public async Task<string> Delete(GroupModel model)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<List<ContactModel>> GetAll()
        {
            var models = Enumerable.Range(100, 10)
                .Select(item => (ContactModel)new GroupModel($"TetsGroup{item}", string.Empty)).ToList();

            return await Task.FromResult(models);
        }

        public async Task<ContactModel> Update(GroupModel model)
        {
            return await Task.FromResult(model);
        }
    }
}