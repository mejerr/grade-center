namespace GradeCenter.Server.Web.ViewModels.Grade
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Models.Enums;

    public class EditGradeInputModel
    {
        [Required]
        public int Id { get; set; }

        public DateTime DateOfGrade { get; set; }

        public decimal Grade { get; set; }

        public GradeType GradeType { get; set; }
    }
}
