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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBuisness labelBuisness;

        public LabelController(ILabelBuisness labelBuisness) : base()
        {
            this.labelBuisness = labelBuisness;
        }

        [HttpPost("AddLabel")]
        public IActionResult AddLabel(LabelModel model)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var label = labelBuisness.AddLabel(model, UserId);
            if (label != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "label Added succesfully", Data = label });
            }
            return BadRequest(new ResponseModel<LabelEntity> { Status = true, Message = "label Adding failed!", Data = label });
        }

        [HttpPost("UpdateLabelName")]
        public IActionResult UpdateLabelName(string newLabelName,long LabelId,long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var label = labelBuisness.UpdateLabelName(newLabelName,LabelId,NoteId,UserId);

            if (label != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "label updated succesfully", Data = label });
            }
            return BadRequest(new ResponseModel<LabelEntity> { Status = true, Message = "label update failed!", Data = label });
        }

        [HttpPost("DeleteLabel")]
        public IActionResult DeleteLabel(long LabelId, long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var res = labelBuisness.DeleteLabel(LabelId, NoteId, UserId);

            if (res)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "label delted succesfully", Data = res});
            }
            return BadRequest(new ResponseModel<bool> { Status = true, Message = "label delete failed!", Data = res });
        }

        [HttpPost("GetLabel")]
        public IActionResult GetLabel(long LabelId, long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var label = labelBuisness.GetLabel(LabelId, NoteId, UserId);

            if (label != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "label get succesfully", Data = label });
            }
            return BadRequest(new ResponseModel<LabelEntity> { Status = true, Message = "label get failed!", Data = label });
        }


        [HttpGet("GetAllLabels")]
        public IActionResult GetAllLabels(long NoteId)
        {
            long UserId = Int64.Parse(User.Claims.FirstOrDefault(r => r.Type == "UserId").Value);
            var labels = labelBuisness.GetAllLabels(NoteId,UserId);
            if (labels != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Status = true, Message = "labels get succesfully", Data = labels });
            }
            return BadRequest(new ResponseModel<List<LabelEntity>> { Status = true, Message = "labels get failed!", Data = labels });
        }
    }
}
