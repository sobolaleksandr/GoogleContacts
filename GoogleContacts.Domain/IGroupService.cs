﻿namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGroupService
    {
        Task<ContactModel> Create(GroupModel model);
        Task<string> Delete(GroupModel model);
        Task<List<ContactModel>> GetAll();
        Task<ContactModel> Update(GroupModel model);
        Task<ContactModel> Get(string resourceName);
    }
}