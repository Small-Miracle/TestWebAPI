using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.RegularExpressions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ModelsLibrary;
using static System.Net.WebRequestMethods;
using DTOLibrary;
using Mapping;


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

//Маппинг MergeHookDTO в MergeHookM
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = mapperConfiguration.CreateMapper();
        
        var hookM = mapper.Map<MergeHookDTO, MergeHookM>(hook);

            string hookMjson = JsonSerializer.Serialize<MergeHookM>(hookM);



            string? description = hookM.ObjectAttributes.Description;
            string? MergeURL = hookM.ObjectAttributes.URL;


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

               
//Отправка запроса на создание примечания к issue
                using (HttpClient client = new HttpClient())
                {

                    note newnote = new note();
                    newnote.issue = new issue();

                    switch (hookM.ObjectAttributes.Action)
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
                      
                        //var url = $"http://localhost:3000/issues/{issueNumber}.json?key=96bb0fbe4b976cdbc973ed5db303d2f6adca934c";
                        var url = "https://www.redmine.org/issues/40490.json";
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

