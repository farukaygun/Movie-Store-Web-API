using AutoMapper;
using FluentAssertions;
using Movie_Store_Web_API.Application.Movie_Operations.Create;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;
using System;
using Unit_Tests.Test_Setup;

namespace Unit_Tests.Application.Genre.Create
{
	public class CreateMovieCommandTests(CommonTestFixture fixture) : IClassFixture<CommonTestFixture>
	{
		private readonly IMovieStoreDbContext context = fixture.Context;
		private readonly IMapper mapper = fixture.Mapper;

		[Fact]
		public void WhenAlreadyExistMovieNameIsGiven_InvalidOperationException_ShouldBeReturned()
		{
			//Arrange
			var movie = new Movie
			{
				Name = "Inception",
				Price = 12,
				Year = 2018,
				Genres = [new Movie_Store_Web_API.Entities.Genre { Name = "Sci-Fi" }],
				Actors = [new Actor { Name = "Leonardo", Surname = "DiCaprio" }],
				Directors = [new Director { Name = "Christopher", Surname = "Nolan" }]
			};

			context.Movies.Add(movie);
			context.SaveChanges();

			var command = new CreateMovieCommand(context, mapper, mapper.Map<CreateMovieModel>(movie));


			FluentActions
				.Invoking(() => command.Handle())
				.Should().ThrowAsync<InvalidOperationException>()
				.WithMessage("Movie already exists!");
		}
	}
}
