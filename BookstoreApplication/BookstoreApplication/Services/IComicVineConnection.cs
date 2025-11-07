namespace BookstoreApplication.Services
{
    public interface IComicVineConnection
    {
        public Task<string> Get(string url);
    }
}
