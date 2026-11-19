using System.ComponentModel.DataAnnotations;

namespace kaapp.API.DTOs.Artikel
{
	public class GetArtikelResponseDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Beschreibung { get; set; }
		public decimal Preis { get; set; }
		public int Bestand { get; set; }
	}

	public class GetAlleArtikelResponseDTO
	{
		public List<GetArtikelResponseDTO> Artikel { get; set; }
	}
}
