using kaapp.API.DTOs.Kunden;
using kaapp.API.Exceptions;
using kaapp.API.Manager;
using Microsoft.AspNetCore.Mvc;

namespace kaapp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KundenController(IKundenManager kundenManager) : ControllerBase
{
	[HttpGet("GetKunden")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> GetKunden()
	{
		try
		{
			var result = await kundenManager.GetKunden();
			return Ok(result);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("GetKunde/{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(404)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> GetKunde(int id)
	{
		try
		{
			var result = await kundenManager.GetKunde(id);
			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("DeleteKunde/{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(404)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> DeleteKunde(int id)
	{
		try
		{
			var result = await kundenManager.DeleteKunde(id);
			if (result == false)
			{
				return NotFound();
			}

			return Ok(result);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("CreateKunde")] // HTTP Methode + Route => https://localhost:7133/api/Kunden/CreateKunde
	[ProducesResponseType(200)]
	[ProducesResponseType(400)] // Automatische API Dokumentation https://localhost:7133/openapi/v1.json
	[ProducesResponseType(500)]
	public async Task<IActionResult> CreateKunde(CreateKundeRequestDTO request) // DTO
	{
		try
		{
			var result = await kundenManager.CreateKunde(request); // Manager Schicht Call
			return Ok(result); // 200
		}
		catch (ValidationException ex) // Falscher Request-Input
		{
			return BadRequest(ex.Message); // 400
		}
	}

	[HttpGet("IsKundeMitglied/{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> IsKundeMitglied(int id)
	{
		try
		{
			var result = await kundenManager.IsKundeMitglied(id);
			return Ok(result);
		}
		catch (ValidationException ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
