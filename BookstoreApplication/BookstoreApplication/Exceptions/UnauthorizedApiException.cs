namespace BookstoreApplication.Exceptions
{
    public class UnauthorizedApiException : ApiConnectionException
    {
        public UnauthorizedApiException() : base("An invalid API key was provided for accessing external API.") { }
    }
}
