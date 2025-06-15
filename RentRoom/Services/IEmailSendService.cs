
namespace RentRoom.Services
{
    public interface IEmailSendService
    {
        Task sendAsync(string Email, string code);
    }
}