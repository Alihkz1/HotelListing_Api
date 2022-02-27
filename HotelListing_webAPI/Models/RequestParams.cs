namespace HotelListing_webAPI.Models
{
    public class RequestParams
    {
        const int maxPageSize = 100;
        public int PageIndex { get; set; } = 1;
         private int _pageSize = 10;
        public int PageSize {
            get 
            {
                return _pageSize; 
            } 
            set
            { 
                _pageSize = (value > maxPageSize) ? maxPageSize : value ;
            }
        }
    }
}
