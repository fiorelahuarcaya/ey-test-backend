namespace ey_techical_test.Utilities
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
