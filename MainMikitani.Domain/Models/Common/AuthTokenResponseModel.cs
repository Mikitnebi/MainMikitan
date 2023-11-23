namespace MainMikitan.Domain.Models.Common;

public record AuthTokenResponseModel
{
    public string AccessToken { get; set; }
    //public string RefreshToke { get; set; }
}
