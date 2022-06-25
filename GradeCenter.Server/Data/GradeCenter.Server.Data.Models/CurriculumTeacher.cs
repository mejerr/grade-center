using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeCenter.Server.Data.Common.Models;

namespace GradeCenter.Server.Data.Models
{
    public class CurriculumTeacher : IDeletableEntity, IAuditInfo
    {
        [Required]
        public int CurriculumId { get; set; }

        public virtual Curriculum Curriculum { get; set; }

        [Required]
        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
