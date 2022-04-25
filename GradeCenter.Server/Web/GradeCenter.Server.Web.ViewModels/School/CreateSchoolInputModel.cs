namespace GradeCenter.Server.Web.ViewModels.School
{
    using System.ComponentModel.DataAnnotations;

    using static GradeCenter.Server.Common.GlobalConstants.Data.School;

    public class CreateSchoolInputModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }
    }
}
