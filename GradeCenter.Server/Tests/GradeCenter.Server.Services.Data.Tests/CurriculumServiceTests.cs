namespace GradeCenter.Server.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GradeCenter.Server.Data;
    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Web.ViewModels.Curriculums;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;

    public class CurriculumServiceTests
    {
        private readonly CurriculumViewModel exampleCurriculum = new CurriculumViewModel()
        {
            Id = 1,
            ClassId = 3,
            Term = 1,
        };

        [Test]
        public async Task Currriculum_GetByIDAsync_Success()
        {
            // Arrange
            var curriculumService = new Mock<ICurriculumService>();

            int curriculumId = 1;

            curriculumService.Setup(x => x.GetByIdAsync<CurriculumViewModel>(this.exampleCurriculum.Id)).Returns(Task.FromResult(this.exampleCurriculum));

            // Act
            var result = await curriculumService.Object.GetByIdAsync<CurriculumViewModel>(curriculumId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(this.exampleCurriculum, result);
            Assert.AreEqual(1, result.Term);
        }

        [Test]
        public async Task Curriculum_GetByTermAndClassIdAsync_Success()
        {
            // Arrange
            var curriculumService = new Mock<ICurriculumService>();

            int term = 1;
            int classId = 3;

            curriculumService.Setup(x => x.GetByTermAndClassIdAsync<CurriculumViewModel>(this.exampleCurriculum.Id, this.exampleCurriculum.ClassId)).Returns(Task.FromResult(this.exampleCurriculum));

            // Act
            var result = await curriculumService.Object.GetByTermAndClassIdAsync<CurriculumViewModel>(term, classId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(this.exampleCurriculum, result);
            Assert.AreEqual(3, result.ClassId);
        }

        [Test]
        public async Task Curriculum_GetByTermAsync_Success()
        {
            // Arrange
            var curriculumService = new Mock<ICurriculumService>();

            int term = 1;

            curriculumService.Setup(x => x.GetByTermAsync<CurriculumViewModel>(this.exampleCurriculum.Term)).Returns(Task.FromResult(this.exampleCurriculum));

            // Act
            var result = await curriculumService.Object.GetByTermAsync<CurriculumViewModel>(term);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(this.exampleCurriculum, result);
            Assert.AreEqual(3, result.ClassId);
        }

        [Test]
        public async Task Curriculum_GetByClassId_Success()
        {
            // Arrange
            var curriculumService = new Mock<ICurriculumService>();

            int classId = 3;

            curriculumService.Setup(x => x.GetByClassIdAsync<CurriculumViewModel>(this.exampleCurriculum.ClassId)).Returns(Task.FromResult(this.exampleCurriculum));

            // Act
            var result = await curriculumService.Object.GetByClassIdAsync<CurriculumViewModel>(classId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(this.exampleCurriculum, result);
            Assert.AreEqual(1, result.Term);
        }

        [Test]
        public async Task Curriculum_GetAllAsync_Success()
        {
            // Arrange
            var curriculumService = new Mock<ICurriculumService>();
            List<CurriculumViewModel> curriculumList = new List<CurriculumViewModel> { this.exampleCurriculum };
            curriculumService.Setup(x => x.GetAllAsync<CurriculumViewModel>()).Returns(Task.FromResult(curriculumList.AsEnumerable()));

            // Act
            var result = await curriculumService.Object.GetAllAsync<CurriculumViewModel>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(curriculumList, result);
        }
    }
}
