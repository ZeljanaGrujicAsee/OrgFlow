using OegFlow.Domain.Entities;
using OrgFlow.Domain.Entities;

namespace OrgFlow.Domain.Entites
{
    public class Organization : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<OfficeLocation> OfficeLocations { get; set; } = new List<OfficeLocation>();
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
