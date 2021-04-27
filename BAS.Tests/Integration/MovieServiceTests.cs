using BAS.AppServices;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace BAS.Tests.Integration
{
    public class MovieServiceTests : BaseIntegrationTest
    {
        public MovieServiceTests(IntegrationTestsFixture services)
            : base(services)
        {
        }

        [Fact]
        public async Task AddMovie_NoCrewNoGenresNoPhoto_ReturnsTrue()
        {
            try
            {
                var movieService = this.serviceProvider.GetService<IMovieService>();
                var movie = new InsertUpdateMovieDTO()
                {
                    Title = "TestMovieTitle",
                    Description = "TestMovieDescription",
                    ReleaseYear = 2010,
                    MovieLengthInMinutes = 90,
                };

                var result = await movieService.InsertMovie(movie);
                Assert.True(result);
            }
            catch (Exception e)
            {
                logger.Error(e);
                Assert.True(false);
            }
        }

        [Fact]
        public async Task AddMovie_WithPhotoNoCrewNoGenres_ReturnsTrue()
        {
            try
            {
                var movieService = this.serviceProvider.GetService<IMovieService>();

                var stream = new FileStream(StaticValues.TestImagePath, FileMode.Open);
                var formFile = new FormFile(stream, 0, stream.Length, string.Empty,
                    StaticValues.TestImageName);
                var movie = new InsertUpdateMovieDTO()
                {
                    Title = "TestMovieTitle",
                    Description = "TestMovieDescription",
                    ReleaseYear = 2010,
                    MovieLengthInMinutes = 90,
                    File = formFile,
                };

                var result = await movieService.InsertMovie(movie);
                Assert.True(result);
            }
            catch (Exception e)
            {
                logger.Error(e);
                Assert.True(false);
            }
        }
    }
}
