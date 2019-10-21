using BethanysPieShop.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.API.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ContactController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                ContactInfo info = new ContactInfo()
                {
                    Email = contactInfo.Email,
                    Message = contactInfo.Message
                };
                _appDbContext.ContactInfos.Add(info);
                _appDbContext.SaveChanges();
                return Ok(info);
            }

            return BadRequest();
        }
    }
}
