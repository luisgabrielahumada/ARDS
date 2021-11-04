using System;
using ARDZ.Filter;

namespace ARDZ
{
    [SwaggerExclude]
    public class UserModelDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string EmailAddress { get; set; }
        public DateTime TrasactionDate { get; set; }
        public string ProfileId { get; set; }
        public string Section { get; set; }
    }
}
