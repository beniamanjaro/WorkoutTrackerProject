using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Infrastructure;

namespace WorkoutTracker.IntegrationTests
    {
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class

    {
        private SqliteConnection _connection;
        public CustomWebApplicationFactory()
        {
            _connection = new SqliteConnection("DataSource =:memory:");
            _connection.Open();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<WorkoutContext>));

                services.Remove(descriptor);

                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlite()
                .BuildServiceProvider();

                services.AddDbContext<WorkoutContext>(options =>
                {
                    options.UseSqlite(_connection);
                    options.UseInternalServiceProvider(serviceProvider);
                }, ServiceLifetime.Scoped);

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<WorkoutContext>();
                    db.Database.EnsureDeleted();

                    db.Database.EnsureCreated();

                    /*Utilities.InitializeDbForTests(db);*/
                }
            });
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Dispose();
            _connection.Close();
        }


    }
}
