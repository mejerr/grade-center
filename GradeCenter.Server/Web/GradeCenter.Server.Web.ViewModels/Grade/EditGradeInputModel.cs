namespace GradeCenter.Server.Web.ViewModels.Grade
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Models.Enums;

    public class EditGradeInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime DateOfGrade { get; set; }

        [Required]
        public decimal Grade { get; set; }

        [Required]
        public GradeType GradeType { get; set; }
    }
}
