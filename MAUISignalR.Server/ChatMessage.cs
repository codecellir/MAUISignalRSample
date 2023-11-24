using Microsoft.AspNetCore.SignalR;

public class ChatMessage : Hub
{
    public async Task SendMessage(string message)
    {
        Console.WriteLine(message);

        await Clients.Others.SendAsync("MessageReceived", message);
    }
}