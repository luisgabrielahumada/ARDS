using ARDZ.Filter;

namespace ARDZ
{
    [SwaggerExclude]
    public class LoginModelDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
}
