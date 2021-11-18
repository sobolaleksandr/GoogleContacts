namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //var personModel = new PersonModel("John", "Doe", "JohnD@yahoo.com", "+7800553535");
            //var groupModel = new GroupModel("testGroup2");
            var query = "Ринат"; // "JohnD@yahoo.com";
            var properties = "names,emailAddresses"; // "emailAddresses";
            var personFields = "names,emailAddresses,phoneNumbers,organizations,memberships";
            var resources = new List<string> { "people/c8717037971012891222" };

            //GoogleService.Initialize();
            //var groups = await GoogleService.Get();
            //GroupModel group = await GoogleContacts.Create(groupModel);
            //var modGroup = await GoogleContacts.ModifyGroup("contactGroups/2f4d42e08a6f5e7f",resources);
            //groupModel.modelResourceName = "contactGroups/2f4d42e08a6f5e7f";
            //var updated = await GoogleContacts.Update(groups.FirstOrDefault());
            //GoogleContacts.Create(personModel);
            //var model = (await GoogleService.Get(personFields)).FirstOrDefault();
            //model.modelEmail = "JohnD@yahoo.com";
            //var model = (await GoogleContacts.SearchContact(query, properties)).FirstOrDefault();
            //await GoogleContacts.Update(model, personFields);
            //await GoogleContacts.Delete(model);
            Console.WriteLine();
        }
    }
}