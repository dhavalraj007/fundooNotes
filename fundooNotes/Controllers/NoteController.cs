using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fundooNotes.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBuisness noteBuisness;
        private readonly ILogger<NoteController> logger;
        private readonly IDistributedCache distributedCache; 

        public NoteController(INoteBuisness noteBuisness,ILogger<NoteController> logger, IDistributedCache distributedCache) :base()
        {
            this.noteBuisness = noteBuisness;
            this.logger = logger;
            this.distributedCache= distributedCache;
        }

        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel model)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var note = noteBuisness.AddNote(model,UserId);
            if(note!=null)
            {
                logger.LogInformation($"New Note added with id={note.NoteId}");
                return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Note Added succesfully", Data = note });
            }
            logger.LogError($"New Note adding failed.");
            return BadRequest(new ResponseModel<NoteEntity> { Status = true, Message = "Note Adding failed!", Data = note });
        }
        [HttpPost("GetNote")]
        public IActionResult GetNote(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var note = noteBuisness.GetNote(NoteId,UserId);
            if(note!=null)
            {
                return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Note get succesfully", Data = note });
            }
            return BadRequest(new ResponseModel<NoteEntity> { Status = true, Message = "Note get failed!", Data = note });
        }


        [HttpPost("DeleteNote")]
        public IActionResult DeleteNote(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var res = noteBuisness.DeleteNote(NoteId, UserId);
            if (res)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Deleted Note succesfully", Data = res});
            }
            return BadRequest(new ResponseModel<bool> { Status = true, Message = "No note found!", Data = res});
        }

        [HttpGet("GetAllNotes")]
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

        [HttpGet("GetAllOfNotes")]
        public IActionResult GetAllOfNotes()
        {
            var notes = noteBuisness.GetAllOfNotes();
            if (notes != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "get all of Notes succesfully", Data = notes });
            }
            return BadRequest(new ResponseModel<List<NoteEntity>> { Status = true, Message = "get all of notes failed!", Data = notes });
        }

        [AllowAnonymous]
        [HttpGet("GetAllOfCachedNotes")]
        public async Task<IActionResult> GetAllOfCachedNotes()
        {

            var cacheKey = "NotesList";
            string serializedNotes;
            List<NoteEntity> notes;
            var RedisNoteList = await distributedCache.GetAsync(cacheKey);
            if(RedisNoteList!=null)
            {
                serializedNotes = Encoding.UTF8.GetString(RedisNoteList);
                notes = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNotes);
            }
            else
            {
                notes = noteBuisness.GetAllOfNotes();
                serializedNotes = JsonConvert.SerializeObject(notes);
                RedisNoteList = Encoding.UTF8.GetBytes(serializedNotes);
                var options =  new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));
                await distributedCache.SetAsync(cacheKey,RedisNoteList, options);
            }
            return Ok(notes);

        }



        [HttpPut("TooglePin")]
        public IActionResult TooglePin(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var notePin = noteBuisness.TooglePin(NoteId, UserId);
            if (notePin)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Pinned succesfully", Data =  notePin});
            }
            return Ok(new ResponseModel<bool> { Status = true, Message = "Note Unpinned succesfully", Data = notePin});
        }

        [HttpPut("ToogleArchive")]
        public IActionResult ToogleArchive(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var noteArchive = noteBuisness.ToogleArchive(NoteId, UserId);
            if (noteArchive)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Archive succesfully", Data = noteArchive });
            }
            return Ok(new ResponseModel<bool> { Status = true, Message = "Note Unarchived succesfully", Data = noteArchive });
        }
        [HttpPut("ToogleTrash")]
        public IActionResult ToogleTrash(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var noteTrash = noteBuisness.ToogleTrash(NoteId, UserId);
            if (noteTrash)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Trashed succesfully", Data = noteTrash });
            }
            return Ok(new ResponseModel<bool> { Status = true, Message = "Note UnTrashed succesfully", Data = noteTrash });
        }

    }
}
