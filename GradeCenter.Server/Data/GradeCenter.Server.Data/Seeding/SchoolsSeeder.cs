namespace GradeCenter.Server.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;

    public class SchoolsSeeder : ISeeder
    {
        public async Task SeedAsync(GradeCenterDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Schools.Any())
            {
                return;
            }

            await dbContext.Schools.AddRangeAsync(
                new List<School>
                {
                    new School
                    {
                        Name = "Математическа гимназия „Д-р Петър Берон",
                        Address = "гр. Варна, кв. Чайка, ул. “Академик Никола Обрешков”",
                    },
                    new School
                    {
                        Name = "Първа езикова гимназия",
                        Address = "гр. Варна, кв. Цветен квартал, ул. „Подвис“ 29",
                    },
                });
        }
    }
}
