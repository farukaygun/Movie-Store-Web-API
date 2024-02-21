namespace Movie_Store_Web_API.Entities
{
	public class Movie : BaseEntity<int>
	{
		public required string Name { get; set; }
		public required int Year { get; set; }
		public required decimal Price { get; set; }
		public required ICollection<Genre> Genres { get; set; } = [];
		public required ICollection<Director> Directors { get; set; } = [];
		public required ICollection<Actor> Actors { get; set; } = [];
	}
}
