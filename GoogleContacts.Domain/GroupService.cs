namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;

    // ReSharper disable once ClassNeverInstantiated.Global
    // Created in DI
    public class GroupService : BaseService, IGroupService
    {
        private readonly ContactGroupsResource _groupsResource;

        public GroupService()
        {
            _groupsResource = new ContactGroupsResource(Service);
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

        public async Task<ContactModel> Get(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
                return new ContactModel("Empty resourceName");

            var request = _groupsResource.Get(resourceName);

            try
            {
                var response = await request.ExecuteAsync();
                return response != null
                    ? new GroupModel(response, string.Empty)
                    : new ContactModel("There is no such group on server");
            }
            catch (Exception exception)
            {
                return new ContactModel(exception.ToString());
            }
        }

        public async Task<List<ContactModel>> GetAll()
        {
            var request = _groupsResource.List();

            try
            {
                var response = await request.ExecuteAsync();
                return response.ContactGroups
                    .Select(group => (ContactModel)new GroupModel(group, string.Empty))
                    .ToList();
            }
            catch (Exception exception)
            {
                return new List<ContactModel>();
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
    }
}