namespace GradeCenter.Server.Web.ViewModels.Grade
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Models.Enums;

    public class GradeInputModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public DateTime DateOfGrade { get; set; }

        [Required]
        public decimal Grade { get; set; }

        [Required]
        public GradeType GradeType { get; set; }
    }
}
