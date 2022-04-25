namespace GradeCenter.Server.Web.ViewModels.School
{
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;

    public class SchoolViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
