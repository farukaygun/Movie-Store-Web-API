using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Director_Detail
{
	public class GetDirectorDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int id)
	{
		public int Id { get; set; } = id;

		public async Task<GetDirectorModel> Handle()
		{
			var director = await context.Directors
				.SingleOrDefaultAsync(x => x.Id == Id);

			return director is not null ? mapper.Map<GetDirectorModel>(director) : throw new InvalidOperationException("Director not found!");
		}
	}   
}