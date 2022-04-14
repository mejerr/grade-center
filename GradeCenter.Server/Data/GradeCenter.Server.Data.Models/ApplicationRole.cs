// ReSharper disable VirtualMemberCallInConstructor

namespace GradeCenter.Server.Data.Models
{
    using System;
    using System.Collections.Generic;

    using GradeCenter.Server.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.UsersRelations = new HashSet<UserRelation>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<UserRelation> UsersRelations { get; set; }
    }
}
