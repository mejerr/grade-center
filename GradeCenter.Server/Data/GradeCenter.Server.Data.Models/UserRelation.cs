namespace GradeCenter.Server.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;

    public class UserRelation : IDeletableEntity, IAuditInfo
    {
        [Required]
        public string UserInferiorId { get; set; }

        [Required]
        public virtual ApplicationUser UserInferior { get; set; }

        [Required]
        public string UserSuperiorId { get; set; }

        [Required]
        public virtual ApplicationUser UserSuperior { get; set; }

        [Required]
        public string UserRoleId { get; set; }

        public virtual ApplicationRole UserRole { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
