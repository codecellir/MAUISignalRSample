using MAUISignalR.Server;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapPost("/api/send", async (MessageDto message, IHubContext<ChatMessage> hub) =>
{
    await hub.Clients.All.SendAsync("MessageReceived", message.Text);

    return Results.Ok("message sended");
});

app.MapHub<ChatMessage>("/chat");
app.Run();
