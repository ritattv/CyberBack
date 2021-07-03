using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Game.ToList();
        }

        [HttpGet("{id}")]
        public Game Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Game.FirstOrDefault(e => e.game_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Game game)
        {
            using var entities = new cyberdbEntities();
            entities.Game.Add(game);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Game.Remove(entities.Game.FirstOrDefault(e => e.game_id == id) ??
                                 throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Game game)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Game.FirstOrDefault(e => e.game_id == id);
            if (entity != null)
            {
                entity.game_name = game.game_name;
                entity.game_year = game.game_year;
            }

            entities.SaveChanges();
        }
    }
}