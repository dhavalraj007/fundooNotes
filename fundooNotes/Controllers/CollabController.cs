using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace fundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBuisness collabBuisness;

        public CollabController(ICollabBuisness collabBuisness) : base()
        {
            this.collabBuisness = collabBuisness;
        }

        [HttpPost("AddCollab")]
        public IActionResult AddCollab(CollabModel model)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var collab= collabBuisness.AddCollab(model, UserId);
            if (collab != null)
            {
                return Ok(new ResponseModel<CollabEntity> { Status = true, Message = "collabrator Added succesfully", Data = collab });
            }
            return BadRequest(new ResponseModel<CollabEntity> { Status = true, Message = "collabrator  Adding failed!", Data = collab });
        }

        
        [HttpPost("DeleteCollab")]
        public IActionResult DeleteCollab(long CollabId, long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var res = collabBuisness.DeleteCollab(CollabId, NoteId, UserId);

            if (res)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "collabrator deleted succesfully", Data = res });
            }
            return BadRequest(new ResponseModel<bool> { Status = true, Message = "collabrator delete failed!", Data = res });
        }

        [HttpPost("GetCollab")]
        public IActionResult GetCollab(long CollabId, long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var collab = collabBuisness.GetCollab(CollabId, NoteId, UserId);

            if (collab != null)
            {
                return Ok(new ResponseModel<CollabEntity> { Status = true, Message = "collabrator  get succesfully", Data = collab });
            }
            return BadRequest(new ResponseModel<CollabEntity> { Status = true, Message = "collabrator get failed!", Data = collab });
        }


        [HttpGet("GetAllCollabs")]
        public IActionResult GetAllCollabs(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var collabs= collabBuisness.GetAllCollabs(NoteId, UserId);
            if (collabs != null)
            {
                return Ok(new ResponseModel<List<CollabEntity>> { Status = true, Message = "collabrators get succesfully", Data = collabs });
            }
            return BadRequest(new ResponseModel<List<CollabEntity>> { Status = true, Message = "collabrators get failed!", Data = collabs });
        }
    }
}
