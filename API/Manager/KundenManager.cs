using kaapp.API.Business;
using kaapp.API.Helpers;
using Microsoft.Extensions.Logging;
using kaapp.API.DTOs.Kunden;

namespace kaapp.API.Manager
{
	public class KundenManager(IKundenBusiness kundenBusiness, DatabaseContext databaseContext, ILogger<KundenManager> logger) : IKundenManager
	{
		public async Task<CreateKundeResponseDTO> CreateKunde(CreateKundeRequestDTO request)
		{
			await databaseContext.BeginTransactionAsync(); // Transaktions beginn 
			try
			{
				var result = await kundenBusiness.CreateKunde(request); // Business Schicht Call
				await databaseContext.CommitAsync(); // Transaktions Commit bei Erfolg
				return result;
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "Fehler beim Anlegen eines Kunden"); // Logging für die Analyse
				await databaseContext.RollbackAsync(); // Transaktions Rollback bei Fehler
				throw;
			}
		}

		public async Task<bool> DeleteKunde(int id)
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await kundenBusiness.DeleteKunde(id);
				await databaseContext.CommitAsync();
				return result;
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "Fehler beim Löschen eines Kunden");
				await databaseContext.RollbackAsync();
				throw;
			}
		}

		public async Task<GetKundeByIdResponseDTO> GetKunde(int id)
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await kundenBusiness.GetKunde(id);
				await databaseContext.CommitAsync();
				return result;
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "Fehler beim Abrufen eines Kunden");
				await databaseContext.RollbackAsync();
				throw;
			}
		}

		public async Task<GetKundenResponseDTO> GetKunden()
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await kundenBusiness.GetKunden();
				await databaseContext.CommitAsync();
				return result;
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "Fehler beim Abrufen der Kunden");
				await databaseContext.RollbackAsync();
				throw;
			}	
		}

		public async Task<bool> IsKundeMitglied(int id)
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await kundenBusiness.IsKundeMitglied(id);
				await databaseContext.CommitAsync();
				return result;
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "Fehler beim Überprüfen, ob ein Kunde Mitglied ist");
				await databaseContext.RollbackAsync();
				throw;
			}
		}
	}
}
