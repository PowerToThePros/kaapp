using kaapp.API.DTOs.Kunden;
using kaapp.API.Helpers;
using System.Data;

namespace kaapp.API.Data;

public class KundenData(DatabaseContext databaseContext) : IKundenData
{
	public async Task<CreateKundeResponseDTO> CreateKunde(CreateKundeRequestDTO request)
	{
		// Command vorbereiten
		var command = databaseContext.CreateCommand("INSERT INTO kunden (vorname, nachname, email) " +
			"VALUES (@vorname, @nachname, @email) RETURNING id");

		// Parameter bindung
		command.Parameters.AddWithValue("vorname", request.Vorname); // Verhindert SQL Injection
		command.Parameters.AddWithValue("nachname", request.Nachname);
		command.Parameters.AddWithValue("email", request.Email);

		// Command ausführen
		var id = await command.ExecuteScalarAsync(); // Gibt erste Zeile und Spalte zurück

		var result = new CreateKundeResponseDTO()
		{
			Id = (int)id // die Datenbank gibt ein objekt zurück daher der Cast
		};

		return result;
	}

	public async Task<bool> DeleteKunde(int id)
	{
		var command = databaseContext.CreateCommand("DELETE FROM kunden WHERE id = @id");
		command.Parameters.AddWithValue("id", id);

		var rowsAffected = await command.ExecuteNonQueryAsync(); // returns amount of rows affected
		return rowsAffected > 0;
	}

	public async Task<GetKundeByIdResponseDTO> GetKunde(int id)
	{
		var command = databaseContext.CreateCommand("SELECT id, vorname, nachname, email FROM kunden WHERE id = @id");
		command.Parameters.AddWithValue("id", id);

		await using var reader = await command.ExecuteReaderAsync();

		var kunde = new GetKundeByIdResponseDTO();

		if (!await reader.ReadAsync())
		{
			return null;
		}

		kunde = new GetKundeByIdResponseDTO
		{
			Id = reader.GetInt32("id"),
			Vorname = reader.GetString("vorname"),
			Nachname = reader.GetString("nachname"),
			Email = reader.GetString("email")
		};

		return kunde;
	}

	public async Task<GetKundenResponseDTO> GetKunden()
	{
		/*
		await using var dataSource = NpgsqlDataSource.Create(connectionString); 
		
		await using var connection = await dataSource.OpenConnectionAsync();

		//var command = new NpgsqlCommand(@"SELECT vorname, nachname, email FROM ""Kunden""", connection);

		var command = new NpgsqlCommand("SELECT id, vorname, nachname, email FROM kunden", connection);
		*/

		var command = databaseContext.CreateCommand("SELECT id, vorname, nachname, email FROM kunden ORDER BY id");
		await using var reader = await command.ExecuteReaderAsync();

		var kundenList = new List<GetKundeByIdResponseDTO>();

		while (await reader.ReadAsync())
		{
			var kunden = new GetKundeByIdResponseDTO
			{
				Id = reader.GetInt32("id"),
				Vorname = reader.GetString("vorname"),
				Nachname = reader.GetString("nachname"),
				Email = reader.GetString("email")
			};
			kundenList.Add(kunden);
		}

		var KundenResponse = new GetKundenResponseDTO
		{
			Kunden = kundenList
		};

		return KundenResponse;
	}

	public async Task<bool> InsertMitglied(int kundenId)
	{
		var command = databaseContext.CreateCommand("INSERT INTO mitglieder (kunden_id, eintrittsdatum) VALUES (@kundenId, @eintrittsdatum)");
		command.Parameters.AddWithValue("kundenId", kundenId);
		command.Parameters.AddWithValue("eintrittsdatum", DateOnly.FromDateTime(DateTime.Now));
		var rowsAffected = await command.ExecuteNonQueryAsync(); // gibt anzahl betroffener rows zurück
		return rowsAffected > 0;
	}

	public async Task<bool> IsKundeMitglied(int kundenId)
	{
		var command = databaseContext.CreateCommand("SELECT COUNT(*) FROM mitglieder WHERE kunden_id = @kundenId");
		command.Parameters.AddWithValue("kundenId", kundenId);
		var result = await command.ExecuteScalarAsync();
		return result == null || Convert.ToInt32(result) == 0 ? false : true;
	}
}
