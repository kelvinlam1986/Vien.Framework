using System;

namespace Vien.Framework.Data.Entities
{
    public class MenuItem : BaseEntity
    {
        public int Id { get; set; }
        public string MenuItemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int? ParentMenuItemId { get; set; }
        public short DisplaySequence { get; set; }
        public bool IsAlwaysEnabled { get; set; }
        public string Icon { get; set; }
    }
}
