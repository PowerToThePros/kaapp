using kaapp.API.Business;
using kaapp.API.DTOs.Artikel;
using kaapp.API.Helpers;

namespace kaapp.API.Manager
{
	public class ArtikelManager(IArtikelBusiness artikelBusiness, DatabaseContext databaseContext, ILogger<ArtikelManager> logger) : IArtikelManager
	{
		public async Task<CreateArtikelResponseDTO> CreateArtikel(CreateArtikelRequestDTO request)
		{
			await databaseContext.BeginTransactionAsync(); // erklären eine arbeitseinheit anhand create kunde da kann was schiefgehen bei inser mitglied
			try
			{
				var result = await artikelBusiness.CreateArtikel(request);
				logger.LogInformation("Artikel wurde erstellt"); // zeigen am beispiel
				await databaseContext.CommitAsync();
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Fehler beim Abrufen eines Kunden"); // logging dashboard und console
				await databaseContext.RollbackAsync();
				throw;
			}
		}

		public async Task<GetAlleArtikelResponseDTO> GetAlleArtikel()
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await artikelBusiness.GetAlleArtikel();
				await databaseContext.CommitAsync();
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Fehler beim Abrufen eines Kunden");
				await databaseContext.RollbackAsync();
				throw;
			}
		}

		public async Task<GetArtikelResponseDTO> GetArtikel(int id)
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await artikelBusiness.GetArtikel(id);
				await databaseContext.CommitAsync();
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Fehler beim Abrufen eines Kunden");
				await databaseContext.RollbackAsync();
				throw;
			}
		}

		public async Task<bool> UpdateBestandForArtikel(UpdateBestandRequestDTO request)
		{
			await databaseContext.BeginTransactionAsync();
			try
			{
				var result = await artikelBusiness.UpdateBestandForArtikel(request);
				await databaseContext.CommitAsync();
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Fehler beim Aktualisieren des Bestands eines Artikels");
				await databaseContext.RollbackAsync();
				throw;
			}
		}
	}
}
