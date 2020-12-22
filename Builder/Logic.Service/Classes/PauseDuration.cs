using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Questify.Builder.Logic.Service.Classes
{
    public class PauseDuration
    {
        public static PauseDuration[] FromConfig { get; } = GetFromConfig();

        public int Duration { get; set; }
        public string Name { get; set; }

        public static implicit operator PauseDuration(int p1)
        {
            return FromConfig.FirstOrDefault(pd => pd.Duration == p1) ?? new PauseDuration
            {
                Duration = p1,
                Name = p1.ToString()
            };
        }

        public override string ToString()
        {
            return Name;
        }

        private static PauseDuration[] GetFromConfig()
        {
            NameValueCollection settings = ConfigurationManager.GetSection("ttsPauseDurationSettings") as NameValueCollection;
            Dictionary<int, string> result = new Dictionary<int, string>();
            if (settings != null)
            {
                foreach (string key_loopVariable in settings.AllKeys)
                {
                    var key = key_loopVariable;
                    int duration = 0;
                    if (int.TryParse(key, out duration))
                    {
                        result.Add(duration, settings[key]);
                    }
                }
            }

            return result.Select(kvp => new PauseDuration
            {
                Duration = kvp.Key,
                Name = kvp.Value
            }).ToArray();
        }
    }
}
