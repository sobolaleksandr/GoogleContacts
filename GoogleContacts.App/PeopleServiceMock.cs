namespace GoogleContacts.App
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    internal class PeopleServiceMock : IPeopleService
    {
        public async Task<ContactModel> Create(PersonModel model)
        {
            return await Task.FromResult(model);
        }

        public async Task<string> Delete(PersonModel model)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<List<ContactModel>> Get()
        {
            var models = Enumerable.Range(0, 10)
                .Select(item =>
                    (ContactModel)new PersonModel($"Test{item}", $"Test{item}", $"Test{item}", $"Test{item}",
                        new ContactModel(string.Empty),
                        string.Empty))
                .ToList();
            return await Task.FromResult(models);
        }

        public async Task<ContactModel> Update(PersonModel model)
        {
            return await Task.FromResult(model);
        }
    }
}