using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Numerics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Web;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/[controller]/Get")]
        public List<ToDos> Get()
        {
            string serviceUrl = "https://jsonplaceholder.typicode.com/todods";
            GetDataFromAPI(serviceUrl);
            List<ToDos> toDos = JsonConvert.DeserializeObject<List<ToDos>>(GetDataFromAPI(serviceUrl));
            if(toDos == null)
            {
                return null;
            }
            else { return toDos; }

        }

        [HttpGet]
        [Route("/[controller]/Get/{id:int}")]
        public ActionResult Get(int id, [FromQuery] string? comments)
        {
            var response = GetArtikelFromAPI(id);
            var test = comments;

            var commentsList = GetCommentsFromAPI(id);
            var commentsListBuffered = new List<Comment>();
            commentsList.ForEach(comment =>
            {
                comment.timestamp = GetTimestamp(DateTime.Now);
                if (comment.id % 2 == 0)
                {
                    commentsListBuffered.Add(comment);
                }
            });
            response.comments = commentsListBuffered;


            response.timeStamp = GetTimestamp(DateTime.Now);
            return Ok(response);
        }

        public static Article GetArtikelFromAPI(int id)
        {
            string serviceUrl = "https://jsonplaceholder.typicode.com/posts/" + id.ToString();
            GetDataFromAPI(serviceUrl);
            Article article = JsonConvert.DeserializeObject<Article>(GetDataFromAPI(serviceUrl));
            return article;
        }

        public static String GetTimestamp(DateTime value)
        {
            //return value.ToString("yyyyMMddHHmmssffff");
            return value.ToString("yyyy:MM:dd");
        }

        public static List<Comment> GetCommentsFromAPI(int number)
        {
            string serviceUrl = "https://jsonplaceholder.typicode.com/posts/" + number.ToString() + "/comments";
            GetDataFromAPI(serviceUrl);
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(GetDataFromAPI(serviceUrl));
            return comments;
        }



        public static List<ToDos> GetToDosFromAPI()
        {
            string serviceUrl = "https://jsonplaceholder.typicode.com/todos";
            GetDataFromAPI(serviceUrl);
            List<ToDos> todos = JsonConvert.DeserializeObject<List<ToDos>>(GetDataFromAPI(serviceUrl));
            return todos;
        }

        public static string GetDataFromAPI(string serviceUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            var response = request.GetResponse();

            MemoryStream responeStream = new MemoryStream();
            response.GetResponseStream().CopyTo(responeStream);
            responeStream.Position = 0;
            StreamReader reader = new StreamReader(responeStream);
            string text = reader.ReadToEnd();
            return text;
        }


      




    }
}