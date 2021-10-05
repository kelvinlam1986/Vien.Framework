using System;

namespace Vien.Framework.Data.Entities
{
    public class UserAccount : BaseEntity
    {
        public string WindowAccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
