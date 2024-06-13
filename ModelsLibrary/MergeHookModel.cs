namespace ModelsLibrary
{
    public class MergeHookModel
    {
        public string? ObjectKind { get; set; }
        public string? EventType { get; set; }
        public UserModel? User { get; set; }
        public ProjectModel? Project { get; set; }
        public ObjectAttributesModel ObjectAttributes { get; set; }
        public List<string>? labels { get; set; }
        public ChangesModel Changes { get; set; }
        public RepositoryModel Repository { get; set; }
    }
}