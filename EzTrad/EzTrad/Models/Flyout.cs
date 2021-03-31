using System;

namespace EzTrad.Models
{
    public class Flyout
    {
        public string Title { get; set; }
        public Type TargetPage { get; set; }
        public string Icon { get; set; }
        public bool IsHeader { get; set; }
        public bool IsTicked { get; set; }
    }
}
