namespace CategoriaConsumer.request;

public class Request :IRequest
{
    private string url = "http://localhost:3000/conf-site";

    public async void Post(string body)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        var content = new StringContent(body);
        request.Content = content;
        await client.SendAsync(request);
    }

}
