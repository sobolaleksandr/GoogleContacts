namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;
    using Google.Apis.Services;

    public class GroupService : IService<GroupModel>
    {
        private readonly ContactGroupsResource _groupsResource;

        public GroupService(IClientService service)
        {
            _groupsResource = new ContactGroupsResource(service);
        }

        public async Task<ContactModel> Create(GroupModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var group = model.Map();
            var request = new CreateContactGroupRequest
            {
                ContactGroup = group
            };

            var createRequest = _groupsResource.Create(request);

            try
            {
                var response = await createRequest.ExecuteAsync();
                return response != null
                    ? new GroupModel(response, string.Empty)
                    : new ContactModel("Unexpected error");
            }
            catch (Exception exception)
            {
                return new ContactModel(exception.ToString());
            }
        }

        public async Task<string> Delete(GroupModel model)
        {
            if (model == null)
                return "Empty model";

            var request = _groupsResource.Delete(model.ModelResourceName);

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

        public async Task<ContactModel> Update(GroupModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var request = new UpdateContactGroupRequest
            {
                ContactGroup = model.Map()
            };

            var updateRequest = _groupsResource.Update(request, model.ModelResourceName);

            try
            {
                var response = await updateRequest.ExecuteAsync();
                return response != null
                    ? new GroupModel(response, string.Empty)
                    : new ContactModel("Unexpected error");
            }
            catch (Exception exception)
            {
                return new ContactModel(exception.ToString());
            }
        }

        public async Task<List<ContactModel>> Get()
        {
            var request = _groupsResource.List();

            try
            {
                var response = await request.ExecuteAsync();
                return response.ContactGroups
                    .Select(group => (ContactModel)new GroupModel(group, string.Empty))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<ContactModel>();
            }
        }
    }
}