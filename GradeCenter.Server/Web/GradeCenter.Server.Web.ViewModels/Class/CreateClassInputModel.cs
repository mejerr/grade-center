namespace GradeCenter.Server.Web.ViewModels.Class
{
    using System.ComponentModel.DataAnnotations;

    using static GradeCenter.Server.Common.GlobalConstants.Data.Class;

    public class CreateClassInputModel
    {
        [Required]
        public int Number { get; set; }

        [Required]
        [MaxLength(DivisionMaxLength)]
        public string Division { get; set; }

        [Required]
        public int SchoolId { get; set; }
    }
}
