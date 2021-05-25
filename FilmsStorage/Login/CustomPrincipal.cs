using System.Security.Principal;

// клас для опису моделі користувача 
namespace FilmsStorage.Login
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public int UserID { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }
        public CustomPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }
    }
}