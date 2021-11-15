

using System.Drawing;

namespace CutCode.CrossPlatform.ViewModels
{
    public class DeveloperCardViewModel : ViewModelBase
    {
        public DeveloperCardViewModel(string profilePic, string name, string desc,string userName, string github, string twitter)
        {
            ProfilePic = profilePic;
            Name = name;
            Desc = desc;
            UserName = userName;
            Github = github;
            Twitter = twitter;
        }
        
        public string ProfilePic { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string UserName { get; set; }
        public string Github { get; set; }
        public string Twitter { get; set; }
    }
}