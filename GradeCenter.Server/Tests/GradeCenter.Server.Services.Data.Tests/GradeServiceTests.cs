namespace GradeCenter.Server.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using GradeCenter.Server.Data.Models;
    using GradeCenter.Server.Data.Models.Enums;
    using Moq;
    using NUnit.Framework;

    public class GradeServiceTests
    {
        private readonly UserGrade exampleUserGrade = new UserGrade()
        {
            UserId = "1",
            SubjectId = 1,
            DateOfGrade = DateTime.UtcNow,
            Grade = 3,
            GradeType = GradeType.Normal,
        };

        [Test]
        public async Task Grade_GetUserGradeAsync_Success()
        {
            // Arrange
            var gradeService = new Mock<IGradeService>();

            int userGradeId = 1;

            gradeService.Setup(x => x.GetUserGradeAsync<UserGrade>(userGradeId)).Returns(Task.FromResult(this.exampleUserGrade));

            // Act
            var result = await gradeService.Object.GetUserGradeAsync<UserGrade>(userGradeId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(this.exampleUserGrade, result);
            Assert.AreEqual(1, result.SubjectId);
        }

        [Test]
        public async Task Grade_RemoveGradeAsync_Success()
        {
            // Arrange
            var gradeService = new Mock<IGradeService>();

            int userGradeId = 1;

            bool removeGrade = true;

            gradeService.Setup(x => x.RemoveGradeAsync(userGradeId)).Returns(Task.FromResult(removeGrade));

            // Act
            await gradeService.Object.AddGradeAsync(this.exampleUserGrade.Grade, this.exampleUserGrade.UserId, this.exampleUserGrade.SubjectId, this.exampleUserGrade.GradeType, this.exampleUserGrade.DateOfGrade);
            var result = await gradeService.Object.RemoveGradeAsync(userGradeId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(true, result);
        }
    }
}
