namespace GradeCenter.Server.Web.ViewModels.Subjects
{
    using System.ComponentModel.DataAnnotations;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Subject;

    public class CreateSubjectInputModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
