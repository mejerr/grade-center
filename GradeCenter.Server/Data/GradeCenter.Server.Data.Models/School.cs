namespace GradeCenter.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Common.Models;

    using static GradeCenter.Server.Common.GlobalConstants.Data.School;

    public class School : BaseDeletableModel<int>
    {
        public School()
        {
            this.Classes = new HashSet<Class>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
