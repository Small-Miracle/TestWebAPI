using DTOLibrary;
using ModelsLibrary;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mapping
{
    public class MappingProfileMergeHook : Profile
    {
        public MappingProfileMergeHook()
        {
            CreateMap<MergeHookDTO, MergeHookModel>()
                   .ForMember(dest => dest.ObjectKind, act => act.MapFrom(src => src.Object_kind))
                   .ForMember(dest => dest.EventType, act => act.MapFrom(src => src.Event_type))
                   .ForMember(dest => dest.ObjectAttributes, act => act.MapFrom(src => src.Object_Attributes));
            CreateMap<Object_attributesDTO, ObjectAttributesModel>();

            CreateMap<MergeParamsDTO, MergeParamsModel>()
                .ForMember(dest => dest.ForceRemoveSourceBranch, act => act.MapFrom(src => src.Force_remove_source_branch));


            CreateMap<Last_commitDTO, LastCommitModel>();
            CreateMap<AuthorDTO, AuthorModel>();

            CreateMap<UserDTO, UserModel>();

            CreateMap<ProjectDTO, ProjectModel>();

            CreateMap<ChangesDTO, ChangesModel>()
                     .ForMember(dest => dest.UpdatedAt, act => act.MapFrom(src => src.Updated_at));
                    
            CreateMap<Updated_atDTO, UpdatedAtModel>();
            CreateMap<State_idDTO, StateIdModel>();
            CreateMap<RepositoryDTO, RepositoryModel>();

        }
    }
}
