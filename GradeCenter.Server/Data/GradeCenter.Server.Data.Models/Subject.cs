namespace GradeCenter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Subject;

    public class Subject : BaseDeletableModel<int>
    {
        public Subject()
        {
            this.CurriculumsSubjects = new HashSet<CurriculumSubject>();
            this.UsersGrades = new HashSet<UserGrade>();
            this.UsersSubjects = new HashSet<UserSubject>();
            this.UsersPresences = new HashSet<UserPresence>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public virtual ICollection<CurriculumSubject> CurriculumsSubjects { get; set; }

        public virtual ICollection<UserGrade> UsersGrades { get; set; }

        public virtual ICollection<UserSubject> UsersSubjects { get; set; }

        public virtual ICollection<UserPresence> UsersPresences { get; set; }
    }
}
