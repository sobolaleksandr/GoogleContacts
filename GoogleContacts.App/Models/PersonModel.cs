namespace GoogleContacts.App.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.PeopleService.v1.Data;

    using GoogleContacts.App.ViewModels;

    /// <summary>
    /// Модель контакта.
    /// </summary>
    public class PersonModel : ContactModel
    {
        /// <summary>
        /// Модель контакта.
        /// </summary>
        /// <param name="person"> Контакт. </param>
        /// <param name="error"> Ошибка. </param>
        public PersonModel(Person person, string error) : base(error)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            var name = person.Names?.FirstOrDefault();
            var email = person.EmailAddresses?.FirstOrDefault();
            var phoneNumber = person.PhoneNumbers?.FirstOrDefault();
            var organization = person.Organizations?.FirstOrDefault();
            var membership = person.Memberships?.FirstOrDefault();

            ResourceName = person.ResourceName;
            ETag = person.ETag;
            GivenName = name?.GivenName ?? string.Empty;
            FamilyName = name?.FamilyName ?? string.Empty;
            PhoneNumber = phoneNumber?.Value ?? string.Empty;
            Email = email?.Value ?? string.Empty;
            Organization = organization?.Name ?? string.Empty;
            GroupResourceName = membership?.ContactGroupMembership.ContactGroupResourceName;
            Name = GivenName + " (" + PhoneNumber + ")";
        }

        /// <summary>
        /// Модель контакта.
        /// </summary>
        /// <param name="vm"> Вью-модель контакта. </param>
        /// <param name="error"> Ошибка. </param>
        public PersonModel(PersonViewModel vm, string error) : base(error)
        {
            ApplyFrom(vm);
        }

        /// <summary>
        /// Адрес электронной почты. 
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FamilyName { get; private set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string GivenName { get; private set; }

        /// <summary>
        /// Членство в группах.
        /// </summary>
        public string GroupResourceName { get; private set; }

        /// <summary>
        /// Организация. 
        /// </summary>
        public string Organization { get; private set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Принять изменения.
        /// </summary>
        /// <param name="vm"> Вью-модель контакта. </param>
        public void ApplyFrom(PersonViewModel vm)
        {
            GivenName = vm.GivenName;
            FamilyName = vm.FamilyName;
            PhoneNumber = vm.PhoneNumber;
            Email = vm.Email;
            Name = GivenName + " (" + PhoneNumber + ")";
            Organization = vm.Organization;
            var groupResourceName = vm.SelectedGroup?.ResourceName;
            if (string.IsNullOrEmpty(groupResourceName))
                return;

            GroupResourceName = groupResourceName;
        }

        /// <summary>
        /// Преобразовать в объект для работы с GoogleContacts. 
        /// </summary>
        /// <returns> Объект для работы с GoogleContacts. </returns>
        public Person Map()
        {
            return new Person
            {
                ResourceName = ResourceName,
                ETag = ETag,
                Names = new List<Name> { new Name { GivenName = GivenName, FamilyName = FamilyName } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = PhoneNumber } },
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Value = Email } },
                Organizations = new List<Organization> { new Organization { Name = Organization } },
                Memberships = new List<Membership>
                {
                    new Membership
                    {
                        ContactGroupMembership = new ContactGroupMembership { ContactGroupResourceName = GroupResourceName }
                    }
                }
            };
        }
    }
}