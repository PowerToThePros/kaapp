using kaapp.API.DTOs.Kunden;

namespace kaapp.API.Data
{
	public interface IKundenData
	{
		public Task<GetKundenResponseDTO> GetKunden();
		public Task<GetKundeByIdResponseDTO> GetKunde(int id);
		public Task<bool> DeleteKunde(int id);
		public Task<CreateKundeResponseDTO> CreateKunde(CreateKundeRequestDTO request);
		public Task<bool> InsertMitglied(int id);
		public Task<bool> IsKundeMitglied(int id);
	}
}
