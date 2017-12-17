using System;
using System.Security.Principal;
using Economy.Dtos;

namespace Economy.Models
{
    [Serializable]
    public class UserPrincipal : GenericPrincipal
    {
        private static readonly UserPrincipal EmptyUser = new UserPrincipal();

        public static UserPrincipal Empty => EmptyUser;

        public int Id => Dto.Id;

        public string Name => Dto.Name;

        public SystemUserDto Dto { get; private set; }

        public static UserPrincipal CurrentUser
        {
            get
            {
                UserPrincipal principal = System.Threading.Thread.CurrentPrincipal as UserPrincipal;

                if (principal == null)
                {
                    return Empty;
                }
                return principal;
            }
            set { System.Threading.Thread.CurrentPrincipal = value; }
        }

        public static bool IsAuthenticated
        {
            get { return CurrentUser != null && CurrentUser != Empty; }
        }


        private UserPrincipal()
            : base(new GenericIdentity(string.Empty), new string[0])
        {
        }

        private UserPrincipal(SystemUserDto dto)
          : base(new GenericIdentity(dto.Login), new string[0])
        {

            Dto = dto;
        }

        public static UserPrincipal CreatePrincipal(SystemUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new UserPrincipal(dto);
        }
    }
}