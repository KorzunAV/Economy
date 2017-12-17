//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class CourseArhiveSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("17D0DF83-C85B-4353-A44A-3A8021BFE576");

        public override Guid CommandId => Id;

        public CourseArhiveDto Dto { get; set; }

		public CourseArhiveSaveCommand(CourseArhiveDto dto)
        {
            Dto = dto;
        }
    }
}