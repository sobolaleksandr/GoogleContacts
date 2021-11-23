namespace GoogleContacts.App.Models
{
    using System;

    using Google.Apis.PeopleService.v1.Data;

    /// <summary>
    /// Модель группы.
    /// </summary>
    public class GroupModel : ContactModel
    {
        private readonly int _memberCount;

        public GroupModel(ContactGroup group, string error) : base(error)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ModelResourceName = group.ResourceName;
            ModelEtag = group.ETag;
            _memberCount = group.MemberCount ?? 0;
            ApplyFrom(group.FormattedName ?? string.Empty);
        }

        public GroupModel(string name, string error) : base(error)
        {
            ApplyFrom(name);
        }

        public string FormattedName { get; private set; }

        public void ApplyFrom(string name)
        {
            FormattedName = name;
            Name = FormattedName + " (" + _memberCount + ")";
        }

        public ContactGroup Map()
        {
            return new ContactGroup
            {
                Name = FormattedName,
                ETag = ModelEtag ?? string.Empty
            };
        }
    }
}