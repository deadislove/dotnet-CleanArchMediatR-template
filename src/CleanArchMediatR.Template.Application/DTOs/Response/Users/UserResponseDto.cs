namespace CleanArchMediatR.Template.Application.DTOs.Response.Users
{
    public class UserResponseDto
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string userName { get; set; } = string.Empty;
        private string _password;

        public string password
        {
            get => new string('*', 12);  
            set => _password = value;
        }
    }
}
