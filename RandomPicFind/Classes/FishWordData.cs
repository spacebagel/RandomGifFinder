using AngleSharp;
using System.Threading.Tasks;

namespace RandomPicFind.Classes;

public class WordObject
{
    public async Task<string> FindRandomWordAsync()
    {
        var config = Configuration.Default.WithDefaultLoader();
        var address = "https://randomword.com/";
        var document = await BrowsingContext.New(config).OpenAsync(address);
        var cell = document.QuerySelector(@"div#random_word");
        return cell.TextContent;
    }
}