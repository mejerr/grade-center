namespace GradeCenter.Server.Web.ViewModels.Grade
{
    using System;

    using GradeCenter.Server.Data.Models.Enums;

    public class GradeStatistics
    {
        public decimal Grade { get; set; }

        public string SubjectName { get; set; }

        public string StudentName { get; set; }

        public string TeacherName { get; set; }

        public DateTime DateOfGrade { get; set; }

        public GradeType GradeType { get; set; }
    }
}
