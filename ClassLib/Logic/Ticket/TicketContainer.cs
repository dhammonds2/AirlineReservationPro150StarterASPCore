using ClassLib.Data;
using ClassLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib.Logic
{
    public class TicketContainer
    {
        private readonly FitchData<TicketObject> ticketContainer;

        public TicketContainer(FitchData<TicketObject> ticketContainer)
        {
            this.ticketContainer = ticketContainer;
        }      
        public IEnumerable<Ticket> getAllTickets() => ticketContainer.getAll().Select(ticketDto => new Ticket(ticketDto));
        public Ticket getTicketById(int id) => new Ticket(ticketContainer.getById(id));
    }
}

