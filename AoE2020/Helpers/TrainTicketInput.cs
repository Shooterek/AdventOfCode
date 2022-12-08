using System.Collections.Generic;

public class TrainTicketInput{
    public TrainTicketInput()
    {
        TicketRules = new List<TicketRule>();
        MyTicket = new List<int>();
        NearbyTickets = new List<List<int>>();
    }
    public List<TicketRule> TicketRules { get; set; }
    public List<int> MyTicket { get; set; }
    public List<List<int>> NearbyTickets { get; set; }
}