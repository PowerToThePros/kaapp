using System.ComponentModel.DataAnnotations;

namespace kaapp.API.DTOs.Artikel
{
	public class UpdateBestandRequestDTO
	{
		public int Id { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int Bestand { get; set; }
	}
}
