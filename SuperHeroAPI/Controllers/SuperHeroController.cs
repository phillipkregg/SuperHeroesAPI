using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
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

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = Heroes.Find(h => h.Id == id);

            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            
            return Ok(hero);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(Heroes);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            Heroes.Add(hero);
            return Ok(Heroes);
        }
        
        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
