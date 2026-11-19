using kaapp.API.DTOs.Kunden;

namespace kaapp.API.Manager;

public interface IKundenManager
{
	public Task<GetKundenResponseDTO> GetKunden();
	public Task<GetKundeByIdResponseDTO> GetKunde(int id);
	public Task<bool> DeleteKunde(int id);
	public Task<int> CreateKunde(CreateKundeRequestDTO request);
	public Task<bool> IsKundeMitglied(int id);
}
