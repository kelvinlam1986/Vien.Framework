namespace MinhLam.Framework.Data.Entities
{
    public class RoleCapability : BaseEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int CapabilityId { get; set; }
        public byte AccessFlag { get; set; }
    }
}
