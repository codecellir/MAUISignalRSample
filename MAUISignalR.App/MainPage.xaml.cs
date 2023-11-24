using Microsoft.AspNetCore.SignalR.Client;

namespace MAUISignalR.App
{
    public partial class MainPage : ContentPage
    {
        private readonly HubConnection _connection;
        public MainPage()
        {
            InitializeComponent();

            var signalRAddress = DeviceInfo.Platform==DevicePlatform.Android
                ? "http://10.0.2.2:5043/chat"
                : "http://localhost:5043/chat";

            _connection = new HubConnectionBuilder()
                .WithUrl(signalRAddress)
                .Build();

            _connection.On<string>("MessageReceived", message =>
            {
                Dispatcher.Dispatch(() =>
                {
                    chatMessage.Text += $"{Environment.NewLine}{message}";
                });
            });

            Task.Run(() =>
            {
                Dispatcher.Dispatch(async () =>
                {
                    try
                    {
                        await _connection.StartAsync();
                        connectionStatus.Text = "Connected!";
                        connectionStatus.TextColor = Colors.Green;
                    }
                    catch (Exception)
                    {
                        connectionStatus.Text = "disConnected!";
                        connectionStatus.TextColor = Colors.Red;
                    }
                });
            });
        }

        private async void sendBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await _connection.InvokeCoreAsync("SendMessage", new[] {chatEntry.Text});

            chatEntry.Text= string.Empty;
        }
    }

}
