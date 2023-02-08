using PersonService.BLL.DTO;
using PersonService.Common.Security;

namespace PersonService.BLL.Contract
{
    public interface IUserService
    {
        bool IsValidUser(string userName, string password);
        bool HasAccessPoint(string userName, AccessPoint accessPoint);
    }
}