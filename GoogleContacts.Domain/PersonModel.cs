namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.PeopleService.v1.Data;

    public class PersonModel : ContactModel
    {
        private string _modelOrganization;
        private ContactGroupMembership modelMembership;

        public PersonModel(Person person, string error) : base(error)
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

            GivenName = name?.GivenName ?? string.Empty;
            FamilyName = name?.FamilyName ?? string.Empty;
            PhoneNumber = phoneNumber?.Value ?? string.Empty;
            Email = email?.Value ?? string.Empty;
            _modelOrganization = organization?.Name ?? string.Empty;
            modelMembership = membership?.ContactGroupMembership;
            Name = GivenName + " (" + PhoneNumber + ")";
        }

        public PersonModel(string name, string familyName, string email, string information, string error) : base(error)
        {
            GivenName = name;
            FamilyName = familyName;
            PhoneNumber = information;
            Email = email;
            Name = GivenName + " (" + PhoneNumber + ")";
        }

        public string Email { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string ModelEtag { get; set; }
        public string ModelResourceName { get; set; }
        public string PhoneNumber { get; set; }

        public void ApplyFrom(string name, string familyName, string email, string phoneNumber)
        {
            GivenName = name;
            FamilyName = familyName;
            PhoneNumber = phoneNumber;
            Email = email;
            Name = GivenName + " (" + PhoneNumber + ")";
        }

        public Person Map()
        {
            return new Person
            {
                ResourceName = ModelResourceName,
                ETag = ModelEtag,
                Names = new List<Name> { new Name { GivenName = GivenName, FamilyName = FamilyName } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = PhoneNumber } },
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Value = Email } },
                Organizations = new List<Organization> { new Organization { Name = _modelOrganization } },
                Memberships = new List<Membership> { new Membership { ContactGroupMembership = modelMembership } }
            };
        }

        public void ApplyFrom(PersonModel person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            ModelResourceName = person.ModelResourceName;
            ModelEtag = person.ModelEtag;

            GivenName = person.GivenName;
            FamilyName = person.FamilyName;
            PhoneNumber = person.PhoneNumber;
            Email = person.Email;
            _modelOrganization = person._modelOrganization;
            modelMembership = person.modelMembership;
            Name = GivenName + " (" + PhoneNumber + ")";
        }
    }
}