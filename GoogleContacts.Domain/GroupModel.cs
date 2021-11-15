namespace GoogleContacts.Domain
{
    using Google.Apis.PeopleService.v1.Data;

    /// <summary>
    /// Модель группы.
    /// </summary>
    public class GroupModel : ContactModel
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _modelEtag;
        public readonly string ModelResourceName;
        public int ModelMemberCount;

        public GroupModel(ContactGroup group)
        {
            ModelResourceName = group.ResourceName;
            _modelEtag = group.ETag;
            Name = group.FormattedName ?? string.Empty;
            ModelMemberCount = group.MemberCount ?? 0;
        }

        public GroupModel(string name)
        {
            Name = name;
        }

        public ContactGroup Map()
        {
            return new ContactGroup
            {
                Name = Name,
                ETag = _modelEtag
            };
        }
    }
}