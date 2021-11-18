namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;

    public class GoogleService
    {
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";

        private static ContactGroupsResource _groupsResource;
        private static PeopleServiceService _service;

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
    }
}