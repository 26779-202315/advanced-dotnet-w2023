using static Week10_4_ChainOfResponsibility.Ex.SupportTicket;
using Week10_4_ChainOfResponsibility.Ex;

namespace Week10_4_ChainOfResponsibility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting 'ChainOfResponsibility' Design Pattern Application!");

            //Prepare our chain of responsibility
            Tier1 Max = new Tier1();
            Tier2 Jane = new Tier2();
            Tier3 John = new Tier3();
            Tier4 Alex = new Tier4();

            Max.SetSuccessor(Jane);
            Jane.SetSuccessor(John);
            John.SetSuccessor(Alex);

            //Simulate different tickets

            //Basic Ticket
            SupportTicket sampleTicket = new SupportTicket(1, TicketType.Basic);
            Max.HandleRequest(sampleTicket);

            //InDepth Ticket
            sampleTicket = new SupportTicket(2, TicketType.InDepth);
            Max.HandleRequest(sampleTicket);

            //Advanced Ticket
            sampleTicket = new SupportTicket(3, TicketType.Advanced);
            Max.HandleRequest(sampleTicket);

            //Vendor Ticket
            sampleTicket = new SupportTicket(4, TicketType.Vendor);
            Max.HandleRequest(sampleTicket);

            //Unknown Ticket
            sampleTicket = new SupportTicket(5, TicketType.Unknown);
            Max.HandleRequest(sampleTicket);

        }

    }
}