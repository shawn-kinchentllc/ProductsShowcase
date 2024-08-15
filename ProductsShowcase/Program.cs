using ProductsShowcase;
using Helper = ProductsShowcase.Library.ProductsShowcaseHelper;

class Program
{
    static async Task Main(string[] args)
    {
        var userInput = string.Empty;
        var productList = new List<Product>();

        try
        {
            Console.WriteLine("Press enter to see a sample list of products");
            Console.WriteLine("Type 'skip' to continue");
            userInput = Console.ReadLine();
            if (userInput.ToLower().Trim() != "skip")
            {
                productList = (await Helper.GetAllProductsAsync()).ToList();
                WriteToConsole(productList);
            }
            

            Console.WriteLine("Enter a Product ID to see an individual product");
            Console.WriteLine("Press Enter to contine");
            userInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                var shouldCallApi = true;
                var inputIsNumeric = ValueIsInteger(userInput);
                while (!inputIsNumeric)
                {
                    Console.WriteLine("Please enter a numeric Product ID to return a product");
                    Console.WriteLine("Press Enter to cancel and continue");
                    userInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        shouldCallApi = false;
                        break;
                    }
                    inputIsNumeric = ValueIsInteger(userInput);
                }

                if (shouldCallApi)
                {
                    productList = (await Helper.GetProductByIdAsync(userInput)).ToList();
                    WriteToConsole(productList);
                }
            }
            

            Console.WriteLine("Search Products where the Title and/or Description contain your search value");
            Console.WriteLine("Press Enter to continue");
            userInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                productList = (await Helper.GetProductsBySearchValueAsync(userInput)).ToList();
                WriteToConsole(productList);
            }

            Console.WriteLine("End of Program");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving Product data:");
            Console.WriteLine(ex);
        }
        
    }

    private static bool ValueIsInteger(string value)
    {
        return int.TryParse(value, out int result);
    }

    private static void WriteToConsole(IList<Product> products)
    {
        if (!products.Any())
        {
            Console.WriteLine("No data found from call to retrieve products");
            Console.WriteLine();
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"Product Id: {product.Id}");
            Console.WriteLine($"Product Title: {product.Title}");
            Console.WriteLine($"Product Description: {product.Description}");
            Console.WriteLine($"Product Price: {product.Price}");
            Console.WriteLine();
        }
    }
}
