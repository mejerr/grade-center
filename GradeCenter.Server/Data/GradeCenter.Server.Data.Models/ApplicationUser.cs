namespace GradeCenter.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    using static GradeCenter.Server.Common.GlobalConstants.Data.User;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.UsersInferiorRelations = new HashSet<UserRelation>();
            this.UsersSuperiorRelations = new HashSet<UserRelation>();
            this.UsersGrades = new HashSet<UserGrade>();
            this.UsersSubjects = new HashSet<UserSubject>();
            this.UsersPresences = new HashSet<UserPresence>();
        }

        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int? ClassId { get; set; }

        public virtual Class Class { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<UserRelation> UsersInferiorRelations { get; set; }

        public virtual ICollection<UserRelation> UsersSuperiorRelations { get; set; }

        public virtual ICollection<UserGrade> UsersGrades { get; set; }

        public virtual ICollection<UserSubject> UsersSubjects { get; set; }

        public virtual ICollection<UserPresence> UsersPresences { get; set; }
    }
}
