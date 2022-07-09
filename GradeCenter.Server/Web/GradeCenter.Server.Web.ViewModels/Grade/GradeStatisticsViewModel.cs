namespace GradeCenter.Server.Web.ViewModels.Grade
{
    using System.Collections.Generic;
    using System.Linq;

    public class GradeStatisticsViewModel
    {
        public IEnumerable<GradeStatistics> GradeStatistics { get; set; }

        public decimal MaxGrade => this.GradeStatistics.Max(g => g.Grade);

        public decimal MinGrade => this.GradeStatistics.Min(g => g.Grade);

        public decimal AverageGrade => this.GradeStatistics.Average(g => g.Grade);
    }
}
