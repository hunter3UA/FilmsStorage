
using System.Security.Principal;

namespace FilmsStorage.Login
{
    interface ICustomPrincipal:IPrincipal
    {
        int UserID { get; set; }
    }
}
