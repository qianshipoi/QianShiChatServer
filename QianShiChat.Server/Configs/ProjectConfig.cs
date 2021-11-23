namespace QianShiChat.Server.Configs
{
    public class ProjectConfig
    {
        public string ProjectName { get; set; }
        public string ProjectHost { get; set; }
        public int ProjectPort { get; set; }
        public string ProjectUrl => $"{ProjectHost}:{ProjectPort}";
    }
}
