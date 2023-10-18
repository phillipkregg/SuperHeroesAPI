using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var hero = _context.SuperHeroes.FindAsync(id);

            if (hero.Result == null)
            {
                return BadRequest("Hero not found");
            }
            
            return Ok(hero);
        }

        [HttpPost("add-hero", Name = "AddHero")]
        [SwaggerOperation(OperationId = "add-hero")]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        
        [HttpPut("update-hero", Name = "UpdateHero")]
        [SwaggerOperation(OperationId = "UpdateHero")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var heroRecord = await _context.SuperHeroes.FindAsync(request.Id);

            if (heroRecord == null)
            {
                return BadRequest("Hero not found");
            }

            PropertyInfo[] properties = typeof(SuperHero).GetProperties();

            foreach (var property in properties)
            {
                object newValue = property.GetValue(request);
                
                if (newValue is int) continue;
                
                if ((string?)newValue != "")
                {
                    property.SetValue(heroRecord, newValue);
                }
            }
            
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("delete-hero/{id}", Name = "DeleteHero")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id, [FromBody] SuperHero request)
        {
            var heroRecord = await _context.SuperHeroes.FindAsync(request.Id);

            if (heroRecord == null)
            {
                return BadRequest("Hero not found");
            }

            _context.SuperHeroes.Remove(heroRecord);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync());

        }
    }
}
