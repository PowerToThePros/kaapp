using kaapp.API.Data;
using kaapp.API.DTOs.Artikel;
using kaapp.API.Exceptions;

namespace kaapp.API.Business
{
	public class ArtikelBusiness(IArtikelData artikelData) : IArtikelBusiness
	{
		public async Task<CreateArtikelResponseDTO> CreateArtikel(CreateArtikelRequestDTO request)
		{
			request.Name = request.Name.Trim(); // hier beispiel
			request.Beschreibung = request.Beschreibung?.Trim() ?? string.Empty; // beschreibung kann null sein
			return await artikelData.CreateArtikel(request);
		}

		public async Task<GetAlleArtikelResponseDTO> GetAlleArtikel()
		{
			return await artikelData.GetAlleArtikel();
		}

		public async Task<GetArtikelResponseDTO> GetArtikel(int id)
		{
			if(id < 1)
			{
				throw new ValidationException("Artikel-ID darf nicht < 1 sein.");
			}
			return await artikelData.GetArtikel(id);
		}
		public async Task<bool> UpdateBestandForArtikel(UpdateBestandRequestDTO request)
		{
			return await artikelData.UpdateBestandForArtikel(request);
		}
	}
}
