namespace GoogleContacts.App
{
    using System.Collections.Generic;
    using System.Reflection;

    using Ninject;

    /// <summary>
    /// Статичное ядро Ninject.
    /// </summary>
    public static class NinjectKernel
    {
        /// <summary>
        /// Ядро привязок.
        /// </summary>
        private static readonly StandardKernel _kernelInstance;

        /// <summary>
        /// Список сборок, из которых уже загружены модули.
        /// </summary>
        private static readonly HashSet<string> _loadedAssemblies;

        /// <summary>
        /// Инициализирует статические поля класса <see cref="NinjectKernel"/>. 
        /// Конструктор ядра, в котором инициализируется свойство Instance и загружаются модули.
        /// </summary>
        static NinjectKernel()
        {
            _kernelInstance = new StandardKernel();
            _loadedAssemblies = new HashSet<string>();
        }

        /// <summary>
        /// Статичное ядро Ninject, описывающее все критичные правила создания классов.
        /// </summary>
        public static StandardKernel KernelInstance
        {
            get
            {
                var assembly = Assembly.GetCallingAssembly();

                // Если вызывающая сборка уже была проверена на модули привязок.
                if (_loadedAssemblies.Contains(assembly.FullName))
                    return _kernelInstance;

                // Из указанной сборки подгружает все INinjectModule с модификатором public, 
                // таким образом осуществляется автоматическая привязка
                _kernelInstance.Load(assembly);
                _loadedAssemblies.Add(assembly.FullName);

                return _kernelInstance;
            }
        }

        /// <summary>
        /// Получить текущее объявление службы T1.
        /// </summary>
        /// <typeparam name="T">Тип службы.</typeparam>
        /// <returns>Возвращает текущее объявление службы.</returns>
        public static T Get<T>()
        {
            return KernelInstance.Get<T>();
        }

        /// <summary>
        /// Удаляет все существующие привязки для службы типов T1 и объявляет новую T2.
        /// </summary>
        /// <typeparam name="T1">Тип текущей служба.</typeparam>
        /// <typeparam name="T2">Новое объявление текущей службы.</typeparam>
        /// <param name="constructorParameters">Параметры конструктора.</param>
        public static void Rebind<T1, T2>(params KeyValuePair<string, object>[] constructorParameters)
            where T2 : T1
        {
            Rebind<T1, T2>(false, constructorParameters);
        }

        /// <summary>
        /// Выполняет привязку для синглтона.
        /// </summary>
        /// <typeparam name="T1">Исходный тип (интерфейс).</typeparam>
        /// <typeparam name="T2">Привязываемый.</typeparam>
        /// <param name="constructorParameters">Параметры конструктора.</param>
        public static void RebindSingleton<T1, T2>(params KeyValuePair<string, object>[] constructorParameters)
            where T2 : T1
        {
            Rebind<T1, T2>(true, constructorParameters);
        }

        /// <summary>
        /// Отвязка.
        /// </summary>
        /// <typeparam name="T">Отвязываемый тип.</typeparam>
        public static void Unbind<T>()
        {
            KernelInstance.Unbind<T>();
        }

        /// <summary>
        /// Удаляет все существующие привязки для службы типов T1 и объявляет новую T2.
        /// </summary>
        /// <typeparam name="T1">Тип текущей служба.</typeparam>
        /// <typeparam name="T2">Новое объявление текущей службы.</typeparam>
        /// <param name="isSingleton">True - для привязки синглтона.</param>
        /// <param name="constructorParameters">Параметры конструктора.</param>
        private static void Rebind<T1, T2>(bool isSingleton, params KeyValuePair<string, object>[] constructorParameters)
            where T2 : T1
        {
            var binding = KernelInstance.Rebind<T1>().To<T2>();
            if (isSingleton)
                binding.InSingletonScope();

            foreach (var constructorParameter in constructorParameters)
            {
                binding.WithConstructorArgument(constructorParameter.Key, constructorParameter.Value);
            }
        }
    }
}