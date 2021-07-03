using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Health> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Health.ToList();
        }

        [HttpGet("{id}")]
        public Health Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Health.FirstOrDefault(e => e.health_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Health health)
        {
            using var entities = new cyberdbEntities();
            entities.Health.Add(health);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Health.Remove(entities.Health.FirstOrDefault(e => e.health_id == id) ??
                                   throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Health health)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Health.FirstOrDefault(e => e.health_id == id);
            if (entity != null)
            {
                entity.health_temperature = health.health_temperature;
                entity.health_pulse = health.health_pulse;
                entity.health_systolicPressure = health.health_systolicPressure;
                entity.health_diastolicPressure = health.health_diastolicPressure;
                entity.player_id = health.player_id;
            }

            entities.SaveChanges();
        }
    }
}