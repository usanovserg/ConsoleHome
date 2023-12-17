namespace ConsoleHome;

public interface IPositionFactory
{
    Position Create(TicketInfo ticketInfo, AccountInfo accountInfo);
}