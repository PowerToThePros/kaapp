using kaapp.API.DTOs.Artikel;
using kaapp.API.Exceptions;
using kaapp.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kaapp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArtikelController(IArtikelManager artikelManager) : ControllerBase
	{
		[HttpGet("GetAlleArtikel")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetAlleArtikel()
		{
			try
			{
				var result = await artikelManager.GetAlleArtikel();
				return Ok(result);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetArtikel/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetArtikel(int id)
		{
			try
			{
				var result = await artikelManager.GetArtikel(id);
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


		[HttpPost("CreateArtikel")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> CreateArtikel(CreateArtikelRequestDTO request)
		{
			try
			{
				var result = await artikelManager.CreateArtikel(request);
				return Ok(result);
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch("UpdateBestandForArtikel")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> UpdateBestandForArtikel(UpdateBestandRequestDTO request)
		{
			try
			{
				var result = await artikelManager.UpdateBestandForArtikel(request);
				if (!result)
				{
					return NotFound();
				}

				return Ok();
			}
			catch (ValidationException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
