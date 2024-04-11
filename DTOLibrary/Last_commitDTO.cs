namespace DTOLibrary
{
    public class Last_commitDTO

    {
        public string? Id { get; set; }
        public string? Message { get; set; }
        public string? Title { get; set; }
        public DateTime Timestamp { get; set; }
        public string? URL { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
