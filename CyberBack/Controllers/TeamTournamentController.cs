using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamTournamentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Team_Tournament> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Team_Tournament.ToList();
        }

        [HttpGet("{id}")]
        public Team_Tournament Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Team_Tournament.FirstOrDefault(e => e.team_tournament_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Team_Tournament teamTournament)
        {
            using var entities = new cyberdbEntities();
            entities.Team_Tournament.Add(teamTournament);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Team_Tournament.Remove(entities.Team_Tournament.FirstOrDefault(e => e.team_tournament_id == id) ??
                                            throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Team_Tournament teamTournament)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Team_Tournament.FirstOrDefault(e => e.team_tournament_id == id);
            if (entity != null)
            {
                entity.tournament_id = teamTournament.tournament_id;
                entity.team_id = teamTournament.team_id;
            }

            entities.SaveChanges();
        }
    }
}