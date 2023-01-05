namespace HotelManagement.ViewModels
{
    public class BookingSearchViewModel
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int MaxAdult { get; set; }
        public int MaxChild { get; set; }
    }
}
