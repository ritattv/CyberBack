using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Coach> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Coach.ToList();
        }

        [HttpGet("{id}")]
        public Coach Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Coach.FirstOrDefault(e => e.coach_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Coach coach)
        {
            using var entities = new cyberdbEntities();
            coach.coach_login = Encryption.GetInstance()
                .HashPassword(coach.coach_login, Encryption.GetInstance().GenerateSalt());
            coach.coach_password = Encryption.GetInstance()
                .HashPassword(coach.coach_password, Encryption.GetInstance().GenerateSalt());
            entities.Coach.Add(coach);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Coach.Remove(entities.Coach.FirstOrDefault(e => e.coach_id == id) ??
                                  throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Coach coach)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Coach.FirstOrDefault(e => e.coach_id == id);
            if (entity != null)
            {
                entity.coach_lastname = coach.coach_lastname;
                entity.coach_firstname = coach.coach_firstname;
                entity.coach_birthdate = coach.coach_birthdate;
                entity.coach_country = coach.coach_country;
                entity.team_id = coach.team_id;
                entity.coach_login = Encryption.GetInstance()
                    .HashPassword(coach.coach_login, Encryption.GetInstance().GenerateSalt());
                ;
                entity.coach_password = Encryption.GetInstance()
                    .HashPassword(coach.coach_password, Encryption.GetInstance().GenerateSalt());
            }

            entities.SaveChanges();
        }
    }
}