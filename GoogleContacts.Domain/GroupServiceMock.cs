namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GroupServiceMock : IService<GroupModel>
    {
        public async Task<ContactModel> Create(GroupModel model)
        {
            return await Task.FromResult(new ContactModel(string.Empty));
        }

        public async Task<string> Delete(GroupModel model)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<List<ContactModel>> Get()
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