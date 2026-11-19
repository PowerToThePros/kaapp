using System.ComponentModel.DataAnnotations;

namespace kaapp.API.DTOs.Kunden;

public class CreateKundeRequestDTO
{
	[Required]
	public required string Vorname { get; set; }
	[Required]
	public required string Nachname { get; set; }
	[Required]
	[EmailAddress]
	public required string Email { get; set; }
	public bool Mitglied { get; set; }
}
