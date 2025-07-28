using Microsoft.AspNetCore.Identity;

namespace Maps.src.Maps.Core.Models
{
    public class Role : IdentityRole<int>
    {
        public virtual List<User> Users { get; set; }
    }
}
