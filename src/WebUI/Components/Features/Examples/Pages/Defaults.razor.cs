using System.Security.Cryptography;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Examples.Pages;

public partial class Defaults
{
    private const int _productsCount = 33;
    private IReadOnlyList<ProductItem> _products = [];
    private string? _searchKeyword;

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();

        _products = Enumerable.Range(1, _productsCount).Select(i => new ProductItem
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

    private bool FilterItems(ProductItem item)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (item.Name.Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (item.UnitPrice.ToDisplayText(CurrencyFormatFor.NoDecimal).Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (item.Stock.ToString(CultureInfo.InvariantCulture).Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    private sealed record ProductItem
    {
        public required string Name { get; init; }
        public required decimal UnitPrice { get; init; }
        public required int Stock { get; init; }
    }
}
