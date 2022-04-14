namespace GradeCenter.Server.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;
    using GradeCenter.Server.Data.Models.Enums;

    public class UserPresence : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [Required]
        public DateTime DateOfClass { get; set; }

        [Required]
        public PresenceType PresenceType { get; set; }
    }
}
