using Microsoft.AspNetCore.Identity;

namespace Maps.src.Maps.Core.Models
{
    public class User : IdentityUser<int>
    {
        public virtual Role UserRole { get; set; }
    }
}
