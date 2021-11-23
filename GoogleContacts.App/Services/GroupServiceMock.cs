namespace GoogleContacts.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;

    /// <summary>
    /// Заглушка для проверки <see cref="GroupModel"/>
    /// </summary>
    internal class GroupServiceMock : IService<GroupModel>
    {
        public async Task<ContactModel> CreateAsync(GroupModel model)
        {
            return await Task.FromResult(new ContactModel(string.Empty));
        }

        public async Task<string> DeleteAsync(GroupModel model)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<List<ContactModel>> GetAsync()
        {
            var models = Enumerable.Range(100, 10)
                .Select(item => (ContactModel)new GroupModel($"TetsGroup{item}", string.Empty)).ToList();

            return await Task.FromResult(models);
        }

        public async Task<ContactModel> UpdateAsync(GroupModel model)
        {
            return await Task.FromResult(model);
        }
    }
}