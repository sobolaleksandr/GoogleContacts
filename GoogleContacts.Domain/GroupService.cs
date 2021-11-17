namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;

    public class GroupService : BaseService, IGroupService
    {
        private readonly ContactGroupsResource _groupsResource;

        public GroupService()
        {
            _groupsResource = new ContactGroupsResource(Service);
        }

        public async Task<string> DeleteGroup(GroupModel model)
        {
            try
            {
                await _groupsResource.Delete(model.ModelResourceName).ExecuteAsync();
                return string.Empty;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        public async Task<List<ContactModel>> GetGroups()
        {
            try
            {
                var response = await _groupsResource.List().ExecuteAsync();
                return response.ContactGroups.Select(group => (ContactModel)new GroupModel(group, string.Empty)).ToList();
            }
            catch (Exception exception)
            {
                return new List<ContactModel>
                {
                    new ContactModel(exception.ToString())
                };
            }
        }

        public async Task<ContactModel> UpdateGroup(GroupModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

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

        public void Stop()
        {
            Dispose();
        }

        public async Task<ContactModel> CreateGroup(GroupModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var request = new CreateContactGroupRequest
            {
                ContactGroup = model.Map()
            };

            try
            {
                var response = await _groupsResource.Create(request).ExecuteAsync();
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