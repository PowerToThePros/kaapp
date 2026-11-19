namespace kaapp.API.DTOs.Kunden
{
	public class GetKundenResponseDTO
	{
		public List<GetKundeByIdResponseDTO> Kunden { get; set; }
	}

	public class GetKundeByIdResponseDTO
	{
		public int Id { get; set; }
		public string Vorname { get; set; }
		public string Nachname { get; set; }
		public string Email { get; set; }
	}
}
