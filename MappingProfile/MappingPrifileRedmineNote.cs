using AutoMapper;
using DTOLibrary;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapping
{
    public class MappingProfileRedmineNote : Profile
    {
        public MappingProfileRedmineNote()
        {
            CreateMap<NoteDTO, NoteModel>();
            CreateMap<IssueDTO, IssueModel>();
        }
    }
}
