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
            return model;
        }

        public async Task<string> Delete(PersonModel model)
        {
            return string.Empty;
        }

        public async Task<List<ContactModel>> Get()
        {
            return Enumerable.Range(0, 10)
                .Select(item =>
                    (ContactModel)new PersonModel($"Test{item}", $"Test{item}", $"Test{item}", $"Test{item}",
                        string.Empty))
                .ToList();
        }

        public async Task<ContactModel> Update(PersonModel model)
        {
            return model;
        }

        public async Task<List<PersonModel>> SearchContact(string query, string readMask)
        {
            return new List<PersonModel>();
        }
    }
}