
using System.Text.Json.Serialization;

namespace ModelsLibrary
{
    public class ObjectAttributesModel
    {
        public int Assigne_id { get; set; }
        public int Author_id { get; set; }
        public DateTime Created_at { get; set; }
        public string? Description { get; set; }
        public int? Head_pipeline_id { get; set; }
        public int Id { get; set; }
        public int Iid { get; set; }
        public DateTime? Last_edited_at { get; set; }
        public int? Last_edited_by_id { get; set; }
        public string? Merge_commit_sha { get; set; }
        public string? Merge_error { get; set; }
        public MergeParamsModel Merge_params { get; set; }
        public string? Merge_status { get; set; }
        public int? Merge_user_id { get; set; }
        public Boolean Merge_when_pipeline_succeeds { get; set; }
        public int? Milestone_id { get; set; }
        public string? Source_branch { get; set; }
        public int? Source_project_id { get; set; }
        public int? State_id { get; set; }
        public string? Target_branch { get; set; }
        public int? Target_project_id { get; set; }
        public int Time_estimate { get; set; }
        public string? Title { get; set; }
        public DateTime Updated_at { get; set; }
        public int? Updated_by_id { get; set; }
        public string? URL { get; set; }
        public ProjectModel Source { get; set; }
        public ProjectModel Target { get; set; }
        public LastCommitModel Last_commit { get; set; }
        public Boolean Work_in_progress { get; set; }
        public int? Total_time_spent { get; set; }
        public int? Time_change { get; set; }
        public int? Human_total_time_spent { get; set; }
        public int? Human_time_change { get; set; }
        public int? Human_time_estimate { get; set; }
        public List<int>? Assignee_ids { get; set; }
        public List<int>? Reviewer_ids { get; set; }
        public List<string>? Labels { get; set; }
        public string? State { get; set; }
        public Boolean Blocking_discussions_resolved { get; set; }
        public Boolean First_contribution { get; set; }
        public string? Detailed_merge_status { get; set; }
        public string? Action { get; set; }
    }
}
