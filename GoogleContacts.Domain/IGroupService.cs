namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGroupService
    {
        Task<ContactModel> CreateGroup(GroupModel model);
        Task<string> DeleteGroup(GroupModel model);
        Task<List<ContactModel>> GetGroups();
        Task<ContactModel> UpdateGroup(GroupModel model);
        void Stop();
    }
}