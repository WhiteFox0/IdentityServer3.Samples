using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using JsApp.IdentityServer.Settings;
using Owin;
using Serilog;

namespace JsApp.IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .WriteTo.LiterateConsole()
                .CreateLogger();


            app.UseIdentityServer(new IdentityServerOptions()
            {
                SiteName = "js identity",
                SigningCertificate = LoadCertificate(),

                Factory = new IdentityServerServiceFactory()
                                .UseInMemoryClients(Clients.Get)
                                .UseInMemoryScopes(Scopes.Get)
                                .UseInMemoryUsers(Users.Get)
            });

        }

        private X509Certificate2 LoadCertificate()
        {
            var certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"bin\identityServer\idsrv3test.pfx");
            return new X509Certificate2(certPath, "idsrv3test");
        }
    }
}
