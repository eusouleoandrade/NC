namespace NC.GameStore.Exception
{
    public class MockRepositoryException : AppException
    {
        public MockRepositoryException(string message) : base(message)
        {
        }

        public MockRepositoryException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public MockRepositoryException(System.Exception innerException) : base("Failed to connect the database.", innerException)
        {
        }
    }
}