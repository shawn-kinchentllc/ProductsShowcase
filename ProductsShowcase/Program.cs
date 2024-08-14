using Helper = ProductsShowcase.Library.ProductsShowcaseHelper;

class Program
{
    static async Task Main(string[] args)
    {
        var productList = await Helper.GetAllProducts();
        if (productList == null)
        {
            Console.WriteLine("No data found from call to retrieve products");
            return;
        }
    }
}
