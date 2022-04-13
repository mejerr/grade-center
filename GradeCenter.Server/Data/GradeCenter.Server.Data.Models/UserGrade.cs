namespace GradeCenter.Server.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;
    using GradeCenter.Server.Data.Models.Enums;

    public class UserGrade : IDeletableEntity, IAuditInfo
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [Required]
        public DateTime DateOfGrade { get; set; }

        [Required]
        public decimal Grade { get; set; }

        [Required]
        public GradeType GradeType { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
