using HttpClientTest.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

const string url = "-----------";
var client = new HttpClient();
Console.WriteLine("1.Get Request");
Console.WriteLine("2.Post Request");
List<Product> Products = new List<Product>();
var choice = Console.ReadLine();

if (choice == "1")
{
    var result = await client.GetStringAsync(url);
    Console.WriteLine("Server response");
    var products = JsonSerializer.Deserialize<List<Product>>(result);
    Console.WriteLine(result);
    
}
else if (choice == "2")
{
    Console.WriteLine("Enter Product properties: ");
    Product product = new Product();
    Console.WriteLine("Enter name:");
    product.Name = Console.ReadLine();
    Console.WriteLine("Enter price:");
    product.Price =int.Parse( Console.ReadLine()!);
    Console.WriteLine("Enter quantitiy:");
    product.Quantitiy =int.Parse( Console.ReadLine()!);
    var jsonString = JsonSerializer.Serialize(product);
    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

    var response = await client.PostAsync(url, content);
    var result = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Server Response");
    Console.WriteLine(result);
}

