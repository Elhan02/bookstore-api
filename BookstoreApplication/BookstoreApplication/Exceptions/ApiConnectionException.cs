namespace BookstoreApplication.Exceptions
{
    public class ApiConnectionException : Exception
    {
        public ApiConnectionException(string message) : base(message) {}
    }
}
