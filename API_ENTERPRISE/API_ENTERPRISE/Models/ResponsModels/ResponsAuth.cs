
namespace API_ENTERPRISE.Models.ResponsModels
{
    public class ResponsAuth
    {
        public bool success { get; set; }
        public string token { get; set; }
        public AuthUser user { get; set; }

    }
}
