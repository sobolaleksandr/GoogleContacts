namespace GoogleContacts.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.PeopleService.v1.Data;

    public class PersonModel : ContactModel
    {
        private string _modelOrganization;

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
            ModelMembership = membership?.ContactGroupMembership;
            Name = GivenName + " (" + PhoneNumber + ")";
        }

        public PersonModel(string name, string familyName, string email, string information, ContactModel group,
            string error) : base(error)
        {
            GivenName = name;
            FamilyName = familyName;
            PhoneNumber = information;
            Email = email;
            Name = GivenName + " (" + PhoneNumber + ")";
            if (group == null)
                return;

            ModelMembership = new ContactGroupMembership { ContactGroupResourceName = group.ModelResourceName };
        }

        public string Email { get; private set; }
        public string FamilyName { get; private set; }
        public string GivenName { get; private set; }
        public ContactGroupMembership ModelMembership { get; private set; }
        public string PhoneNumber { get; private set; }

        public void ApplyFrom(string name, string familyName, string email, string phoneNumber,
            ContactModel group)
        {
            GivenName = name;
            FamilyName = familyName;
            PhoneNumber = phoneNumber;
            Email = email;
            Name = GivenName + " (" + PhoneNumber + ")";
            if (group == null)
                return;

            ModelMembership = new ContactGroupMembership { ContactGroupResourceName = group.ModelResourceName };
        }

        public virtual void ApplyFrom(ContactModel model)
        {
            if (!(model is PersonModel person))
                return;

            ModelResourceName = person.ModelResourceName;
            ModelEtag = person.ModelEtag;

            GivenName = person.GivenName;
            FamilyName = person.FamilyName;
            PhoneNumber = person.PhoneNumber;
            Email = person.Email;
            _modelOrganization = person._modelOrganization;
            ModelMembership = person.ModelMembership;
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
                Memberships = new List<Membership> { new Membership { ContactGroupMembership = ModelMembership } }
            };
        }
    }
}