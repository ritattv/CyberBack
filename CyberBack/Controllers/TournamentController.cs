using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Tournament> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Tournament.ToList();
        }

        [HttpGet("{id}")]
        public Tournament Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Tournament.FirstOrDefault(e => e.tournament_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Tournament tournament)
        {
            using var entities = new cyberdbEntities();
            entities.Tournament.Add(tournament);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Tournament.Remove(entities.Tournament.FirstOrDefault(e => e.tournament_id == id) ??
                                       throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Tournament tournament)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Tournament.FirstOrDefault(e => e.tournament_id == id);
            if (entity != null)
            {
                entity.tournament_name = tournament.tournament_name;
                entity.tournament_type = tournament.tournament_type;
                entity.tournament_startdate = tournament.tournament_startdate;
                entity.tournament_enddate = tournament.tournament_enddate;
                entity.tournament_location = tournament.tournament_location;
                entity.game_id = tournament.game_id;
                entity.organizer_id = tournament.organizer_id;
            }

            entities.SaveChanges();
        }
    }
}