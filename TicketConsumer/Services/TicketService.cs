using MassTransit;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace TicketConsumer.Services
{
    public class TicketService:IConsumer<Model.UserTicket>
    {
        public async Task Consume(ConsumeContext<Model.UserTicket> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
