namespace GoogleContacts.App.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.PeopleService.v1.Data;

    using GoogleContacts.App.ViewModels;

    public class PersonModel : ContactModel
    {
        public string Organization { get; set; }

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
            Organization = organization?.Name ?? string.Empty;
            Membership = membership?.ContactGroupMembership;
            Name = GivenName + " (" + PhoneNumber + ")";
        }

        public PersonModel(PersonViewModel vm, string error) : base(error)
        {
            ApplyFrom(vm);
        }

        public string Email { get; private set; }
        public string FamilyName { get; private set; }
        public string GivenName { get; private set; }
        public ContactGroupMembership Membership { get; private set; }
        public string PhoneNumber { get; private set; }

        public void ApplyFrom(PersonViewModel vm)
        {
            GivenName = vm.GivenName;
            FamilyName = vm.FamilyName;
            PhoneNumber = vm.PhoneNumber;
            Email = vm.Email;
            Name = GivenName + " (" + PhoneNumber + ")";
            Organization = vm.Organization;
            var groupResourceName = vm.Group?.ModelResourceName;
            if (string.IsNullOrEmpty(groupResourceName))
                return;

            Membership = new ContactGroupMembership { ContactGroupResourceName = groupResourceName };
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
                Organizations = new List<Organization> { new Organization { Name = Organization } },
                Memberships = new List<Membership> { new Membership { ContactGroupMembership = Membership } }
            };
        }
    }
}