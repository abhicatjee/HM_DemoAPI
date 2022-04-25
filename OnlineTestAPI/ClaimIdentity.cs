using System.Collections.Generic;
using System.Security.Claims;

namespace OnlineTestAPI
{
    internal class ClaimIdentity : ClaimsIdentity
    {
        public ClaimIdentity(IEnumerable<Claim> claims) : base(claims)
        {
        }
    }
}