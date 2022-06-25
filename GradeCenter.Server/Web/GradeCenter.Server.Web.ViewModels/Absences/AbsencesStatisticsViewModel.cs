namespace GradeCenter.Server.Web.ViewModels.Absences
{
    using System.Collections.Generic;
    using System.Linq;

    using GradeCenter.Server.Data.Models.Enums;

    public class AbsencesStatisticsViewModel
    {
        public IEnumerable<AbsencesStatistics> AbsencesStatistics { get; set; }

        private IEnumerable<AbsencesGroupByUserViewModel> AbsencesGroupedByStudent => this.CalculateMostAbsences(PresenceType.Absent);

        private IEnumerable<AbsencesGroupByUserViewModel> PresencesGroupedByStudent => this.CalculateMostAbsences(PresenceType.Present);

        private IEnumerable<AbsencesGroupByUserViewModel> DelaysGroupedByStudent => this.CalculateMostAbsences(PresenceType.Late);

        public int TotalAbsences => this.AbsencesStatistics.Count(a => a.PresenceType == PresenceType.Absent);

        public int TotalPresences => this.AbsencesStatistics.Count(a => a.PresenceType == PresenceType.Present);

        public int TotalDelays => this.AbsencesStatistics.Count(a => a.PresenceType == PresenceType.Late);

        public int MostAbsences => this.AbsencesGroupedByStudent.Max(a => a.Count);

        public string StudentNameWithMostAbsences => this.AbsencesGroupedByStudent
            .OrderByDescending(a => a.Count)
            .FirstOrDefault()
            ?.StudentName;

        public int MostPresences => this.PresencesGroupedByStudent.Max(a => a.Count);

        public string StudentNameWithMostPresences => this.PresencesGroupedByStudent
            .OrderByDescending(a => a.Count)
            .FirstOrDefault()
            ?.StudentName;

        public int MostDelays => this.DelaysGroupedByStudent.Max(a => a.Count);

        public string StudentNameWithMostDelays => this.DelaysGroupedByStudent
            .OrderByDescending(a => a.Count)
            .FirstOrDefault()
            ?.StudentName;

        private IEnumerable<AbsencesGroupByUserViewModel> CalculateMostAbsences(PresenceType presenceType)
        {
            return this.AbsencesStatistics
                .Where(a => a.PresenceType == presenceType)
                .GroupBy(g => g.StudentName)
                .Select(group => new AbsencesGroupByUserViewModel
                {
                    StudentName = group.Key,
                    Count = group.Count(),
                })
                .ToList();
        }
    }
}
