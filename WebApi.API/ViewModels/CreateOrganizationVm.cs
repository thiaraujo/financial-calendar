using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace WebApi.API.ViewModels
{
    public class CreateOrganizationVm
    {
        public Identification Identification { get; set; }
        public EOrganizationType OrganizationType { get; set; }
        public string Image { get; set; }
    }
}
