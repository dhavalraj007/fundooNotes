using BuisnessLogic.Interfaces;
using BuisnessLogic.Services;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace fundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IUserBuisness userBuisness;

        public TicketController(IBus bus, IUserBuisness userBuisness):base()
        {
            _bus = bus;
            this.userBuisness = userBuisness;
        }

        [HttpPost("CreateTicketForPassword")]
        public async Task<IActionResult> CreateTicketForPassword(string email)
        {
            var token = userBuisness.ForgetPassword(email);
            if (token != null)
            {
                var ticket = userBuisness.CreateTicketForPassword(email, token);
                Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(ticket);
                return Ok(new { status = true, message = "Mail sent succesfully" });
            }
            else
                return BadRequest(new { status = false, message = "Mail not sent" });
        }
    }
}
