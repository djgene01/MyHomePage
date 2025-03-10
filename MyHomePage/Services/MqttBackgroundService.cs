
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using M2Mqtt;
using M2Mqtt.Messages;
using Microsoft.AspNetCore.SignalR;

public class MqttBackgroundService : BackgroundService
{
    private readonly IHubContext<MqttHub> _hubContext;
    private MqttClient _mqttClient;

    public MqttBackgroundService(IHubContext<MqttHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _mqttClient = new MqttClient("192.168.1.29", 1883, false, null, null, MqttSslProtocols.None);
        _mqttClient.Connect("BlazorClient");

        if (_mqttClient.IsConnected)
        {
            _mqttClient.Subscribe(new string[] { "solar_assistant/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            _mqttClient.MqttMsgPublishReceived += (sender, e) =>
            {
                var topic = e.Topic;
                var payload = Encoding.UTF8.GetString(e.Message);
                _hubContext.Clients.All.SendAsync("ReceiveMqttData", topic, payload);
                //Console.WriteLine($"Topic: {topic}, Payload: {payload}");
            };
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        if (_mqttClient.IsConnected)
        {
            _mqttClient.Disconnect();
        }
    }
}