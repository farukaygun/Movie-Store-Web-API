using AutoMapper;
using Movie_Store_Web_API.Application.Actor_Operations.Create;
using Movie_Store_Web_API.Application.Director_Operations.Create;
using Movie_Store_Web_API.Application.Genre_Operations.Create;
using Movie_Store_Web_API.Application.Movie_Operations.Create;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Common
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Movie, CreateMovieModel>().ReverseMap();
			CreateMap<Movie, GetMovieModel>().ReverseMap();

			CreateMap<Genre, CreateGenreModel>().ReverseMap();
			CreateMap<Actor, CreateActorModel>().ReverseMap();
			CreateMap<Director, CreateDirectorModel>().ReverseMap();
		}
	}
}
