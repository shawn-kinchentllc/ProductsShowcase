using Newtonsoft.Json;

namespace ProductsShowcase.Library;

public static class ProductsShowcaseHelper
{
    private const string baseUrl = "https://dummyjson.com/products";

    public static async Task<IList<Product>> GetAllProductsAsync()
    {
        var products = new List<Product>();
        string apiUrl = baseUrl;
        
        var responseBody = await CallApi(apiUrl);
        // Return if no data is found in body
        if (string.IsNullOrEmpty(responseBody)) return products;

        dynamic dataObj = JsonConvert.DeserializeObject(responseBody);
        foreach (var item in dataObj.products)
        {
            products.Add(CreateProduct(item));
        }

        return products;
    }

    public static async Task<IList<Product>> GetProductByIdAsync(string id)
    {
        var products = new List<Product>();
        string apiUrl = $"{baseUrl}/{id}";

        var responseBody = await CallApi(apiUrl);
        // Return if no data is found in body
        if (string.IsNullOrEmpty(responseBody)) return products;

        dynamic dataObj = JsonConvert.DeserializeObject(responseBody);
        products.Add(CreateProduct(dataObj));

        return products;
    }

    public static async Task<IList<Product>> GetProductsBySearchValueAsync(string value)
    {
        var products = new List<Product>();
        string apiUrl = $"{baseUrl}/search?q={value}";
        
        var responseBody = await CallApi(apiUrl);
        // Return if no data is found in body
        if (string.IsNullOrEmpty(responseBody)) return products;

        dynamic dataObj = JsonConvert.DeserializeObject(responseBody);
        foreach (var item in dataObj.products)
        {
            products.Add(CreateProduct(item));
        }

        return products;
    }

    private static async Task<string> CallApi(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode) return string.Empty;

            string responseBody = await response.Content.ReadAsStringAsync();
            
            return responseBody;
        }
    }

    private static Product CreateProduct(dynamic obj)
    {
        return new Product
        {
            Id = obj.id,
            Title = obj.title,
            Description = obj.description,
            Price = obj.price
        };
    }
}
