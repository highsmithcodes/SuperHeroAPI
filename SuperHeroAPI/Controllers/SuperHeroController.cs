using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    public class SuperHeroController : Controller
    {
        public DataContext DataContext { get; }

        public SuperHeroController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await DataContext.SuperHeroes.ToListAsync());
        }

        // GET 1 Superhero
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await DataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        // POST:
        //[HttpPost]
        //public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        //{
        //    DataContext.SuperHeroes.Add(hero);
        //    await DataContext.SaveChangesAsync();

        //    return Ok(await DataContext.SuperHeroes.ToListAsync());
        //}

        // PUT:
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {

            var dbHero = await DataContext.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await DataContext.SaveChangesAsync();

            return Ok(await DataContext.SuperHeroes.ToListAsync());
        }

        // DELETE:
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await DataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            DataContext.SuperHeroes.Remove(dbHero);
            await DataContext.SaveChangesAsync();

            return Ok(await DataContext.SuperHeroes.ToListAsync());
        }



    }
}

