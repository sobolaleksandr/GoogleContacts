﻿namespace GoogleContacts.App.ViewModels
{
    using GoogleContacts.Domain;

    /// <summary>
    /// Базовая вью-модель с результатом диалога.
    /// </summary>
    public abstract class DialogViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Флаг наличия отмены операции.
        /// </summary>
        public bool? DialogResult { get; internal set; }
    }
}