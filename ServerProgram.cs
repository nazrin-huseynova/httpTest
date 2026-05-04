

using System.Net;
using System.Text;
using System.Text.Json;
using HttpServerTest.Models;

var listener = new HttpListener();
listener.Prefixes.Add("-------");
listener.Start();
Console.WriteLine("Server started on ---------");

List<Product> Products = new() 
{
 new Product
 {
     Name = "Samsung Galaxy",
     Price = 718,
     Quantitiy = 1
 },
 new Product
 {
     Name = "Samsung J3",
     Price = 1550,
     Quantitiy = 2
 },
 new Product
 {
     Name = "Iphone 13 Pro Max ",
     Price = 3500,
     Quantitiy=1
 }
};

while (true)
{
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;

    if (request.HttpMethod == "GET")
    {
        var responseText = " Hello from Server (GET)";
        string jsonString = JsonSerializer.Serialize(Products);
        Console.WriteLine("All products were sent");
        var data = Encoding.UTF8.GetBytes(jsonString);
        response.ContentType = " application/json";
        await response.OutputStream.WriteAsync(data, 0, data.Length);
    }
    else if (request.HttpMethod == "POST")
    {
        using var reader = new StreamReader(request.InputStream);
        var body = await reader.ReadToEndAsync();
        var result = JsonSerializer.Deserialize<Product>(body);
        
        Console.WriteLine($"Recieved from client : {result.Id}{result.Name}, {result.Price}, { result.Quantitiy}");
        Products.Add(result);

        var responseText = $"\"Succesfully added";
        var data = Encoding.UTF8.GetBytes(responseText);
        response.ContentType = "application/json";
        await response.OutputStream.WriteAsync(data);
    }
    response.Close();
}
