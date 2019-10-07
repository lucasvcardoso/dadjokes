namespace IHazDadJokes.API.Lib
{
    public interface IDadJokesServiceConfiguration
    {
        int Limit { get; set; }
        string Url { get; set;  }
    }
}