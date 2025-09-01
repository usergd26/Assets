using Microsoft.AspNetCore.Identity;

namespace Assets.Domain.Entities
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
