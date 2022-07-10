namespace GradeCenter.Server.Web.ViewModels.Absences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models.Enums;

    public class AbsencesStatistics
    {
        public string SubjectName { get; set; }

        public string StudentName { get; set; }

        public string TeacherName { get; set; }

        public DateTime DateOfClass { get; set; }

        public PresenceType PresenceType { get; set; }
    }
}
