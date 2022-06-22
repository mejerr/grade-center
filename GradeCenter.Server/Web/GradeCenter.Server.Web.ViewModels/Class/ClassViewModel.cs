namespace GradeCenter.Server.Web.ViewModels.Class
{
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Services.Mapping;

    public class ClassViewModel : IMapFrom<Class>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Division { get; set; }

        public int SchoolId { get; set; }
    }
}
