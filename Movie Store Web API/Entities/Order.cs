using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Store_Web_API.Entities
{
	public class Order : BaseEntity<int>
	{
		private DateTime date = DateTime.Now;

		public required int CustomerId { get; set; }
		[ForeignKey(nameof(CustomerId))]
		public required Customer Customer { get; set; }
		public required ICollection<Movie> Movies { get; set; }
		public required double Price { get; set; }
		public DateTime Date { get => date; set => date = value; }
	}
}
