//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class CourseArhiveSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("4B887F1B-495A-4DC6-8F03-82CBE630639F");

        public override Guid CommandId => Id;

        public List<CourseArhiveDto> Dtos { get; set; }

		public CourseArhiveSaveListCommand(List<CourseArhiveDto> dtos)
        {
            Dtos = dtos;
        }
    }
}