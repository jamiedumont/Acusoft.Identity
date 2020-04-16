using Microsoft.AspNetCore.Identity;

namespace Acusoft.Identity.Client.Models
{
    public class GrpcErrorDescriber
    {
        public virtual IdentityError DefaultError(string message)
        {
            return new IdentityError()
            {
                Code = "DefaultError",
                Description = message
            };
        }
    }
}
