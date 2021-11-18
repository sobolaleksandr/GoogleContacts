namespace GoogleContacts.App.ViewModels
{
    using System.ComponentModel;

    using GoogleContacts.Domain;

    public class GroupViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _name;

        public GroupViewModel()
        {

        }

        public GroupViewModel(GroupModel group)
        {
            Name = group.FormattedName;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public static string NameTitle => "Имя";

        public static string WindowTitle => "Окно редактирования группы";

        public string Error => this[nameof(Name)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                            error = $"Поле {NameTitle} не должно быть пустым!";
                        break;
                }

                return error;
            }
        }
    }
}