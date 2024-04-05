using StoreApp.Shared.Dtos.ProductDtos;

namespace StoreApp.Client.Components.Pages;

public partial class Store
{
    public string SearchInputText { get; set; }
    public ProductDto ProductDto { get; set; } = new();
    public List<ProductDto> StoreProductDtos { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var allProducts = await ProductService.GetAllAsync();
        var sortedProductsStatus = allProducts.Where(p => p.Status == true);

        var sortedProductsStock = sortedProductsStatus.Where(p => p.Stock > 1);
        StoreProductDtos.AddRange(sortedProductsStock);
    }

    private async Task AddToCart(ProductDto product)
    {
        if (!ShoppingCartService.UserCart.TryGetValue(product, out int amount))
        {
            ShoppingCartService.UserCart[product] = 1;
        }
        else
        {
            ShoppingCartService.UserCart[product] = amount + 1;
        }
    }

    private async Task SearchProducts()
    {
        if (SearchInputText == "")
        {
            return;
        }

        var allProducts = await ProductService.GetAllAsync();
        var productSearch = allProducts.Where(p => p.Name.ToLower().Contains(SearchInputText.ToLower()));

        StoreProductDtos.Clear();
        StoreProductDtos.AddRange(productSearch);

        SearchInputText = "";
    }

    private async Task ClearSearchQuery()
    {
        StoreProductDtos.Clear();

        var allProducts = await ProductService.GetAllAsync();
        var sortedProductsStatus = allProducts.Where(p => p.Status == true);

        var sortedProductsStock = sortedProductsStatus.Where(p => p.Stock > 1);
        StoreProductDtos.AddRange(sortedProductsStock);
    }
}