namespace PracticeCore.Services
{
    public interface IUserService
    {
        string getUserId();
        bool IsAuthenticated();
    }
}