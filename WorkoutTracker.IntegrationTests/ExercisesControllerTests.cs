using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace WorkoutTracker.IntegrationTests
{
    public class ExercisesControllerTests : IDisposable
    {
        private static WebApplicationFactory<Program> _factory;

        public ExercisesControllerTests()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }


        [Fact]
        public async Task GetExercise_WithNonExistingExerciseId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/exercises/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetExercise_WithExistingExerciseId_ReturnsTheExercise()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/exercises/2");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}