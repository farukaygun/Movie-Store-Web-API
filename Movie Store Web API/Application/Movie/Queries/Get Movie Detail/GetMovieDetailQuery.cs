﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Db_Operations;

namespace Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movie_Detail
{
	public class GetMovieDetailQuery(IMovieStoreDbContext context,
		IMapper mapper,
		int id)
	{
		public int Id { get; set; } = id;

		public async Task<GetMovieModel> Handle()
		{
			var movie = await context.Movies
				.Include(m => m.Genres)
				.Include(m => m.Actors)
				.Include(m => m.Directors)
				.SingleOrDefaultAsync(m => m.Id == Id);

			return movie is not null ? mapper.Map<GetMovieModel>(movie) : throw new InvalidOperationException("Movie not found!");
		}
	}
}
