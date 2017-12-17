//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class SystemUserSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("B542A966-76A6-49BE-A25D-9A3AB7D2EF20");

        public override Guid CommandId => Id;

        public List<SystemUserDto> Dtos { get; set; }

		public SystemUserSaveListCommand(List<SystemUserDto> dtos)
        {
            Dtos = dtos;
        }
    }
}