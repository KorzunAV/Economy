using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands
{
    public class BelinvestCourseArhiveSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("61520B67-92D6-4ED5-BECA-D0A61CE94704");

        public override Guid CommandId
        {
            get { return Id; }
        }

        public List<BelinvestCourseArhiveDto> Dtos { get; set; }
    }
}