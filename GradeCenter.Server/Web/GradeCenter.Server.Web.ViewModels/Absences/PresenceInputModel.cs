namespace GradeCenter.Server.Web.ViewModels.Absences
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GradeCenter.Server.Data.Models.Enums;

    public class PresenceInputModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public DateTime DateOfClass { get; set; }

        [Required]
        public PresenceType PresenceType { get; set; }
    }
}
