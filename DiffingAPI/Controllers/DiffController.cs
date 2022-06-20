using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DiffingAPI.Models;

namespace DiffingAPI.Controllers {
  [Route("v1/[controller]")]
  [ApiController]
  public class DiffController : ControllerBase {
    private readonly DiffingContext _context;

    public DiffController(DiffingContext context) {
      _context = context;
    }

    // GET: v1/diff/5
    /// <summary>
    /// Retrieves data pair from db and generates the diff
    /// </summary>
    /// <param name="id">Id of diff</param>
    /// <returns>Diff result</returns>
    [HttpGet("{id}")]
    public ActionResult<DiffResult> GetDiff(int id) {
      var dataPair = RetrieveDataPair(id);
      if (dataPair is null || !dataPair.IsValidDataPair()) {
        return NotFound();
      }

      var result = dataPair.GenerateDiffResult();

      return Ok(JsonConvert.SerializeObject(result));
    }

    // PUT: v1/diff/5/left
    /// <summary>
    /// Adds left data to a data pair
    /// </summary>
    /// <param name="id">Id of diff</param>
    /// <param name="data">Base64 encoded string</param>
    [HttpPut("{id}/left")]
    public async Task<IActionResult> PutDiffLeft(int id, [FromBody] string data) {
      // Check if data is Base64 encoded string and decode.
      try {
        byte[] decodedData = Convert.FromBase64String(data);
      }
      catch (Exception) {
        return BadRequest();
      }

      var dataPair = RetrieveDataPair(id);

      if (dataPair is null) {
        dataPair = new DataPair() {
          Id = id,
          LeftData = data
        };

        _context.Add(dataPair);
      }
      else {
        dataPair.LeftData = data;
      }


      var result = await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDiff), new {id = dataPair.Id}, dataPair);
    }

    // PUT: v1/diff/5/left
    /// <summary>
    /// Adds right data to a data pair
    /// </summary>
    /// <param name="id">Id of diff</param>
    /// <param name="data">Base64 encoded string</param>
    [HttpPut("{id}/right")]
    public async Task<IActionResult> PutDiffRight(int id, [FromBody] string data) {
      // Check if data is Base64 encoded string and decode.
      try {
        byte[] decodedData = Convert.FromBase64String(data);
      }
      catch (Exception) {
        return BadRequest();
      }

      var dataPair = RetrieveDataPair(id);

      if (dataPair is null) {
        dataPair = new DataPair() {
          Id = id,
          RightData = data
        };

        _context.Add(dataPair);
      }
      else {
        dataPair.RightData = data;
      }


      var result = await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDiff), new { id = dataPair.Id }, dataPair);
    }

    /// <summary>
    /// Retrieves data pair from db
    /// </summary>
    /// <param name="id">Id of data pair</param>
    /// <returns>Data pair or null if not found</returns>
    private DataPair? RetrieveDataPair(int id) {
      return _context.DataPairs.Where(d => d.Id == id).FirstOrDefault();
    }
  }
}
