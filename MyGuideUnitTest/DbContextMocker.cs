using IntelligentTourGuide.Web.Data;
using IntelligentTourGuide.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGuideUnitTest
{
    public static class DbContextMocker
    {
        // NOTE: 
        //     Since all tests run parallelly,
        //     ensure that each test method gets its own locally scoped InMemory database

        public static ApplicationDbContext GetApplicationDbContext(string databasename)
        {
            // Create a fresh service provider for the InMemory Database instance.
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance,
            // telling the context to use InMemory database and the new service provider.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databasename)
                            .UseInternalServiceProvider(serviceProvider)
                            .Options;

            // Create the instance of the DbContext (would be an InMemory database)
            // NOTE: It will use the Scema as defined in the Data and Models folders
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the inmemory database
            dbContext.SeedData();

            return dbContext;
        }

        internal static readonly State[] TestData_States
            = {
                new State
                {
                    StateId = 1,
                    StateName = "First State"
                },
                new State
                {
                    StateId = 2,
                    StateName = "Second State"
                },
                new State
                {
                    StateId = 3,
                    StateName = "Third State"
                }
            };

        /// <summary>
        ///     An extension Method for the DbContext object.
        /// </summary>
        /// <param name="context"></param>
        private static void SeedData(this ApplicationDbContext context)
        {
            context.States.AddRange(TestData_States);

            context.SaveChanges();
        }
    }
}
