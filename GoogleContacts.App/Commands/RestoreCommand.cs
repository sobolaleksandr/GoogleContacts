namespace GoogleContacts.App.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Команда восстанововления примитива.
    /// </summary>
    public class RestoreCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        /// <summary>
        /// Восстановление примитива.
        /// </summary>
        /// <param name="parameter"> Вызывающий примитив. </param>
        public void Execute(object parameter)
        {
            //TODO
        }
    }
}