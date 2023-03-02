using ClassLib.Data;
using ClassLib.Interface;

namespace ClassLib.Logic
{
    public class Ticket
    {
        public PersistData<TicketObject> ticket;
        
        public Ticket(PersistData<TicketObject> ticket)
        {
            this.ticket = ticket;
        }
        public TicketObject data { get; }

        public Ticket(TicketObject ticketDto)
        {
            data = ticketDto;
        }
        public int ticketId => data.ticketId;
        public int customerId => data.customerId;
        public int flightId => data.customerId;
        public string travelType => data.travelType;
        public string classType => data.classType;
        public int numberOfAdults => data.numberOfAdults;
        public int numberOfChildren => data.numberOfChildren;

        public void saveTicket(Ticket ticket) => this.ticket.save(ticket.data);
        public int saveTicketAndRetrieveId(Ticket ticket) => this.ticket.saveGetId(ticket.data);
        public void deleteTicket(int id) => this.ticket.delete(id);
        public void updateTicket(Ticket ticket) => this.ticket.update(ticket.data);
    }
}

