namespace GradeCenter.Server.Web.ViewModels.Curriculums
{
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;

    public class CurriculumViewModel : IMapFrom<Curriculum>
    {
        public int Id { get; set; }

        public int Term { get; set; }

        public int ClassId { get; set; }
    }
}
