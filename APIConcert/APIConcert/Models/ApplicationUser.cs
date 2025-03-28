using Microsoft.AspNetCore.Identity;

namespace APIConcert.Models {
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
