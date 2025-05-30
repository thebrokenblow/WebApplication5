namespace WebApplication5.ViewModels;

public class PageViewModel
{
    public PageViewModel(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPage  = (int)Math.Ceiling(count / (double)pageSize);
    }
    public int PageNumber { get; set; }
    public int TotalPage { get; set; }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPage;
}