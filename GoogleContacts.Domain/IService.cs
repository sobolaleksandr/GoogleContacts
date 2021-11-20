namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IService<in T> where T : ContactModel
    {
        Task<ContactModel> Create(T model);
        Task<string> Delete(T model);
        Task<List<ContactModel>> Get();
        Task<ContactModel> Update(T model);
    }
}