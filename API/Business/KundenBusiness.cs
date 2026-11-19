using kaapp.API.Data;
using kaapp.API.DTOs.Kunden;
using kaapp.API.Exceptions;

namespace kaapp.API.Business;

public class KundenBusiness(IKundenData kundenData) : IKundenBusiness
{
	public async Task<int> CreateKunde(CreateKundeRequestDTO request)
	{
		var newId = await kundenData.CreateKunde(request); // Data Schicht Call

		if (request.Mitglied)
		{
			var success = await kundenData.InsertMitglied(newId); // Data Schicht Call

			if (!success)
			{
				// Fehler werfen die der Manager Fängt um Rollback zu machen
				// Kunde darf nicht angelegt worden sein, wenn die Mitgliedschaft setzen fehlschlug
				throw new ArgumentNullException("Mitglied konnte nicht angelegt werden.");
			}
		}

		return newId;
	}

	public async Task<bool> DeleteKunde(int id)
	{
		if (id < 1)
		{
			throw new ValidationException("Id darf nicht < 1 sein");
		}

		return await kundenData.DeleteKunde(id);
	}

	public async Task<GetKundeByIdResponseDTO> GetKunde(int id)
	{
		if (id < 1)
		{
			throw new ValidationException("Id darf nicht < 1 sein");
		}

		return await kundenData.GetKunde(id);
	}

	public async Task<GetKundenResponseDTO> GetKunden()
	{
		return await kundenData.GetKunden();
	}

	public async Task<bool> IsKundeMitglied(int id)
	{
		if (id < 1)
		{
			throw new ValidationException("Id darf nicht < 1 sein");
		}

		return await kundenData.IsKundeMitglied(id);
	}
}
