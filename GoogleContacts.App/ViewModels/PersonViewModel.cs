﻿namespace GoogleContacts.App.ViewModels
{
    using System.ComponentModel;

    using GoogleContacts.Domain;

    public sealed class PersonViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _email;
        private string _familyName;
        private string _givenName;
        private string _phoneNumber;

        public PersonViewModel(PersonModel model)
        {
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            GivenName = model.Name;
            FamilyName = model.FamilyName;
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public static string EmailTitle => "Адрес электронной почты";

        public string FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                OnPropertyChanged();
            }
        }

        public static string FamilyNameTitle => "Фамилия";

        public static string GivenNameTitle => "Имя";

        public string GivenName
        {
            get => _givenName;
            set
            {
                _givenName = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public static string PhoneNumberTitle => "Номер телефона";

        public static string WindowTitle => "Окно редактирования контакта";

        public string Error => this[nameof(Email)] + this[nameof(FamilyName)] + this[nameof(PhoneNumber)];

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
                }

                return error;
            }
        }
    }
}