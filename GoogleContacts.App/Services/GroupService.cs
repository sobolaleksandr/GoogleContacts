namespace GoogleContacts.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;
    using Google.Apis.Services;

    using GoogleContacts.App.Models;

    /// <summary>
    /// Сервия для работы с <see cref="GroupModel"/>
    /// </summary>
    public class GroupService : IService<GroupModel>
    {
        /// <summary>
        /// Ресурс для работы с <see cref="GroupModel"/> 
        /// </summary>
        private readonly ContactGroupsResource _groupsResource;

        /// <summary>
        /// Сервия для работы с <see cref="GroupModel"/>
        /// </summary>
        /// <param name="service"> Ресурс для работы с <see cref="PersonModel"/> </param>
        public GroupService(IClientService service)
        {
            _groupsResource = new ContactGroupsResource(service);
        }

        public async Task<ContactModel> CreateAsync(GroupModel model)
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

        public async Task<string> DeleteAsync(GroupModel model)
        {
            if (model == null)
                return "Empty model";

            var request = _groupsResource.Delete(model.ResourceName);

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
            var request = _groupsResource.List();

            try
            {
                var response = await request.ExecuteAsync();
                var groups = response.ContactGroups.Where(group => group.GroupType != "SYSTEM_CONTACT_GROUP");
                return groups
                    .Select(group => (ContactModel)new GroupModel(group, string.Empty))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<ContactModel>();
            }
        }

        public async Task<ContactModel> UpdateAsync(GroupModel model)
        {
            if (model == null)
                return new ContactModel("Empty model");

            var request = new UpdateContactGroupRequest
            {
                ContactGroup = model.Map()
            };

            var updateRequest = _groupsResource.Update(request, model.ResourceName);

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