using AutoMapper;
using Movie_Store_Web_API.Application.Actor_Operations.Create;
using Movie_Store_Web_API.Application.Actor_Operations.Queries.Get_Actors;
using Movie_Store_Web_API.Application.Actor_Operations.Update;
using Movie_Store_Web_API.Application.Customer_Operations.Create;
using Movie_Store_Web_API.Application.Director_Operations.Create;
using Movie_Store_Web_API.Application.Director_Operations.Queries.Get_Directors;
using Movie_Store_Web_API.Application.Genre_Operations.Create;
using Movie_Store_Web_API.Application.Genre_Operations.Queries.Get_Genres;
using Movie_Store_Web_API.Application.Movie_Operations.Create;
using Movie_Store_Web_API.Application.Movie_Operations.Queries.Get_Movies;
using Movie_Store_Web_API.Application.Order.Create;
using Movie_Store_Web_API.Application.Order.Queries.Get_Orders;
using Movie_Store_Web_API.Entities;

namespace Movie_Store_Web_API.Common
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Movie, CreateMovieModel>().ReverseMap();
			CreateMap<Movie, GetMovieModel>().ReverseMap();

			CreateMap<Actor, CreateActorModel>().ReverseMap();
			CreateMap<Actor, GetActorModel>().ReverseMap();
			CreateMap<Actor, PatchActorModel>().ReverseMap();

			CreateMap<Director, CreateDirectorModel>().ReverseMap();
			CreateMap<Director, GetDirectorModel>().ReverseMap();

			CreateMap<Genre, CreateGenreModel>().ReverseMap();
			CreateMap<Genre, GetGenreModel>().ReverseMap();
		
			CreateMap<Order, CreateOrderModel>().ReverseMap();
			CreateMap<Order, GetOrderModel>().ReverseMap();

			CreateMap<Customer, CreateCustomerModel>().ReverseMap();
			CreateMap<Customer, CustomerOrderModel>().ReverseMap();
		}
	}
}
