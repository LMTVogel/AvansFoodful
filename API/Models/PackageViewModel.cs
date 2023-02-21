using Core.Domain;

namespace API.Models
{
    public class PackageViewModel
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public Package? Package { get; set; }
    }
}
