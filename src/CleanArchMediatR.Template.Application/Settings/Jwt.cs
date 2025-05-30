namespace CleanArchMediatR.Template.Application.Settings
{
    public class Jwt
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set;} = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string RefreshKey {  get; set; } = string.Empty;
    }
}
