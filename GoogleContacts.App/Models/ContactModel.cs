namespace GoogleContacts.App.Models
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Базовая модель контакта.
    /// </summary>
    public class ContactModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Поле свойства <see cref="Name"/>
        /// </summary>
        private string _name;

        /// <summary>
        /// Идентификатор модели.
        /// </summary>
        protected string ETag;

        /// <summary>
        /// Базовая модель контакта.
        /// </summary>
        /// <param name="error"></param>
        public ContactModel(string error)
        {
            Error = error;
            Contacts = new ObservableCollection<ContactModel>();
        }

        /// <summary>
        /// Список контактов.
        /// </summary>
        public ObservableCollection<ContactModel> Contacts { get; set; }

        /// <summary>
        /// Ошибка модели.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Модель выбрана.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Наименование модели.
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
        /// Наименование ресурса.
        /// </summary>
        public string ResourceName { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод генерации события при изменении определенного свойства.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}