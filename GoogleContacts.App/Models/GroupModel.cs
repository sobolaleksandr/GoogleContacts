namespace GoogleContacts.App.Models
{
    using System;

    using Google.Apis.PeopleService.v1.Data;

    /// <summary>
    /// Модель группы.
    /// </summary>
    public class GroupModel : ContactModel
    {
        /// <summary>
        /// Количество членов группы.
        /// </summary>
        private readonly int _memberCount;

        /// <summary>
        /// Модель группы.
        /// </summary>
        /// <param name="group"> Группа. </param>
        /// <param name="error"> Ошибка. </param>
        public GroupModel(ContactGroup group, string error) : base(error)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ResourceName = group.ResourceName;
            ETag = group.ETag;
            _memberCount = group.MemberCount ?? 0;
            ApplyFrom(group.FormattedName ?? string.Empty);
        }

        /// <summary>
        /// Модель группы.
        /// </summary>
        /// <param name="name"> Наименование. </param>
        /// <param name="error"> Ошибка. </param>
        public GroupModel(string name, string error) : base(error)
        {
            ApplyFrom(name);
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string FormattedName { get; private set; }

        /// <summary>
        /// Принять изменения.
        /// </summary>
        /// <param name="name"> Наименование. </param>
        public void ApplyFrom(string name)
        {
            FormattedName = name;
            Name = FormattedName + " (" + _memberCount + ")";
        }

        /// <summary>
        /// Преобразовать в объект для работы с GoogleContacts. 
        /// </summary>
        /// <returns> Объект для работы с GoogleContacts. </returns>
        public ContactGroup Map()
        {
            return new ContactGroup
            {
                Name = FormattedName,
                ETag = ETag ?? string.Empty
            };
        }
    }
}