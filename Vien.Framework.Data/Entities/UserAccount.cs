using System;

namespace MinhLam.Framework.Data.Entities
{
    public class UserAccount : BaseEntity
    {
        public int Id { get; set; }
        public string WindowAccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
