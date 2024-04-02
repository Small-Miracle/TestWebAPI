﻿namespace DTOLibrary
{
    public class Object_attributesDTO

    {
        public int Assigne_id { get; set; }
        public int Author_id { get; set; }
        public string? Created_at { get; set; }
        public string? Description { get; set; }
        public int? Head_pipeline_id { get; set; }
        public int Id { get; set; }
        public int Iid { get; set; }
        public string? Last_edited_at { get; set; }
        public int? Last_edited_by_id { get; set; }
        public string? Merge_commit_sha { get; set; }
        public string? Merge_error { get; set; }
        public Merge_paramsDTO Merge_params { get; set; }
        public string? Merge_status { get; set; }
        public int? Merge_user_id { get; set; }
        public Boolean Merge_when_pipeline_succeeds { get; set; }
        public int? Milestone_id { get; set; }
        public string? source_branch { get; set; }
        public int? Source_project_id { get; set; }
        public int? State_id { get; set; }
        public string? Target_branch { get; set; }
        public int? Target_project_id { get; set; }
        public int Time_estimate { get; set; }
        public string? Title { get; set; }
        public string? Updated_at { get; set; }
        public int? updated_by_id { get; set; }
        public string? url { get; set; }
        public ProjectDTO Source { get; set; }
        public ProjectDTO Target { get; set; }
        public Last_commitDTO Last_commit { get; set; }
        public Boolean work_in_progress { get; set; }
        public int? total_time_spent { get; set; }
        public int? time_change { get; set; }
        public int? human_total_time_spent { get; set; }
        public int? human_time_change { get; set; }
        public int? human_time_estimate { get; set; }
        public List<int>? assignee_ids { get; set; }
        public List<int>? reviewer_ids { get; set; }
        public List<string>? labels { get; set; }
        public string? state { get; set; }
        public Boolean blocking_discussions_resolved { get; set; }
        public Boolean first_contribution { get; set; }
        public string? detailed_merge_status { get; set; }
        public string? action { get; set; }
    }
}