using AutoMapper;
using Movie_Store_Web_API.Application.Actor_Operations.Create;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors;
using Movie_Store_Web_API.Application.Actor_Operations.Update;
using Movie_Store_Web_API.Application.Director_Operations.Create;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors;
using Movie_Store_Web_API.Application.Genre_Operations.Create;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genres;
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

			CreateMap<Genre, GetGenreModel>().ReverseMap();
			CreateMap<Actor, GetActorModel>().ReverseMap();
			CreateMap<Director, GetDirectorModel>().ReverseMap();

			CreateMap<Actor, PatchActorModel>().ReverseMap();
		}
	}
}
