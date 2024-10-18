namespace ProEventos.Persistence.Models
{
    public class PageParams
    {
        private int _pageSize = 10;

        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int PageSize 
        { 
            get { return _pageSize; } 
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; } 
        }

        public string Term { get; set; } = string.Empty;
    }
}