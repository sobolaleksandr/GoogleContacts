namespace GoogleContacts.Domain
{
    using System;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.PeopleService.v1;
    using Google.Apis.Services;

    public class UnitOfWork : IDisposable
    {
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";
        private readonly PeopleServiceService _service;

        private bool _disposed;

        public UnitOfWork(bool isDebug)
        {
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = M_CLIENT_ID,
                    ClientSecret = M_CLIENT_SECRET
                },
                new[] { "https://www.googleapis.com/auth/contacts" },
                "user",
                CancellationToken.None).Result;

            _service = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleContacts",
            });

            CreateServices(isDebug);
        }

        public IService<GroupModel> GroupService { get; private set; }

        public IService<PersonModel> PeopleService { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void CreateServices(in bool isDebug)
        {
            if (isDebug)
            {
                PeopleService = new PeopleServiceMock();
                GroupService = new GroupServiceMock();
                return;
            }

            PeopleService = new PeopleService(_service);
            GroupService = new GroupService(_service);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _service?.Dispose();
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}