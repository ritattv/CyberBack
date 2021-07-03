using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Team.ToList();
        }

        [HttpGet("{id}")]
        public Team Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Team.FirstOrDefault(e => e.team_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Team team)
        {
            try
            {
                using var entities = new cyberdbEntities();
                entities.Team.Add(team);
                entities.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Team.Remove(entities.Team.FirstOrDefault(e => e.team_id == id) ??
                                 throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Team team)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Team.FirstOrDefault(e => e.team_id == id);
            if (entity != null)
            {
                entity.team_name = team.team_name;
                entity.team_country = team.team_country;
                entity.game_id = team.game_id;
            }

            entities.SaveChanges();
        }
    }
}