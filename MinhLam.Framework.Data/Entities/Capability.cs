namespace MinhLam.Framework.Data.Entities
{
    public class Capability : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MenuItemId { get; set; }
        public byte AccessType { get; set; }
    }
}
