namespace GoogleContacts.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;

    using GoogleContacts.App.Models;

    /// <summary>
    /// Сервия для работы с <see cref="PersonModel"/>
    /// </summary>
    public class PeopleService : IService<PersonModel>
    {
        /// <summary>
        /// Поля для запроса данных контакта.
        /// </summary>
        private const string PERSON_FIELDS = "names,emailAddresses,phoneNumbers,organizations,memberships";

        /// <summary>
        /// Ресурс для работы с <see cref="PersonModel"/>
        /// </summary>
        private readonly PeopleServiceService _service;

        /// <summary>
        /// Сервия для работы с <see cref="PersonModel"/>
        /// </summary>
        /// <param name="service"> Ресурс для работы с <see cref="PersonModel"/> </param>
        public PeopleService(PeopleServiceService service)
        {
            _service = service;
        }

        public async Task<ContactModel> CreateAsync(PersonModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var person = model.Map();
            var request = _service.People.CreateContact(person);

            try
            {
                var response = await request.ExecuteAsync();
                return response != null
                    ? new PersonModel(response, string.Empty)
                    : new ContactModel("Unexpected error");
            }
            catch (Exception exception)
            {
                return new ContactModel(exception.ToString());
            }
        }

        public async Task<string> DeleteAsync(PersonModel model)
        {
            if (model == null)
                return "Empty model";

            var request = _service.People.DeleteContact(model.ResourceName);

            try
            {
                await request.ExecuteAsync();
                return string.Empty;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        public async Task<List<ContactModel>> GetAsync()
        {
            var request = _service.People.Connections.List("people/me");
            request.PersonFields = PERSON_FIELDS;

            try
            {
                var connectionsResponse = await request.ExecuteAsync();
                var connections = connectionsResponse.Connections;
                return connections
                    .Select(person => (ContactModel)new PersonModel(person, string.Empty))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<ContactModel>();
            }
        }

        public async Task<ContactModel> UpdateAsync(PersonModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var person = model.Map();
            var request = _service.People.UpdateContact(person, model.ResourceName);
            request.UpdatePersonFields = PERSON_FIELDS;

            try
            {
                var response = await request.ExecuteAsync();
                return response != null
                    ? new PersonModel(response, string.Empty)
                    : new ContactModel("Unexpected error");
            }
            catch (Exception exception)
            {
                return new ContactModel(exception.ToString());
            }
        }
    }
}