namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;
    using Google.Apis.Services;

    public class GoogleService : IPeopleService
    {
        public GoogleService()
        {

        }

        private static ContactGroupsResource _groupsResource;
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";
        private static PeopleServiceService _service;

        //TODO: cancelation token
        public async Task<PersonModel> CreateContact(PersonModel personModel)
        {
            var request = _service.People.CreateContact(personModel.Map());
            var createdContact = await request.ExecuteAsync();

            return new PersonModel(createdContact, string.Empty);
        }

        public async Task<bool> DeleteGroup(GroupModel model)
        {
            try
            {
                await _groupsResource.Delete(model.ModelResourceName).ExecuteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ContactModel>> GetContacts()
        {
            var personFields = "names,emailAddresses,phoneNumbers,organizations,memberships";
            var peopleRequest = _service.People.Connections.List("people/me");
            peopleRequest.PersonFields = personFields;
            var connectionsResponse = await peopleRequest.ExecuteAsync();
            var connections = connectionsResponse.Connections;

            return connections.Select(person => (ContactModel)new PersonModel(person, string.Empty)).ToList();
        }

        public async Task<List<ContactModel>> GetGroups()
        {
            try
            {
                var response = await _groupsResource.List().ExecuteAsync();
                return response.ContactGroups.Select(group => (ContactModel)new GroupModel(group, string.Empty)).ToList();
            }
            catch
            {
                return new List<ContactModel>();
            }
        }

        public static void Initialize()
        {
            // Create OAuth credential.
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = M_CLIENT_ID,
                    ClientSecret = M_CLIENT_SECRET
                },
                new[] { "https://www.googleapis.com/auth/contacts" },
                "user",
                CancellationToken.None).Result;

            // Create the service.
            _service = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "My App",
            });

            _groupsResource = new ContactGroupsResource(_service);
        }

        //TODO: not used
        public async Task<ModifyContactGroupMembersResponse> ModifyGroup(string groupResourceName,
            List<string> resources)
        {
            var members = _groupsResource.Members;

            var request = new ModifyContactGroupMembersRequest
            {
                ResourceNamesToAdd = resources
            };

            return await members.Modify(request, groupResourceName).ExecuteAsync();
        }

        public async Task<List<PersonModel>> SearchContact(string query, string readMask)
        {
            var request = _service.People.SearchContacts();
            request.Query = query;
            request.ReadMask = readMask;
            var response = await request.ExecuteAsync();

            var personModels = new List<PersonModel>();

            foreach (var result in response.Results)
            {
                personModels.Add(new PersonModel(result.Person, string.Empty));
            }

            return personModels;
        }

        public async Task<bool> TryToDeleteContact(PersonModel personModel)
        {
            try
            {
                await _service.People.DeleteContact(personModel.ModelResourceName).ExecuteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //todo:get and update (coz etag after update changes)
        public async Task<PersonModel> UpdateContact(PersonModel personModel)
        {
            //TODO: to elsewhere
            var personFields = "names,emailAddresses,phoneNumbers,organizations,memberships";
            var person = personModel.Map();
            var updateRequest = _service.People.UpdateContact(person, personModel.ModelResourceName);
            updateRequest.UpdatePersonFields = personFields;
            var updatedContact = await updateRequest.ExecuteAsync();

            return new PersonModel(updatedContact, string.Empty);
        }

        public async Task<GroupModel> UpdateGroup(GroupModel model)
        {
            if (model == null)
                return null;

            var request = new UpdateContactGroupRequest
            {
                ContactGroup = model.Map()
            };

            ContactGroup response;

            try
            {
                response = await _groupsResource.Update(request, model.ModelResourceName).ExecuteAsync();
            }
            catch
            {
                return null;
            }

            return new GroupModel(response, string.Empty);
        }
    }
}