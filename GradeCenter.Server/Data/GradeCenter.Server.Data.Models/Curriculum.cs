namespace GradeCenter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;

    public class Curriculum : BaseDeletableModel<int>
    {
        public Curriculum()
        {
            this.CurriculumsSubjects = new HashSet<CurriculumSubject>();
        }

        [Required]
        public int ClassId { get; set; }

        public virtual Class Class { get; set; }

        [Required]
        public int Term { get; set; }

        public virtual ICollection<CurriculumSubject> CurriculumsSubjects { get; set; }

        public virtual ICollection<CurriculumTeacher> CurriculumsTeachers { get; set; }
    }
}
