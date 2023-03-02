namespace ClassLib.Interface
{
    public class TicketObject
    {
        public int ticketId { get; set; }
        public int customerId { get; set; }
        public int flightId { get; set; }
        public string travelType { get; set; }
        public string classType { get; set; }
        public int numberOfAdults { get; set; }
        public int numberOfChildren { get; set; }
    }
}
