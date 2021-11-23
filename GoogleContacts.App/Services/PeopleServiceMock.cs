namespace GoogleContacts.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1.Data;

    using GoogleContacts.App.Models;

    internal class PeopleServiceMock : IService<PersonModel>
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
            var models = Enumerable.Range(0, 10).Select(item =>
                (ContactModel)new PersonModel(new Person(), string.Empty) { Name = $"TestPerson{item}" }).ToList();

            return await Task.FromResult(models);
        }

        public async Task<ContactModel> Update(PersonModel model)
        {
            return await Task.FromResult(model);
        }
    }
}