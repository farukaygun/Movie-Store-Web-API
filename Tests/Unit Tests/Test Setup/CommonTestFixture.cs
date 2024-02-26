using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Common;
using Movie_Store_Web_API.Db_Operations;

namespace Unit_Tests.Test_Setup
{
	public class CommonTestFixture
	{
		public IMovieStoreDbContext Context { get; set; }
		public IMapper Mapper { get; set; }

		public CommonTestFixture()
		{
			var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
				.UseInMemoryDatabase("MovieStoreTestDB")
				.Options;
			Context = new MovieStoreDbContext(options);
			Context.Database.EnsureCreated();
			Context.AddGenres();
			Context.SaveChanges();

			Mapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutoMapperProfile>();
			}).CreateMapper();
		}
	}
}
