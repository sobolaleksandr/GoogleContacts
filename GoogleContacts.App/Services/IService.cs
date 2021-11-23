namespace GoogleContacts.App.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;

    /// <summary>
    /// Сервис для работы с контактами.
    /// </summary>
    /// <typeparam name="T"> Модель контакта. </typeparam>
    public interface IService<in T> where T : ContactModel
    {
        /// <summary>
        /// Создать.
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Модель контакта. </returns>
        Task<ContactModel> CreateAsync(T model);

        /// <summary>
        /// Удалить. 
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Сообщение об ошибке. </returns>
        Task<string> DeleteAsync(T model);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        /// <returns> Список контактов. </returns>
        Task<List<ContactModel>> GetAsync();

        /// <summary>
        /// Обновить контакт. 
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Модель контакта. </returns>
        Task<ContactModel> UpdateAsync(T model);
    }
}