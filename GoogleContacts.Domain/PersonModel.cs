namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.PeopleService.v1.Data;

    public class PersonModel : ContactModel
    {
        private readonly string _modelOrganization;
        public readonly string Email;
        public readonly string ModelEtag;
        public readonly string FamilyName;
        public readonly string PhoneNumber;
        public readonly string ModelResourceName;
        private ContactGroupMembership modelMembership;

        public PersonModel(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            var name = person.Names?.FirstOrDefault();
            var email = person.EmailAddresses?.FirstOrDefault();
            var phoneNumber = person.PhoneNumbers?.FirstOrDefault();
            var organization = person.Organizations?.FirstOrDefault();
            var membership = person.Memberships?.FirstOrDefault();

            ModelResourceName = person.ResourceName;
            ModelEtag = person.ETag;

            Name = name?.GivenName ?? string.Empty;
            FamilyName = name?.FamilyName ?? string.Empty;
            PhoneNumber = phoneNumber?.Value ?? string.Empty;
            Email = email?.Value ?? string.Empty;
            _modelOrganization = organization?.Name ?? string.Empty;
            modelMembership = membership?.ContactGroupMembership;
        }

        public PersonModel(string name, string familyName, string email, string phoneNumber)
        {
            Name = name;
            FamilyName = familyName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public Person Map()
        {
            return new Person
            {
                ResourceName = ModelResourceName,
                ETag = ModelEtag,
                Names = new List<Name> { new Name { GivenName = Name, FamilyName = FamilyName } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = PhoneNumber } },
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Value = Email } },
                Organizations = new List<Organization> { new Organization { Name = _modelOrganization } }
            };
        }
    }
}