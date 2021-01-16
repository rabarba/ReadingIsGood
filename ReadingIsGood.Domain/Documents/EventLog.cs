namespace ReadingIsGood.Domain.Documents
{
    public class EventLog : Document
    {
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
