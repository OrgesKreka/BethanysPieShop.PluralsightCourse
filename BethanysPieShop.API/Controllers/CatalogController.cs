using BethanysPieShop.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.API.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CatalogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

            appDbContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Pies([FromQuery] int pagesize = 10, [FromQuery] int pageIndex = 0)
        {
            var items = await _appDbContext.Pies
                             .OrderBy(p => p.Name)
                             .Include(p => p.Category)
                             .Skip(pagesize * pageIndex)
                             .Take(pagesize)
                             .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> PiesOfTheWeek()
        {
            var items = await _appDbContext.Pies
                             .Where(p => p.IsPieOfTheWeek)
                             .ToListAsync();
            return Ok(items);
        }


        [HttpGet]
        [Route("pies/{id:int}")]
        public async Task<IActionResult> GetPieById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var pie = await _appDbContext.Pies
                            .Where(p => p.PieId == id)
                            .SingleOrDefaultAsync();

            if (pie == null)
                return NotFound();

            return Ok(pie);
        }
    }
}
