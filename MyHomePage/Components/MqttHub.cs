using Microsoft.AspNetCore.SignalR;

public class MqttHub : Hub
{
    public async Task SendMqttData(string topic, string payload)
    {
        await Clients.All.SendAsync("ReceiveMqttData", topic, payload);
    }
}