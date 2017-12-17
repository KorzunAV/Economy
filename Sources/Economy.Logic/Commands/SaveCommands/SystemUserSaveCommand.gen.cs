//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class SystemUserSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("583495CC-43F5-4AF4-80A4-0E32B475456D");

        public override Guid CommandId => Id;

        public SystemUserDto Dto { get; set; }

		public SystemUserSaveCommand(SystemUserDto dto)
        {
            Dto = dto;
        }
    }
}