namespace CategoriaConsumer.request;

public class Request :IRequest
{
    private string url = "https://localhost:7240/configuracaosite/adicionar";

    public async void Post(string body)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        var content = new StringContent(body,null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
    }

}
