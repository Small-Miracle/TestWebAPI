namespace ModelsLibrary
{
    public class LastCommitM
    {
        public string? Id { get; set; }
        public string? Message { get; set; }
        public string? Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Url { get; set; }
        public AuthorM Author { get; set; }
    }
}
