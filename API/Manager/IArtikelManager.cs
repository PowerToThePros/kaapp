using kaapp.API.DTOs.Artikel;

namespace kaapp.API.Manager
{
	public interface IArtikelManager
	{
		public Task<GetAlleArtikelResponseDTO> GetAlleArtikel();
		public Task<GetArtikelResponseDTO> GetArtikel(int id);

		public Task<CreateArtikelResponseDTO> CreateArtikel(CreateArtikelRequestDTO request);
		public Task<bool> UpdateBestandForArtikel(UpdateBestandRequestDTO request);
	}
}
