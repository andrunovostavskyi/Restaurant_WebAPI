namespace Restaurants.Application.Common;

public class ResultPage<T>
{
    public ResultPage(IEnumerable<T> items, int pageNumber, int pageSize, int totalItems)
    {
        ListItems = items;
        TotalPages = pageNumber;
        TotalItemsCount = totalItems;
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
    }
    public IEnumerable<T>? ListItems{ get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
