using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.RegularExpressions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestWebApi.DTO;
using TestWebAPI.Models;
using AutoMapper;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]






    public class MergeHookController : ControllerBase
    {

        private readonly ILogger<MergeHookController> _logger;

        public MergeHookController(ILogger<MergeHookController> logger)
        {
            _logger = logger;
        }



        [HttpPost]
        public async Task<IActionResult> AddDataAsync([FromBody] MergeHookDTO hook)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MergeHookDTO, MergeHookM>()
                    .ForMember(dest => dest.ObjectKind, act => act.MapFrom(src => src.Object_kind))
                    .ForMember(dest => dest.EventType, act => act.MapFrom(src => src.Event_type))
                    .ForMember(dest => dest.ObjectAttributes, act => act.MapFrom(src => src.Object_Attributes));
                cfg.CreateMap<Object_attributesDTO, ObjectAttributesM>();

                cfg.CreateMap<Merge_paramsDTO, MergeParamsM>()
                    .ForMember(dest => dest.ForceRemoveSourceBranch, act => act.MapFrom(src => src.force_remove_source_branch));


                cfg.CreateMap<Last_commitDTO, LastCommitM>();
                cfg.CreateMap<AuthorDTO, AuthorM>();

                cfg.CreateMap<UserDTO, UserM>();

                cfg.CreateMap<ProjectDTO, ProjectM>();

                cfg.CreateMap<ChangesDTO, ChangesM>()
                         .ForMember(dest => dest.UpdatedAtM, act => act.MapFrom(src => src.Updated_at));
                cfg.CreateMap<Updated_atDTO, UpdatedAtM>();
                cfg.CreateMap<State_idDTO, StateIdM>();
                cfg.CreateMap<RepositoryDTO, RepositoryM>();
            });

            var mapper = new Mapper(config);
            var hookM = mapper.Map<MergeHookDTO, MergeHookM>(hook);


            string? description = hookM.ObjectAttributes.Description;
            string? MergeURL = hookM.ObjectAttributes.url;


//Парсинг description
            string pattern = @"issue: #(\d+)";
            MatchCollection matches = Regex.Matches(description, pattern);
            List<int> IssueNumbers = new List<int>();
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    IssueNumbers.Add(int.Parse(match.Groups[1].Value));
                }

               
//Отправка запроса на создание примечания к issue на Redmine
                using (HttpClient client = new HttpClient())
                {

                    note newnote = new note();
                    newnote.issue = new issue();

                    switch (hookM.ObjectAttributes.action)
                        {
                        case "open":
                            newnote.issue.notes = $"Создан Merge Request - {MergeURL}";
                            break;

                        case "close":
                            newnote.issue.notes = $"Merge Request закрыт - {MergeURL}";
                            break;

                        case "update":
                            newnote.issue.notes = $"Merge Request обновлен - {MergeURL}";
                            break;

                        case "merge":
                            newnote.issue.notes = $"Merge Request смерджен - {MergeURL}";
                            break;

                        default:
                            newnote.issue.notes = $"Что-то другое с Merge Request - {MergeURL}";
                            break;

                    }
                    string json = JsonSerializer.Serialize<note>(newnote);


                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                   
                    foreach (var issueNumber in IssueNumbers)
                    {
                        Console.WriteLine(issueNumber);

                        var url = $"http://localhost:3000/issues/{issueNumber}.json?key=96bb0fbe4b976cdbc973ed5db303d2f6adca934c";

                        HttpResponseMessage response = await client.PutAsync(url, content);

                        Console.WriteLine(response);

                    }
                }
            }

            else
            {
                Console.WriteLine("Нету issue");
            }

            return Ok(hookM);

        }
    }

}

