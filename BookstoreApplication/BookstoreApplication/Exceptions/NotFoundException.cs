namespace BookstoreApplication.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int id) : base($"{entityName} with ID: {id} could not be found.") 
        {
        }
    }
}
