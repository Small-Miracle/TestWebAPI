using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.RegularExpressions;
using AutoMapper;
using ModelsLibrary;
using DTOLibrary;
using Mapping;
using System.Linq;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Web;



namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("")]






    public class MergeHookController : ControllerBase
    {
        long chat = -4255163248;
        string RedmineAPIToken = "f09c2ab7481d63037a30ccf8512c1b34f5121b9d";
        List<string> action = new List<string> { "close", "update", "merge" }; 
        private readonly ILogger<MergeHookController> _logger;
        private readonly List<TelegramUserAuthInfo> _telegramAuthInfo;
        private static string token { get; set; } = "7248703340:AAGVz0TV3xEQC-Y183XpVWfiJw9swwi1yik";
        private static TelegramBotClient BotClient = new TelegramBotClient(token);
        public MergeHookController(ILogger<MergeHookController> logger)
        {
            _logger = logger;
           

            _telegramAuthInfo = new List<TelegramUserAuthInfo>()
            {
                new TelegramUserAuthInfo()
                {
                    Email = "test@mail.ru",
                    UserId = 1,
                    ChatId = -4231653975
                },
                new TelegramUserAuthInfo()
                {
                    Email = "v.nynok@yandex.ru",
                    UserId = 444,
                    ChatId = 5202846717
                   
                }
            };
        }

        static async void SendMessageAsync(TelegramBotClient botClient, long chatId, string message)
        {
            try
            {
                await botClient.SendTextMessageAsync( chatId, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке сообщения: {ex.Message}");
            }
        }
        private async Task AddRedmineNote(MergeHookModel hook, List<int> issueNumbers)
        {
            //Отправка запроса на создание примечания к issue
            using (HttpClient client = new HttpClient())
            {

                var NewnoteModel = new NoteModel();
                NewnoteModel.issue = new IssueModel();
                string? MergeURL = hook.ObjectAttributes.URL;

                switch (hook.ObjectAttributes.Action)
                {
                    case "open":
                        NewnoteModel.issue.notes = $"Создан Merge Request - {MergeURL}";
                        break;
                    case "reopen":
                        NewnoteModel.issue.notes = $"Merge Request отрыт - {MergeURL}";
                        break;

                    case "close":
                        NewnoteModel.issue.notes = $"Merge Request закрыт - {MergeURL}";
                        break;

                    case "update":
                        NewnoteModel.issue.notes = $"Merge Request обновлен - {MergeURL}";
                        break;

                    case "merge":
                        NewnoteModel.issue.notes = $"Merge Request смерджен - {MergeURL}";
                        break;

                    default:
                        NewnoteModel.issue.notes = $"Что-то другое с Merge Request - {MergeURL}";
                        break;

                }

                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfileRedmineNote>();
                });

                var mapper = mapperConfiguration.CreateMapper();

                var NewNoteDTO = mapper.Map<NoteModel>(NewnoteModel);
                string json = JsonSerializer.Serialize(NewNoteDTO);
                

                HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                foreach (var issueNumber in issueNumbers)
                {

                    var url = $"http://localhost:3000/issues/{issueNumber}.json?key={RedmineAPIToken}";
                  
                    HttpResponseMessage response = await client.PutAsync(url, content);

                    Console.WriteLine(response);

                    //отправка сообщения в telegram чат
                    if (hook.ObjectAttributes.Action == "merge")
                    {
                        SendMessageAsync(BotClient, chat, $" Для issue #{issueNumber}  \n {NewnoteModel.issue.notes}");
                    }
                    if (action.Contains(hook.ObjectAttributes.Action))
                        {
                        NotifyUser(hook, issueNumber, NewnoteModel.issue.notes);
                    }
                }
            }
        }
        private async Task NotifyUser(MergeHookModel hook, int issue, string comment)
        {
            var authInfo = _telegramAuthInfo;
            var UserInfo = authInfo.FirstOrDefault(x => x.Email == hook?.User.Email);
   

            if (authInfo is null) return;

           SendMessageAsync(BotClient, UserInfo.ChatId, $"Для issue #{issue} \n {comment}");
        }
  
        [HttpPost]
        public async Task<IActionResult> AddDataAsync([FromBody] MergeHookDTO hook)
        {

            //Маппинг MergeHookDTO в MergeHookM
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfileMergeHook>();
            });

            var mapper = mapperConfiguration.CreateMapper();
        
            var hookModel = mapper.Map<MergeHookModel>(hook);

            //Парсинг description
            string pattern = @"issue: #(\d+)";
            MatchCollection matches = Regex.Matches(hookModel.ObjectAttributes.Description, pattern);
            List<int> IssueNumbers = new List<int>();
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    IssueNumbers.Add(int.Parse(match.Groups[1].Value));
                }
                await AddRedmineNote(hookModel, IssueNumbers);
            }
            else if (hookModel.ObjectAttributes.Action != "update")
            {
                Console.WriteLine("Нету issue");
            }

            return Ok(hookModel);
        }
        
    }

}

