namespace Movie_Store_Web_API.Entities
{
	public class Genre : BaseEntity<int>
	{
		public required string Name { get; set; }
		public ICollection<Movie> Movies { get; set; } = [];
	}
}
