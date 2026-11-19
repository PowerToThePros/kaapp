using kaapp.API.DTOs.Artikel;
using kaapp.API.Helpers;
using System.Data;

namespace kaapp.API.Data
{
	public class ArtikelData(DatabaseContext databaseContext) : IArtikelData
	{
		public async Task<CreateArtikelResponseDTO> CreateArtikel(CreateArtikelRequestDTO request)
		{
			var command = databaseContext.CreateCommand("INSERT INTO Artikel (Name, Beschreibung, Preis, Bestand) VALUES (@Name, @Beschreibung, @Preis, @Bestand) RETURNING id");
			command.Parameters.AddWithValue("@Name", request.Name); // Macht auch bereits SQL Injection Prevention, da Parameterized Queries verwendet werden
			command.Parameters.AddWithValue("@Beschreibung", request.Beschreibung);
			command.Parameters.AddWithValue("@Preis", request.Preis);
			command.Parameters.AddWithValue("@Bestand", request.Bestand);

			var id = await command.ExecuteScalarAsync(); // ExecuteScalarAsync returns the first column of the first row in the result set

			var result = new CreateArtikelResponseDTO()
			{
				Id = (int)id
			};

			return result;
		}

		public async Task<GetAlleArtikelResponseDTO> GetAlleArtikel()
		{
			var command = databaseContext.CreateCommand("SELECT id, name, beschreibung, preis, bestand FROM artikel ORDER BY id");
			await using var reader = await command.ExecuteReaderAsync();

			var artikelList = new List<GetArtikelResponseDTO>();

			while (await reader.ReadAsync())
			{
				var artikel = new GetArtikelResponseDTO
				{
					Id = reader.GetInt32("id"),
					Name = reader.GetString("name"),
					Beschreibung = reader.GetString("beschreibung"),
					Preis = reader.GetDecimal("preis"),
					Bestand = reader.GetInt32("bestand")
				};
				artikelList.Add(artikel);
			}

			var response = new GetAlleArtikelResponseDTO
			{
				Artikel = artikelList
			};

			return response;
		}

		public async Task<GetArtikelResponseDTO> GetArtikel(int id)
		{
			var command = databaseContext.CreateCommand("SELECT id, name, beschreibung, preis, bestand FROM artikel WHERE id = @id");
			command.Parameters.AddWithValue("id", id);

			await using var reader = await command.ExecuteReaderAsync();

			var artikel = new GetArtikelResponseDTO();

			if (!await reader.ReadAsync())
			{
				return null;
			}

			artikel = new GetArtikelResponseDTO
			{
				Id = reader.GetInt32("id"),
				Name = reader.GetString("name"),
				Beschreibung = reader.GetString("beschreibung"),
				Preis = reader.GetDecimal("preis"),
				Bestand = reader.GetInt32("bestand")
			};

			return artikel;
		}

		public async Task<bool> UpdateBestandForArtikel(UpdateBestandRequestDTO request)
		{
			var command = databaseContext.CreateCommand("UPDATE artikel SET bestand = @Bestand WHERE id = @Id");
			command.Parameters.AddWithValue("@Id", request.Id);
			command.Parameters.AddWithValue("@Bestand", request.Bestand);
			var rowsAffected = await command.ExecuteNonQueryAsync();
			return rowsAffected > 0;
		}
	}
}
