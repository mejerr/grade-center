namespace GradeCenter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Class;

    public class Class : BaseDeletableModel<int>
    {
        public Class()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Curriculums = new HashSet<Curriculum>();
        }

        [Required]
        public int Number { get; set; }

        [Required]
        [MaxLength(DivisionMaxLength)]
        public string Division { get; set; }

        [Required]
        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Curriculum> Curriculums { get; set; }
    }
}
