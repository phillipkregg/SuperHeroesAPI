using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHeroAPI.Controllers
{
    [Route("api/super-heroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        private static readonly List<SuperHero> Heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id = 1, 
                Name = "Spider Man",
                FirstName = "Peter", 
                LastName = "Parker",
                PlaceOfBirth = "New York City"
            },
            new SuperHero
            {
                Id = 2, 
                Name = "Ironman",
                FirstName = "Tony", 
                LastName = "Stark",
                PlaceOfBirth = "Long Island"
            }
        };

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet("get-all-heroes", Name = "GetAllHeroes")]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("get-hero/{id}", Name = "GetHero")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = Heroes.Find(h => h.Id == id);

            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            
            return Ok(hero);
        }

        [HttpPost("add-hero", Name = "AddHero")]
        [SwaggerOperation(OperationId = "add-hero")]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            Heroes.Add(hero);
            return Ok(Heroes);
        }
        
        [HttpPut("update-hero/{id}", Name = "UpdateHero")]
        [SwaggerOperation(OperationId = "UpdateHero")]
        public async Task<ActionResult<SuperHero>> UpdateHero(int id, [FromBody] SuperHero request)
        {
            var hero = Heroes.Find(h => h.Id == id);

            if (hero == null)
            {
                return BadRequest("Hero not found");
            }

            // Use model validation to check which properties are allowed to be updated
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(Heroes);
        }

        [HttpDelete("delete-hero/{id}", Name = "DeleteHero")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id, [FromBody] SuperHero request)
        {
            var hero = Heroes.Find(h => h.Id == id);
            
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }

            Heroes.Remove(hero);

            return Ok(Heroes);

        }
    }
}
