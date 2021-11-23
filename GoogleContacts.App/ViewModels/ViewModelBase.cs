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
        protected ViewModelBase()
        {
            ApplyCommand = new ApplyCommand();
        }

        /// <summary>
        /// Команда принятия изменений примитива.
        /// </summary>
        public ApplyCommand ApplyCommand { get; set; }

        /// <summary>
        /// Событие, генерируемое при изменении свойств.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод генерации события при изменении определенного свойства.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ApplyCommand.RaiseCanExecuteChanged();
        }
    }
}