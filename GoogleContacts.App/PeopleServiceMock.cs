using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleContacts.App
{
    using System.Linq;
    using System.Threading.Tasks;

    using GoogleContacts.Domain;

    class PeopleServiceMock: IPeopleService
    {
        public async Task<PersonModel> CreateContact(PersonModel personModel)
        {
            return personModel;
        }

        public async Task<List<ContactModel>> GetContacts()
        {
            return Enumerable.Range(0, 10)
                .Select(item =>
                    (ContactModel)new PersonModel($"Test{item}", $"Test{item}", $"Test{item}", $"Test{item}", string.Empty))
                .ToList();

        }

        public async Task<List<PersonModel>> SearchContact(string query, string readMask)
        {
            return new List<PersonModel>();
        }

        public async Task<bool> TryToDeleteContact(PersonModel personModel)
        {
            return true;
        }

        public async Task<PersonModel> UpdateContact(PersonModel personModel)
        {
            return personModel;
        }
    }
}
