namespace Movie_Store_Web_API.Entities
{
	public class Director : BaseEntity<int>
	{
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public ICollection<Movie> Movies { get; set; } = [];
	}
}
