using NLog;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace Questify.Builder.Logic.Service
{
    public class MathClient
    {
        public string GetMathAsBase64Image(string mathMl)
        {
            using (var client = new HttpClient())
            {
                mathMl = System.Web.HttpUtility.JavaScriptStringEncode(mathMl);
                var task = client.PostAsync(ConfigurationManager.AppSettings["MathMlService"], new StringContent($"\"{mathMl}\"", Encoding.UTF8, "application/json"));
                task.Wait();

                if (task.Result.IsSuccessStatusCode)
                {
                    var responseTask = task.Result.Content.ReadAsStringAsync();
                    responseTask.Wait();

                    var base64 = responseTask.Result;
                    base64 = base64.Replace("\"", string.Empty);

                    base64 = base64.Replace("data:image/png;base64,", string.Empty);
                    return base64;
                }
                else
                {
                    LogManager.GetCurrentClassLogger().Error($"Failure calling Math service. Status code {task.Result.StatusCode}. Math was: {mathMl}");
                }
                return string.Empty;
            }
        }
    }
}
