namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPeopleService
    {
        Task<ContactModel> Create(PersonModel model);
        Task<string> Delete(PersonModel model);
        Task<List<ContactModel>> Get();
        Task<ContactModel> Update(PersonModel model);
    }
}