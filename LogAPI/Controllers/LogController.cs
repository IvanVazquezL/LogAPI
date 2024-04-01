using LogAPI.Models;
using LogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;

namespace LogAPI.Controllers
{
    [ApiController]
    [Route("api/log")]
    public class LogController : ControllerBase
    {
        private ILogCollection db = new LogCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            return Ok(await db.GetAllLogs());
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog([FromBody] Log log)
        {
            if (log == null)
                return BadRequest();

            if (log.UserId == string.Empty)
                ModelState.AddModelError("UserId", "UserId is required");

            if (log.PageId == string.Empty)
                ModelState.AddModelError("PageId", "PageId is required");

            if (log.Timestamp == DateTime.MinValue)
                ModelState.AddModelError("Timestamp", "Timestamp is required");

            await db.InsertLog(log);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLog([FromBody] Log log, string id)
        {
            if (log == null)
                return BadRequest();
            
            if (log.UserId == string.Empty)
                ModelState.AddModelError("UserId", "UserId is required");
            
            if (log.PageId == string.Empty)
                ModelState.AddModelError("PageId", "PageId is required");

            if (log.Timestamp == DateTime.MinValue)
                ModelState.AddModelError("Timestamp", "Timestamp is required");

            log.Id = new ObjectId(id);
            await db.UpdateLog(log);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(string id)
        {
            await db.DeleteLog(id);
            return NoContent();
        }

        [HttpGet("pageId/{id}")]
        public async Task<IActionResult> GetLogsByPageId(string id)
        {
            return Ok(await db.GetLogsByPageId(id));
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetLogsByUserId(string id)
        {
            return Ok(await db.GetLogsByUserId(id));
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetLogsByDate(string date)
        {
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format. Please provide the date in yyyy-MM-dd format.");
            }

            return Ok(await db.GetLogsByDate(parsedDate.Date));
        }
    }
}
