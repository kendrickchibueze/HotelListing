namespace HotelListing.DTO.Response
{
    public class UserManagerResponse
    {
            public string Message { get; set; }
            public bool IsSuccess { get; set; }
            public IEnumerable<string> Errors { get; set; }
            public string? Token { get; set; }
            public string? RefreshToken { get; set; }
    }
}
