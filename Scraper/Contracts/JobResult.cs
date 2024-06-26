namespace ESOF.WebApp.Scraper.Contracts;

public record JobResult
(
    string Title,
    string Location,
    string Company,
    string Content,
    string OtherDetails
);