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

    public static class GoogleService
    {
        private static ContactGroupsResource _groupsResource;
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";
        private static PeopleServiceService _service;

        public static async Task<PersonModel> CreateContact(PersonModel personModel)
        {
            var createdContact = await _service.People.CreateContact(personModel.Map()).ExecuteAsync();

            return new PersonModel(createdContact);
        }

        public static async Task<GroupModel> CreateGroup(GroupModel model)
        {
            if (model == null)
                return null;

            var request = new CreateContactGroupRequest
            {
                ContactGroup = model.Map()
            };

            try
            {
                var response = await _groupsResource.Create(request).ExecuteAsync();
                return new GroupModel(response);
            }
            catch
            {
                return null;
            }
        }

        public static async Task<bool> DeleteGroup(GroupModel model)
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

        public static async Task<List<PersonModel>> GetContacts(string personFields)
        {
            var peopleRequest = _service.People.Connections.List("people/me");
            peopleRequest.PersonFields = personFields;
            var connectionsResponse = await peopleRequest.ExecuteAsync();
            var connections = connectionsResponse.Connections;

            return connections.Select(person => new PersonModel(person)).ToList();
        }

        public static async Task<List<GroupModel>> GetGroups()
        {
            try
            {
                var response = await _groupsResource.List().ExecuteAsync();
                return response.ContactGroups.Select(group => new GroupModel(group)).ToList();
            }
            catch
            {
                return new List<GroupModel>();
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

        public static async Task<ModifyContactGroupMembersResponse> ModifyGroup(string groupResourceName,
            List<string> resources)
        {
            var members = _groupsResource.Members;

            var request = new ModifyContactGroupMembersRequest
            {
                ResourceNamesToAdd = resources
            };

            return await members.Modify(request, groupResourceName).ExecuteAsync();
        }

        public static async Task<List<PersonModel>> SearchContact(string query, string readMask)
        {
            var request = _service.People.SearchContacts();
            request.Query = query;
            request.ReadMask = readMask;
            var response = await request.ExecuteAsync();

            var personModels = new List<PersonModel>();

            foreach (var result in response.Results)
            {
                personModels.Add(new PersonModel(result.Person));
            }

            return personModels;
        }

        public static async Task<bool> TryToDeleteContact(PersonModel personModel)
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
        public static async Task<PersonModel> UpdateContact(PersonModel personModel, string personFields)
        {
            var person = personModel.Map();
            var updateRequest = _service.People.UpdateContact(person, personModel.ModelResourceName);
            updateRequest.UpdatePersonFields = personFields;
            var updatedContact = await updateRequest.ExecuteAsync();

            return new PersonModel(updatedContact);
        }

        public static async Task<GroupModel> UpdateGroup(GroupModel model)
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

            return new GroupModel(response);
        }
    }

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var personModel = new PersonModel("John", "Doe", "JohnD@yahoo.com", "+7800553535");
            var groupModel = new GroupModel("testGroup2");
            var query = "Ринат"; // "JohnD@yahoo.com";
            var properties = "names,emailAddresses"; // "emailAddresses";
            var personFields = "names,emailAddresses,phoneNumbers,organizations,memberships";
            var resources = new List<string> { "people/c8717037971012891222" };

            GoogleService.Initialize();
            var groups = await GoogleService.GetGroups();
            //GroupModel group = await GoogleContacts.CreateGroup(groupModel);
            //var modGroup = await GoogleContacts.ModifyGroup("contactGroups/2f4d42e08a6f5e7f",resources);
            //groupModel.modelResourceName = "contactGroups/2f4d42e08a6f5e7f";
            //var updated = await GoogleContacts.UpdateGroup(groups.FirstOrDefault());
            //GoogleContacts.CreateContact(personModel);
            var model = (await GoogleService.GetContacts(personFields)).FirstOrDefault();
            //model.modelEmail = "JohnD@yahoo.com";
            //var model = (await GoogleContacts.SearchContact(query, properties)).FirstOrDefault();
            //await GoogleContacts.UpdateContact(model, personFields);
            //await GoogleContacts.TryToDeleteContact(model);
            Console.WriteLine();
        }
    }
}