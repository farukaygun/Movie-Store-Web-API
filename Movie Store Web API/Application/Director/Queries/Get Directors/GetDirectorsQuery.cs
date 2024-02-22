using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors
{
	public class GetDirectorsQuery(IMovieStoreDbContext context,
		IMapper mapper)
	{
		public async Task<List<GetDirectorModel>> Handle()
		{
			var directors = await context.Directors
				.OrderBy(d => d.Id)
				.ToListAsync();

			return mapper.Map<List<GetDirectorModel>>(directors);
		}
	}

	public class GetDirectorModel
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public ICollection<GetMovieModel>? Movies { get; set; }
	}
}