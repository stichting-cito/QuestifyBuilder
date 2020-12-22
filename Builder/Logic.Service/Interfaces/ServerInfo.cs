namespace Questify.Builder.Logic.Service.Interfaces
{
    public class ServerInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppKey { get; set; }
        public string AppId { get; set; }
        public bool IsSelected { get; set; }
        public bool Disabled { get; set; }
    }
}
