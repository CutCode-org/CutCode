

using System.Drawing;

namespace CutCode.CrossPlatform.Models
{
    public class DevAboutModel
    {
        public DevAboutModel(string profilePic, string name, string userName, string github, string twitter)
        {
            ProfilePic = profilePic;
            Name = name;
            UserName = userName;
            Github = github;
            Twitter = twitter;
        }
        
        public string ProfilePic { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Github { get; set; }
        public string Twitter { get; set; }
    }
}