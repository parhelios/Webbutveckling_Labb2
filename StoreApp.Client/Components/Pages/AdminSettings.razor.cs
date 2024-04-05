using StoreApp.Shared.Dtos.OrderDtos;
using StoreApp.Shared.Dtos.ProductDtos;
using StoreApp.Shared.Dtos.UserDtos;

namespace StoreApp.Client.Components.Pages;

public partial class AdminSettings
{
    private string _productName;
    private string _productDescription;
    private string _productPrice;
    private string _productImageUrl;
    private string _selectedCategory;
    private string _categoryName;

    private ProductDto _selectedItemProduct = null;
    private UserDto _selectedItemUser = null;
    private OrderDto _selectedItemOrder = null;

    private string _searchStringProduct = "";
    private string _searchStringUser = "";
    private string _searchStringOrder = "";

    public ProductDto _product = new ProductDto();
    public List<ProductCategoryDto> AllProductCategories { get; set; } = [];
    public List<ProductDto> AllProducts { get; set; } = [];
    public List<UserDto> AllUsers { get; set; } = [];
    public List<OrderDto> AllOrders { get; set; } = [];

    public Dictionary<int, string> AllCategoriesDictionary { get; set; } = [];
    protected override async Task OnInitializedAsync()
    {
        AllProductCategories.AddRange(await CategoryService.GetAllAsync());
        AllProducts.AddRange(await ProductService.GetAllAsync());
        AllUsers.AddRange(await UserService.GetAllAsync());
        AllCategoriesDictionary = AllProductCategories.ToDictionary(c => c.Id, c => c.Name);
        AllOrders.AddRange(await UserService.GetAllOrders());
    }

    private bool FilterFuncProduct1(ProductDto productDto) => FilterFuncProduct(productDto, _searchStringProduct);

    private bool FilterFuncProduct(ProductDto productDto, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (productDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (productDto.Id.ToString().Contains(searchString))
            return true;

        return false;
    }

    private bool FilterFuncUser1(UserDto userDto) => FilterFuncUser(userDto, _searchStringUser);

    private bool FilterFuncUser(UserDto userDto, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (userDto.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }
    private bool FilterFuncOrder1(OrderDto orderDto) => FilterFuncOrder(orderDto, _searchStringOrder);

    private bool FilterFuncOrder(OrderDto orderDto, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (orderDto.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    private async Task PopulateProducts()
    {
        AllProducts.Clear();
        AllProducts.AddRange(await ProductService.GetAllAsync());
    }

    private async void AddProduct()
    {
        var category = AllProductCategories.FirstOrDefault(c => c.Name == _selectedCategory).Id;
        var priceParse = double.TryParse(_productPrice, out double productPrice);

        if (!priceParse)
        {
            return;
        }

        var newProduct = new CreateProductDto
        {
            Name = _productName,
            Description = _productDescription,
            Price = productPrice,
            Category = category,
            ImageUrl = _productImageUrl
        };

        await ProductService.AddNewProductAsync(newProduct);

        await PopulateProducts();
    }

    private async void AddCategory()
    {
        var newCategory = new ProductCategoryDto
        {
            Name = _categoryName
        };

        await CategoryService.AddAsync(newCategory);
    }

    private async Task DeleteProductAsync(int contextId)
    {
        await ProductService.DeleteAsync(contextId);

        await PopulateProducts();
    }

    private async void ToggleProductStatus(int productId)
    {
        await ProductService.ToggleProductStatusAsync(productId);

        await PopulateProducts();
    }

    private async Task EditProduct(int contextId)
    {
        _product = await ProductService.GetByIdAsync(contextId);
    }

    private async Task UpdateProduct()
    {
        if (_product.Name is null)
        {
            return;
        }

        await ProductService.UpdateAsync(_product.Id, _product);
        await PopulateProducts();
    }

    private object GetCategoryName(int productCategory)
    {
        if (AllCategoriesDictionary.ContainsKey(productCategory))
        {
            return AllCategoriesDictionary[productCategory];
        }
        else
        {
            return "Unknown Category";
        }
    }

    private async Task ShowBtnPress(int orderId)
    {
        var showOrder = AllOrders.First(o => o.Id == orderId);

        showOrder.ShowDetails = !showOrder.ShowDetails;
    }
}