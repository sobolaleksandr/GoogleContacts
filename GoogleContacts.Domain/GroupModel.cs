namespace GoogleContacts.Domain
{
    using System;

    using Google.Apis.PeopleService.v1.Data;

    using GoogleContacts.Domain.Annotations;

    /// <summary>
    /// Модель группы.
    /// </summary>
    public class GroupModel : ContactModel
    {
        private readonly string _modelEtag;

        public readonly string ModelResourceName;

        public GroupModel([NotNull] ContactGroup group, string error) : base(error)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ModelResourceName = group.ResourceName;
            _modelEtag = group.ETag;
            Name = group.FormattedName ?? string.Empty;

            var memberCount = group.MemberCount ?? 0;
            Name += " (" + memberCount + ")";
        }

        public GroupModel(string name, string error) : base(error)
        {
            Name = name;
        }

        public void ApplyFrom(string name)
        {
            Name = name;
        }

        public ContactGroup Map()
        {
            return new ContactGroup
            {
                Name = Name,
                ETag = _modelEtag ?? string.Empty
            };
        }
    }
}