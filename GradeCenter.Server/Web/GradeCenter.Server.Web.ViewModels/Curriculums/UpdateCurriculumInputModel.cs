namespace GradeCenter.Server.Web.ViewModels.Curriculums
{
    using System.ComponentModel.DataAnnotations;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Curriculum;

    public class UpdateCurriculumInputModel
    {
        [Required]
        [MaxLength(TermLength)]
        public int Term { get; set; }

        [Required]
        [MaxLength(ClassIdLength)]
        public int ClassId { get; set; }
    }
}
