using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;

namespace JsApp.IdentityServer.Settings
{
    public class Scopes
    {
        public static List<Scope> Get { get; }
                = new List<Scope>
                {
                    StandardScopes.OpenId,
                    StandardScopes.Profile,
                    StandardScopes.Email,

                    new Scope()
                    {
                        Name = "api",

                        DisplayName = "my api",
                        Description = "test",

                        ScopeSecrets = new List<Secret>()
                        {
                            new Secret("api-secret".Sha256())
                        },

                        Type = ScopeType.Resource
                    }
                };
    }
}
