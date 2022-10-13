using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WokroutTracker.Presentation.Controllers;
using WorkoutTracker.Application.WorkoutPlans.Queries;
using WorkoutTracker.Domain.Abstractions;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Presentation.Controllers;

namespace WorkoutTracker.UnitTests
{
    public class UnitTest1
    {
        public class WorkoutPlanControllerFixture
        {
            private readonly Mock<IMediator> mediatorStub = new Mock<IMediator>();
            private readonly Mock<IMapper> mapperStub = new Mock<IMapper>();

        [Fact]
        public async Task GetWorkoutPlan_WithNonExistingWorkoutPlan_ReturnsNotFound()
        {
            //Arrange

            mediatorStub.Setup(m => m.Send(It.IsAny<GetWorkoutPlanById>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((WorkoutPlan)null);

            //var controller = new WorkoutPlansController(mediatorStub.Object, mapperStub.Object);

            //Act
            //var result = await controller.GetWorkoutPlanById(1);


            //Assert
            //Assert.IsType<NotFoundResult>(result);

        }
        }
    }
}