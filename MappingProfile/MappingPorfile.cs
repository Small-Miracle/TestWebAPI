using DTOLibrary;
using ModelsLibrary;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MergeHookDTO, MergeHookM>()
                   .ForMember(dest => dest.ObjectKind, act => act.MapFrom(src => src.Object_kind))
                   .ForMember(dest => dest.EventType, act => act.MapFrom(src => src.Event_type))
                   .ForMember(dest => dest.ObjectAttributes, act => act.MapFrom(src => src.Object_Attributes));
            CreateMap<Object_attributesDTO, ObjectAttributesM>();

            CreateMap<MergeParamsDTO, MergeParamsM>()
                .ForMember(dest => dest.ForceRemoveSourceBranch, act => act.MapFrom(src => src.Force_remove_source_branch));


            CreateMap<Last_commitDTO, LastCommitM>();
            CreateMap<AuthorDTO, AuthorM>();

            CreateMap<UserDTO, UserM>();

            CreateMap<ProjectDTO, ProjectM>();

            CreateMap<ChangesDTO, ChangesM>()
                     .ForMember(dest => dest.UpdatedAt, act => act.MapFrom(src => src.Updated_at));
                    
            CreateMap<Updated_atDTO, UpdatedAtM>();
            CreateMap<State_idDTO, StateIdM>();
            CreateMap<RepositoryDTO, RepositoryM>();

        }
    }
}
