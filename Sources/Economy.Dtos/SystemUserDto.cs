using System;

namespace Economy.Dtos
{
    public partial class SystemUserDto
    {
        public bool IsNew
        {
            get { return Id == Guid.Empty; }
        }
    }
}