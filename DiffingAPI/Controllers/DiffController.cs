using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiffingAPI;

namespace DiffingAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly DiffingContext _context;

        public DiffController(DiffingContext context)
        {
            _context = context;
        }

        // GET: v1/diff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diff>> GetDiff(int id)
        {
            return NotFound();
        }

        // PUT: v1/diff/5/left
        [HttpPut("{id}/left")]
        public async Task<IActionResult> PutDiffLeft(int id, [FromBody] string data)
        {
            return NotFound();
        }

        // PUT: v1/diff/5/left
        [HttpPut("{id}/right")]
        public async Task<IActionResult> PutDiffRight(int id, [FromBody] string data)
        {
            return NotFound();
        }
    }
}
