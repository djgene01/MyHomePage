using Microsoft.AspNetCore.SignalR.Client;

namespace MyHomePage.Services
{
    public class SolarDataService : IDisposable
    {
        public string ServerUrl { get; private set; }
        private HubConnection? _hubConnection;
        private readonly Dictionary<string, string> _solarData = new();
        private readonly Func<Task> _updateCallback; // Callback to trigger UI updates safely

        public bool IsConnected { get; private set; }
        public bool IsConnecting { get; private set; }
        public string ErrorMessage { get; private set; } = "";

        public SolarDataService(string serverUrl = "https://localhost:7159/", Func<Task> updateCallback = null)
        {
            ServerUrl = serverUrl;
            _updateCallback = updateCallback ?? (() => Task.CompletedTask); // Default to no-op if null
        }

        public async Task ConnectAsync()
        {
            if (string.IsNullOrEmpty(ServerUrl))
            {
                ErrorMessage = "Please provide a server URL.";
                Console.WriteLine("Error: Server URL is empty.");
                return;
            }

            try
            {
                IsConnecting = true;
                ErrorMessage = "";
                await _updateCallback();

                if (_hubConnection != null)
                {
                    await _hubConnection.DisposeAsync();
                }

                Console.WriteLine($"Attempting to connect to {ServerUrl}/mqtthub...");
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl($"{ServerUrl.TrimEnd('/')}/mqtthub")
                    .ConfigureLogging(logging =>
                    {
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Debug);
                    })
                    .Build();

                _hubConnection.On<string, string>("ReceiveMqttData", async (topic, payload) =>
                {
                    _solarData[topic] = payload;
                    await _updateCallback(); // Use callback instead of direct event
                });

                _hubConnection.Closed += async (error) =>
                {
                    Console.WriteLine($"Hub connection closed: {error?.Message}");
                    IsConnected = false;
                    IsConnecting = false;
                    await _updateCallback();
                };

                await _hubConnection.StartAsync();
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    Console.WriteLine("Connected to SignalR hub.");
                    IsConnected = true;
                    IsConnecting = false;
                }
                else
                {
                    throw new Exception("Failed to establish connection to the hub.");
                }
                await _updateCallback();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Connection failed: {ex.Message}";
                Console.WriteLine($"Connection error: {ex.Message}");
                IsConnected = false;
                IsConnecting = false;
                await _updateCallback();
            }
        }

        public string GetSolarValue(string key)
        {
            return _solarData.TryGetValue($"solar_assistant/{key}", out var value) ? value : "0";
        }

        public void Dispose()
        {
            _hubConnection?.DisposeAsync().AsTask().Wait();
        }
    }
}