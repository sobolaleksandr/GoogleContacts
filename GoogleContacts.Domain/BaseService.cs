namespace GoogleContacts.Domain
{
    using System;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.PeopleService.v1;
    using Google.Apis.Services;

    public abstract class BaseService : IDisposable
    {
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";
        protected readonly PeopleServiceService Service;

        private bool _disposed;

        protected BaseService()
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

            Service = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleContacts",
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            Service?.Dispose();
            _disposed = true;
        }

        ~BaseService()
        {
            Dispose(false);
        }
    }
}