namespace MinhLam.Framework.Data.Entities
{
    public class RoleUserAccount : BaseEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccountId { get; set; }

    }
}
