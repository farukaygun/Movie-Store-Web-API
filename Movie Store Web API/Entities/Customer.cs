namespace Movie_Store_Web_API.Entities
{
	public class Customer : BaseEntity<int>
	{
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public ICollection<Movie> OrderedMovies { get; set; } = [];
		public ICollection<Genre> FavoriteGenres { get; set; } = [];
	}
}
