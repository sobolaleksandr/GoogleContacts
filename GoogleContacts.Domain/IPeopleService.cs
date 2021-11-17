namespace GoogleContacts.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPeopleService
    {
        Task<PersonModel> CreateContact(PersonModel personModel);
        Task<List<ContactModel>> GetContacts();
        Task<bool> TryToDeleteContact(PersonModel personModel);
        Task<PersonModel> UpdateContact(PersonModel personModel);
    }
}