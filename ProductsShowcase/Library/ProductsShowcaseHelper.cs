using System;
using Newtonsoft.Json;

namespace ProductsShowcase.Library;

public static class ProductsShowcaseHelper
{
    public static async Task<IList<Product>?> GetAllProducts()
    {
        string productsUrl = "https://dummyjson.com/products";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(productsUrl);
            // Throw error if not successful
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            // Return null if no data is found in body
            if (string.IsNullOrEmpty(responseBody)) return null;

            dynamic dataObj = JsonConvert.DeserializeObject(responseBody);
            var products = new List<Product>();
            foreach (var item in dataObj.products)
            {
                var product = new Product
                {
                    Id = item.id,
                    Title = item.title,
                    Description = item.description,
                    Price = item.price
                };

                products.Add(product);
            }

            return products.Any() ? products : null;
        }
    }
}
