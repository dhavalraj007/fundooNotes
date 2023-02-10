using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBuisness noteBuisness;

        public NoteController(INoteBuisness noteBuisness):base()
        {
            this.noteBuisness = noteBuisness;
        }

        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel model)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var note = noteBuisness.AddNote(model,UserId);
            if(note!=null)
            {
                return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Note Added succesfully", Data = note });
            }
            return BadRequest(new ResponseModel<NoteEntity> { Status = true, Message = "Note Adding failed!", Data = note });
        }

        [HttpGet("GetNote")]
        public IActionResult GetAllNotes()
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var notes = noteBuisness.GetAllNotes(UserId);
            if (notes != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "get Notes succesfully", Data = notes });
            }
            return BadRequest(new ResponseModel<List<NoteEntity>> { Status = true, Message = "get notes failed!", Data = notes });
        }
    }
}
