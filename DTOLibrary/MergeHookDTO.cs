namespace DTOLibrary
{
    public class MergeHookDTO
    {
        public string? Object_kind { get; set; }
        public string? Event_type { get; set; }
        public UserDTO? User { get; set; }
        public ProjectDTO? Project { get; set; }
        public Object_attributesDTO Object_Attributes { get; set; }
        public List<string>? labels { get; set; }
        public ChangesDTO Changes { get; set;  }
        public RepositoryDTO Repository { get; set; }
    }
}