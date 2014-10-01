using System.Dynamic;

namespace Game.Shell.Services
{
    public interface IUserInfoService
    {
        string GetUserName();
        string GetUserProfileImagePath();
    }
}
