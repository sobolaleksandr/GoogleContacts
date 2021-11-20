namespace GoogleContacts.Domain
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Базовый класс для моделей представления.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase()
        {
            ApplyCommand = new ApplyCommand();
        }

        /// <summary>
        /// Поле свойства <see cref="IsChanged"/>.
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Примитив изменен.
        /// </summary>
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Событие, генерируемое при изменении свойств.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод генерации события при изменении определенного свойства.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ApplyCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Команда принятия изменений примитива.
        /// </summary>
        public ApplyCommand ApplyCommand { get; set; }

        /// <summary>
        /// Валидация значений с плавающей точкой.
        /// </summary>
        /// <param name="value"> Значение с плавающей точкой. </param>
        /// <param name="title"> Наименование атрибута. </param>
        /// <returns> Пустую строку в случае, если валидация пройдена. </returns>
        protected string ValidateDouble(string value, string title)
        {
            return !double.TryParse(value, out _) ? $@"В поле '{title}' должно быть число!" : string.Empty;
        }
    }
}