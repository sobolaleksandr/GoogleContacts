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
        private string _modelEtag;
        public string ModelResourceName;
        private int _memberCount;
        public string FormattedName { get; set; }

        public GroupModel([NotNull] ContactGroup group, string error) : base(error)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ModelResourceName = group.ResourceName;
            _modelEtag = group.ETag;
            FormattedName = group.FormattedName ?? string.Empty;
            _memberCount = group.MemberCount ?? 0;
            Name = FormattedName + " (" + _memberCount + ")";
        }

        public GroupModel(string name, string error) : base(error)
        {
            FormattedName = name;
        }

        public void ApplyFrom(string name)
        {
            FormattedName = name;
        }

        public void ApplyFrom([NotNull] GroupModel group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ModelResourceName = group.ModelResourceName;
            _modelEtag = group._modelEtag;
            FormattedName = group.FormattedName;
            _memberCount = group._memberCount;
            Name = FormattedName + " (" + _memberCount + ")";
        }

        public ContactGroup Map()
        {
            return new ContactGroup
            {
                Name = FormattedName,
                ETag = _modelEtag ?? string.Empty
            };
        }
    }
}