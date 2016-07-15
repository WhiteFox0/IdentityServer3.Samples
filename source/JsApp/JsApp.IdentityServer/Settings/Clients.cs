using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;

namespace JsApp.IdentityServer.Settings
{
    public class Clients
    {
        public static IEnumerable<Client> Get { get;  } 
                = new[] {
                new Client
                {
                    Enabled = true,
                    ClientName = "JS Client",
                    ClientId = "js",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44301/popup.html",
                        // The new page is a valid redirect page after login
                        "https://localhost:44301/silent-renew.html"
                    },
                     // Valid URLs after logging out
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44301/index.html"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44301"
                    },

                    AllowAccessToAllScopes = true,
                    AccessTokenLifetime = 70
                }
            };
        
    }
}
