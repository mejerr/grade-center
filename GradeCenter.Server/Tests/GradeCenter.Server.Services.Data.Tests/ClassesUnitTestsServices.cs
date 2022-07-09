namespace GradeCenter.Server.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data;
    using GradeCenter.Server.Web.ViewModels.Class;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;

    public class ClassesUnitTestsServices
    {
        private readonly ClassViewModel exampleClass = new ClassViewModel()
        {
            Id = 1,
            SchoolId = 1,
            Number = 10,
            Division = "A",
        };

        [Test]
        public async Task Class_GetByIDAsync_Success()
        {
            // Arrange
            var classService = new Mock<IClassService>();

            int classId = 1;

            classService.Setup(x => x.GetByIdAsync<ClassViewModel>(this.exampleClass.Id)).Returns(Task.FromResult(this.exampleClass));

            // Act
            var result = await classService.Object.GetByIdAsync<ClassViewModel>(classId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(this.exampleClass, result);
            Assert.AreEqual(10, result.Number);
        }

        [Test]
        public async Task Class_GetByNumberAndDivisionAsync_Success()
        {
            // Arrange
            var classService = new Mock<IClassService>();
            int classNumber = 10;
            int classSchoolId = 1;
            string classDivision = "A";

            classService.Setup(x => x.GetByNumberAndDivisionAsync<ClassViewModel>(this.exampleClass.Number, this.exampleClass.Division, this.exampleClass.SchoolId)).Returns(Task.FromResult(this.exampleClass));

            // Act
            var result = await classService.Object.GetByNumberAndDivisionAsync<ClassViewModel>(classNumber, classDivision, classSchoolId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(this.exampleClass, result);
            Assert.AreEqual(1, result.SchoolId);
        }

        [Test]
        public async Task Class_GetByNumberAsync_Success()
        {
            // Arrange
            var classService = new Mock<IClassService>();
            int classNumber = 10;
            int classSchoolId = 1;

            classService.Setup(x => x.GetByNumberAsync<ClassViewModel>(this.exampleClass.Number, this.exampleClass.SchoolId)).Returns(Task.FromResult(this.exampleClass));

            // Act
            var result = await classService.Object.GetByNumberAsync<ClassViewModel>(classNumber, classSchoolId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(this.exampleClass, result);
            Assert.AreEqual(10, result.Number);
        }

        [Test]
        public async Task Class_GetBySchoolIdAsync_Success()
        {
            // Arrange
            var classService = new Mock<IClassService>();
            int classSchoolId = 1;

            classService.Setup(x => x.GetBySchoolIdAsync<ClassViewModel>(this.exampleClass.SchoolId)).Returns(Task.FromResult(this.exampleClass));

            // Act
            var result = await classService.Object.GetBySchoolIdAsync<ClassViewModel>(classSchoolId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(this.exampleClass, result);
            Assert.AreEqual(1, result.SchoolId);
        }

        [Test]
        public async Task Class_GetAllAsync_Success()
        {
            // Arrange
            var classService = new Mock<IClassService>();
            List<ClassViewModel> classList = new List<ClassViewModel> { this.exampleClass };
            classService.Setup(x => x.GetAllAsync<ClassViewModel>()).Returns(Task.FromResult(classList.AsEnumerable()));

            // Act
            var result = await classService.Object.GetAllAsync<ClassViewModel>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(classList, result);
        }

        [Test]
        [Order(1)]
        public async Task Class_CreateAsync_Success()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<GradeCenterDbContext>();
            builder.UseInMemoryDatabase("GradeCenterDbContextTest");
            var options = builder.Options;
            using (var context = new GradeCenterDbContext(options))
            {
                // var classService = new Mock<IClassService>();
                var classService = new ClassService(context);

                // Act
                var result = await classService.CreateAsync(this.exampleClass.Number, this.exampleClass.Division, this.exampleClass.SchoolId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(this.exampleClass.Id, result);
                Assert.AreEqual(1, result);
            }
        }

        [Test]
        [Order(2)]
        public async Task Class_UpdateAsync_Success()
        {
            // Arrange
            var updateNumber = 11;
            var builder = new DbContextOptionsBuilder<GradeCenterDbContext>();
            builder.UseInMemoryDatabase("GradeCenterDbContextTest");
            var options = builder.Options;
            using (var context = new GradeCenterDbContext(options))
            {
                var classService = new ClassService(context);

                // Act
                var update = await classService.UpdateAsync(this.exampleClass.Id, updateNumber, this.exampleClass.Division);

                // Assert
                Assert.IsNotNull(update);
                Assert.IsTrue(update);
            }
        }

        [Test]
        [Order(3)]
        public async Task Class_DeleteAsync_Success()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<GradeCenterDbContext>();
            builder.UseInMemoryDatabase("GradeCenterDbContextTest");
            var options = builder.Options;
            using (var contextDelete = new GradeCenterDbContext(options))
            {
                var classService = new ClassService(contextDelete);

                // Act
                var result = await classService.DeleteAsync(this.exampleClass.Id);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result);
            }
        }
    }
}
