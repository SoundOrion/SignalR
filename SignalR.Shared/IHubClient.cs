public interface IHubClient
{
    Task ReceiveMessage(string message);
    Task ReceiveResult(string result);
}
