using System.Security.Cryptography;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Examples.Pages;

public partial class Defaults
{
    private const int _productsCount = 33;
    private IReadOnlyList<ProductModel> _products = [];
    private string? _searchKeyword;

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();

        _products = Enumerable.Range(1, _productsCount).Select(i => new ProductModel
        {
            Name = $"Product {i}",
            UnitPrice = Math.Round(Convert.ToDecimal(RandomNumberGenerator.GetInt32(1000) + 1), 2),
            Stock = RandomNumberGenerator.GetInt32(100) + 1
        }).ToList();
    }

    protected override void LoadBreadcrumbs()
    {
        ClearBreadcrumbs();
        AddBreadcrumb(HomeBreadcrumbFor.Index);
        AddBreadcrumb(ExamplesBreadcrumbFor.Index);
        AddBreadcrumb(ComponentsBreadcrumbFor.Active(ExamplesDisplayTextFor.Defaults));
    }

    private bool FilterItems(ProductModel product)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (product.Name.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (product.UnitPriceDisplayText.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (product.StockDisplayText.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    private sealed record ProductModel
    {
        public required string Name { get; init; }
        public required decimal UnitPrice { get; init; }
        public required int Stock { get; init; }

        public string UnitPriceDisplayText => UnitPrice.ToDisplayText(CurrencyFormatFor.NoDecimal);
        public string StockDisplayText => Stock.ToString(CultureInfo.InvariantCulture);
    }
}
