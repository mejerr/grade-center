namespace GradeCenter.Server.Web.ViewModels.Absences
{
    using System.Collections.Generic;
    using System.Linq;

    using GradeCenter.Server.Data.Models.Enums;

    public class AbsencesStatisticsViewModel
    {
        public IEnumerable<AbsencesStatistics> AbsencesStatistics { get; set; }

        public int TotalAbsences => this.AbsencesStatistics.Count(a => a.PresenceType == PresenceType.Absent);

        public int TotalPresences => this.AbsencesStatistics.Count(a => a.PresenceType == PresenceType.Present);

        public int TotalDelays => this.AbsencesStatistics.Count(a => a.PresenceType == PresenceType.Late);

        public int MostAbsences => this.GetAbsencesGroupedByStudent().Count() > 0
            ? this.GetAbsencesGroupedByStudent().Max(a => a.Count)
            : 0;

        public string StudentNameWithMostAbsences => this.GetAbsencesGroupedByStudent()
            .OrderByDescending(a => a.Count)
            .FirstOrDefault()
            ?.StudentName;

        public int MostPresences => this.GetPresencesGroupedByStudent().Count() > 0
            ? this.GetPresencesGroupedByStudent().Max(a => a.Count)
            : 0;

        public string StudentNameWithMostPresences => this.GetPresencesGroupedByStudent()
            .OrderByDescending(a => a.Count)
            .FirstOrDefault()
            ?.StudentName;

        public int MostDelays => this.GetDelaysGroupedByStudent().Count() > 0
            ? this.GetDelaysGroupedByStudent().Max(a => a.Count)
            : 0;

        public string StudentNameWithMostDelays => this.GetDelaysGroupedByStudent()
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

        private IEnumerable<AbsencesGroupByUserViewModel> GetAbsencesGroupedByStudent()
        {
            return this.CalculateMostAbsences(PresenceType.Absent);
        }

        private IEnumerable<AbsencesGroupByUserViewModel> GetPresencesGroupedByStudent()
        {
            return this.CalculateMostAbsences(PresenceType.Present);
        }

        private IEnumerable<AbsencesGroupByUserViewModel> GetDelaysGroupedByStudent()
        {
            return this.CalculateMostAbsences(PresenceType.Late);
        }
    }
}
