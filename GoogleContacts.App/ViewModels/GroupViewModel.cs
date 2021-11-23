namespace GoogleContacts.App.ViewModels
{
    using System.ComponentModel;

    using GoogleContacts.App.Models;

    /// <summary>
    /// Вью-модель <see cref="GroupModel"/>
    /// </summary>
    public class GroupViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="Name"/>
        /// </summary>
        private string _name;

        /// <summary>
        /// Вью-модель <see cref="GroupModel"/>
        /// </summary>
        /// <param name="name"> Наименование. </param>
        public GroupViewModel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="Name"/>
        /// </summary>
        public static string NameTitle => "Имя";

        /// <summary>
        /// Заголовок окна. 
        /// </summary>
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