namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    // ReSharper disable once ClassNeverInstantiated.Global
    // Created in DI
    public class PeopleService : BaseService, IPeopleService
    {
        private const string PERSON_FIELDS = "names,emailAddresses,phoneNumbers,organizations,memberships";

        //TODO: cancelation token
        public async Task<ContactModel> Create(PersonModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var person = model.Map();
            var request = Service.People.CreateContact(person);

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

        public async Task<string> Delete(PersonModel model)
        {
            if (model == null)
                return "Empty model";

            var request = Service.People.DeleteContact(model.ModelResourceName);

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

        public async Task<List<ContactModel>> Get()
        {
            var request = Service.People.Connections.List("people/me");
            request.PersonFields = PERSON_FIELDS;

            try
            {
                var connectionsResponse = await request.ExecuteAsync();
                var connections = connectionsResponse.Connections;
                return connections
                    .Select(person => (ContactModel)new PersonModel(person, string.Empty))
                    .ToList();
            }
            catch (Exception exception)
            {
                return new List<ContactModel>();
            }
        }

        public async Task<ContactModel> Update(PersonModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var person = model.Map();
            var request = Service.People.UpdateContact(person, model.ModelResourceName);
            request.UpdatePersonFields = PERSON_FIELDS;

            try
            {
                var response = await request.ExecuteAsync();
                return new PersonModel(response, string.Empty);
            }
            catch (Exception exception)
            {
                return new ContactModel(exception.ToString());
            }
        }
    }
}