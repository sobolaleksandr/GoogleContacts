namespace GoogleContacts.Domain
{
    using System;

    using Google.Apis.PeopleService.v1.Data;

    /// <summary>
    /// Модель группы.
    /// </summary>
    public class GroupModel : ContactModel
    {
        private int _memberCount;

        public GroupModel([NotNull] ContactGroup group, string error) : base(error)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            ModelResourceName = group.ResourceName;
            ModelEtag = group.ETag;
            FormattedName = group.FormattedName ?? string.Empty;
            _memberCount = group.MemberCount ?? 0;
            Name = FormattedName + " (" + _memberCount + ")";
        }

        public GroupModel(string name, string error) : base(error)
        {
            FormattedName = name;
            Name = FormattedName + " (" + _memberCount + ")";
        }

        public string FormattedName { get; private set; }

        public void ApplyFrom(string name)
        {
            FormattedName = name;
        }

        public virtual void ApplyFrom(ContactModel model)
        {
            if (!(model is GroupModel group))
               return;

            ModelResourceName = group.ModelResourceName;
            ModelEtag = group.ModelEtag;
            FormattedName = group.FormattedName;
            _memberCount = group._memberCount;
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