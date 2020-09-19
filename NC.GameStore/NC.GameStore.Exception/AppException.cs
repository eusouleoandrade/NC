namespace NC.GameStore.Exception
{
    public abstract class AppException : System.Exception
    {
        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
