namespace GradeCenter.Server.Web.ViewModels.Curriculums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Curriculum;

    public class CreateCurriculumInputModel
    {
        public CreateCurriculumInputModel()
        {
            this.Teachers = new List<TeacherInputModel>();
            this.Subjects = new List<SubjectInputModel>();
        }

        [Required]
        [MaxLength(TermLength)]
        public int Term { get; set; }

        [Required]
        [MaxLength(ClassIdLength)]
        public int ClassId { get; set; }

        [Required]
        public List<TeacherInputModel> Teachers { get; set; }

        [Required]
        public List<SubjectInputModel> Subjects { get; set; }
    }
}
