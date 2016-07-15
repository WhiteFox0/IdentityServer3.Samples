

using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace JsApp.IdentityServer.Settings
{
    public static class Users
    {
        public static List<InMemoryUser> Get { get;  }
            = new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "bob",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "bob.smith@email.com")
                    }
                }
            };
            
        
    }
}
