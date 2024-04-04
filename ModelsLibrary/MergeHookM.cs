namespace ModelsLibrary
{
    public class MergeHookM
    {
        public string? ObjectKind { get; set; }
        public string? EventType { get; set; }
        public UserM? User { get; set; }
        public ProjectM? Project { get; set; }
        public ObjectAttributesM ObjectAttributes { get; set; }
        public List<string>? labels { get; set; }
        public ChangesM Changes { get; set; }
        public RepositoryM Repository { get; set; }
    }
}