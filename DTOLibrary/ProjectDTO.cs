﻿namespace DTOLibrary
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Web_url { get; set; }
        public string? Git_ssh_url { get; set; }
        public string? Git_http_url { get; set; }
        public string? Name_space { get; set; }
        public int Visibility_level { get; set; }
        public string? Path_with_namespace { get; set; }
        public string? Default_branch { get; set; }
        public string? Ci_config_path { get; set; }
        public string? Home_page { get; set; }
        public string? URL { get; set; }
        public string? Ssh_url { get; set; }
        public string? Http_url { get; set; }
    }
}
