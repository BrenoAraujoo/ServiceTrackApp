namespace ServiceTrackHub.Application.ViewModel
{
    public class ResponseViewModel <T>
    {
        public ResponseViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResponseViewModel(T data)
        {
            Data = data;
        }

        public ResponseViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResponseViewModel(string error)
        {
            Errors.Add(error);
        }

        public T? Data { get; private set; }
        public List<string?> Errors { get; private set; } = new();
    }
}
