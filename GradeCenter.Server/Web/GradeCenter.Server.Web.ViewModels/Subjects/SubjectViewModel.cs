namespace GradeCenter.Server.Web.ViewModels.Subjects
{
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;

    public class SubjectViewModel : IMapFrom<UserSubject>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
