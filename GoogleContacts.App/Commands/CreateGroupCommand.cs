namespace GoogleContacts.App.Commands
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using GoogleContacts.App.Models;
    using GoogleContacts.App.Services;
    using GoogleContacts.App.ViewModels;
    using GoogleContacts.App.Views;

    /// <summary>
    /// Команда создания группы.
    /// </summary>
    public class CreateGroupCommand : BaseCommand
    {
        /// <summary>
        /// Команда создания группы.
        /// </summary>
        /// <param name="people"> Контакты. </param>
        /// <param name="groups"> Группы контактов. </param>
        /// <param name="unitOfWork"> Единица работы. </param>
        /// <param name="updateFunction"> Функция обновления UI. </param>
        public CreateGroupCommand(ObservableCollection<ContactModel> people,
            ObservableCollection<ContactModel> groups, UnitOfWork unitOfWork, Func<Task> updateFunction) : base(
            people, groups, unitOfWork, updateFunction)
        {
        }

        public override async void Execute(object parameter)
        {
            var vm = new GroupViewModel(string.Empty);
            var window = new EditGroupView
            {
                DataContext = vm
            };

            if (window.ShowDialog() != true)
                return;

            var groupModel = new GroupModel(vm.Name, string.Empty);
            var result = await GroupService.CreateAsync(groupModel);
            UpdateAsync(result);
        }
    }
}