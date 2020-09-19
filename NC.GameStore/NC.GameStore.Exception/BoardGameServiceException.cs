namespace NC.GameStore.Exception
{
    public class BoardGameServiceException : AppException
    {
        public BoardGameServiceException(string message) : base(message)
        {
        }

        public BoardGameServiceException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public BoardGameServiceException(System.Exception innerException) : base("Failed to process the service.", innerException)
        {
        }
    }
}
