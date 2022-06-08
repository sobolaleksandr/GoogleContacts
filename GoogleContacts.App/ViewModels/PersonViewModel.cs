namespace GoogleContacts.App.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using GoogleContacts.App.Models;

    /// <summary>
    /// Вью-модель <see cref="PersonModel"/>
    /// </summary>
    public sealed class PersonViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="Email"/>
        /// </summary>
        private string _email;

        /// <summary>
        /// Поле свойства <see cref="FamilyName"/>
        /// </summary>
        private string _familyName;

        /// <summary>
        /// Поле свойства <see cref="GivenName"/>
        /// </summary>
        private string _givenName;

        /// <summary>
        /// Поле свойства <see cref="Organization"/>
        /// </summary>
        private string _organization;

        /// <summary>
        /// Поле свойства <see cref="PhoneNumber"/>
        /// </summary>
        private string _phoneNumber;

        /// <summary>
        /// Поле свойства <see cref="SelectedGroup"/>
        /// </summary>
        private ContactModel _selectedGroup;

        /// <summary>
        /// Вью-модель <see cref="PersonModel"/>
        /// </summary>
        /// <param name="person"> Контакт. </param>
        /// <param name="groups"> Группы. </param>
        public PersonViewModel(PersonModel person, ObservableCollection<ContactModel> groups) : this(groups)
        {
            Email = person.Email;
            PhoneNumber = person.PhoneNumber;
            GivenName = person.GivenName;
            FamilyName = person.FamilyName;
            Organization = person.Organization;
            var groupResourceName = person.GroupResourceName;
            if (string.IsNullOrEmpty(groupResourceName))
                return;

            SelectedGroup = groups.FirstOrDefault(group => group.ResourceName == groupResourceName);
        }

        /// <summary>
        /// Вью-модель <see cref="PersonModel"/>
        /// </summary>
        /// <param name="groups"> Группы. </param>
        public PersonViewModel(ObservableCollection<ContactModel> groups)
        {
            Groups = groups;
        }

        /// <summary>
        /// Адрес электронной почты. 
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="Email"/>
        /// </summary>
        public static string EmailTitle => "Адрес электронной почты";

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="FamilyName"/>
        /// </summary>
        public static string FamilyNameTitle => "Фамилия";

        /// <summary>
        /// Имя.
        /// </summary>
        public string GivenName
        {
            get => _givenName;
            set
            {
                _givenName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="GivenName"/>
        /// </summary>
        public static string GivenNameTitle => "Имя";

        /// <summary>
        /// Группы.
        /// </summary>
        public ObservableCollection<ContactModel> Groups { get; set; }

        /// <summary>
        /// Наименование атрибута <see cref="Groups"/>
        /// </summary>
        public static string GroupTitle => "Группы";

        /// <summary>
        /// Организация. 
        /// </summary>
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="Organization"/>
        /// </summary>
        public static string OrganizationTitle => "Организация";

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="PhoneNumber"/>
        /// </summary>
        public static string PhoneNumberTitle => "Номер телефона";

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public ContactModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Окно редактирования контакта";

        public string Error => this[nameof(Email)] + this[nameof(FamilyName)] + this[nameof(PhoneNumber)] +
                               this[nameof(SelectedGroup)] + this[nameof(Organization)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(Email):
                        if (string.IsNullOrEmpty(Email))
                            error = $"Поле {EmailTitle} не должно быть пустым!";
                        break;
                    case nameof(FamilyName):
                        if (string.IsNullOrEmpty(FamilyName))
                            error = $"Поле {FamilyNameTitle} не должно быть пустым!";
                        break;
                    case nameof(PhoneNumber):
                        if (string.IsNullOrEmpty(PhoneNumber))
                            error = $"Поле {PhoneNumberTitle} не должно быть пустым!";
                        break;
                    case nameof(GivenName):
                        if (string.IsNullOrEmpty(GivenName))
                            error = $"Поле {GivenNameTitle} не должно быть пустым!";
                        break;
                    case nameof(SelectedGroup):
                        error = string.Empty;
                        break;
                    case nameof(Organization):
                        if (string.IsNullOrEmpty(Organization))
                            error = $"Поле {OrganizationTitle} не должно быть пустым!";
                        break;
                }

                return error;
            }
        }
    }
}