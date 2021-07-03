using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Player.ToList();
        }

        [HttpGet("{id}")]
        public Player Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Player.FirstOrDefault(e => e.player_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Player player)
        {
            using var entities = new cyberdbEntities();
            player.player_login = Encryption.GetInstance()
                .HashPassword(player.player_login, Encryption.GetInstance().GenerateSalt());
            player.player_password = Encryption.GetInstance()
                .HashPassword(player.player_password, Encryption.GetInstance().GenerateSalt());
            entities.Player.Add(player);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Player.Remove(entities.Player.FirstOrDefault(e => e.player_id == id) ??
                                   throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Player player)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Player.FirstOrDefault(e => e.player_id == id);
            if (entity != null)
            {
                entity.player_lastname = player.player_lastname;
                entity.player_firstname = player.player_firstname;
                entity.player_birthdate = player.player_birthdate;
                entity.player_country = player.player_country;
                entity.team_id = player.team_id;
                entity.player_login = Encryption.GetInstance()
                    .HashPassword(player.player_login, Encryption.GetInstance().GenerateSalt());
                entity.player_password = Encryption.GetInstance()
                    .HashPassword(player.player_password, Encryption.GetInstance().GenerateSalt());
            }

            entities.SaveChanges();
        }
    }
}