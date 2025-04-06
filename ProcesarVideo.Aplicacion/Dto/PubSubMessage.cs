namespace ProcesarVideos.Aplicacion.Dto
{
    public class PubSubMessage
    {
        public string Data { get; set; } // base64 string
        public Dictionary<string, string> Attributes { get; set; }
        public string MessageId { get; set; }
        public string PublishTime { get; set; }
    }
}
