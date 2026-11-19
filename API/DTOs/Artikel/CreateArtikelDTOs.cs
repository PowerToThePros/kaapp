using System.ComponentModel.DataAnnotations;

namespace kaapp.API.DTOs.Artikel
{
	public class CreateArtikelRequestDTO
	{
		// Data anotations nutzen wir für validation der ASP.Net validiert die dann im controller automatisch
		// so was wie "" oder " " wird nicht berücksichtigt also ist business validation trotzdem sinnvoll
		[Required]
		[MaxLength(100)]
		public required string Name { get; set; }
		public string? Beschreibung { get; set; }
		[Range(0, double.MaxValue)]
		public decimal Preis { get; set; }
		[Range(0, int.MaxValue)]
		public int Bestand { get; set; }
	}

	public class CreateArtikelResponseDTO
	{
		public int Id { get; set; }
	}
}
