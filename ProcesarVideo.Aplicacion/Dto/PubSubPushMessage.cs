namespace ProcesarVideos.Aplicacion.Dto
{
    public class PubSubPushMessage
    {
        public PubSubMessage Message { get; set; }
        public string Subscription { get; set; }
    }
}
