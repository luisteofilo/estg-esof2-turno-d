namespace ESOF.WebApp.Scraper.Helpers;

public abstract class IScraper<T>
{
    public abstract Task<T> Handle(string Url);
}