namespace GoogleContacts.App.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using GoogleContacts.App.Commands;

    /// <summary>
    /// Базовый класс для моделей представления.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Базовый класс для моделей представления.
        /// </summary>
        protected ViewModelBase()
        {
            ApplyCommand = new ApplyCommand();
        }

        /// <summary>
        /// Команда принятия изменений.
        /// </summary>
        public ApplyCommand ApplyCommand { get; set; }

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
    }
}