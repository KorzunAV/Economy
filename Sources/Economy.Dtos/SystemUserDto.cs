using System;

namespace Economy.Dtos
{
    public class SystemUserDto : BaseDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public string Name { get; set; }

        public bool IsNew
        {
            get { return Id == Guid.Empty; }
        }
    }
}