using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Tests.Test_Setup
{
	internal static class Directors
	{
		public static void AddDirectors(this IMovieStoreDbContext context)
		{
			context.Directors.AddRange(
				new Director { Name = "Christopher", Surname = "Nolan" }														);
		}
	}
}
