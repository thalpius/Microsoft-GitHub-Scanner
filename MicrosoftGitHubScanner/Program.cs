using Newtonsoft.Json;
using System.Text;

Thalpius();

if (args.Length != 4)
{
    Help();
}

var userArg = args[0].Replace("/user:", "");
var repoArg = args[1].Replace("/repo:", "");
var branchArg = args[2].Replace("/branch:", "");
var keywordArg = args[3].Replace("/keyword:", "");

try
{
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
    HttpResponseMessage response = await httpClient.GetAsync($"https://api.github.com/repos/{userArg}/{repoArg}/git/trees/{branchArg}?recursive=10");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    var alertObj = JsonConvert.DeserializeObject<Repo>(responseBody);

    foreach (var item in alertObj.tree)
    {
        if (item.type == "blob")
        {
            HttpResponseMessage response1 = await httpClient.GetAsync(item.url);
            response1.EnsureSuccessStatusCode();
            string responseBody1 = await response1.Content.ReadAsStringAsync();

            var alertObj1 = JsonConvert.DeserializeObject<Content>(responseBody1);
            string Base64 = (alertObj1.content);
            string decodedStringCheck = Base64Decode(Base64);
            if (decodedStringCheck.Contains($"{keywordArg}"))
            {
                Console.WriteLine($"\nFound the keyword {keywordArg} in file:\nhttps://github.com/{userArg}/{repoArg}/blob/{branchArg}/" + item.path + "\n");
                Console.WriteLine("Content of the file:\n" + decodedStringCheck);
            }
        }
    }
}

catch (HttpRequestException e)
{
    Console.WriteLine(e.ToString());
}

static void Help()
{
    Console.WriteLine("Please specify all the arguments needed to scan the repository...");
    Console.WriteLine("");
    Console.WriteLine("Example: MicrosoftGitHubScanner.exe /user:thalpius /repo:Microsoft-ADFS-Info /branch:master /keyword:ConfigurationDatabaseConnectionString");
    Console.WriteLine("");
    System.Environment.Exit(1);
}

static void Thalpius()
{
    Console.WriteLine("  _______ _           _       _           ");
    Console.WriteLine(" |__   __| |         | |     (_)          ");
    Console.WriteLine("    | |  | |__   __ _| |_ __  _ _   _ ___ ");
    Console.WriteLine("    | |  | '_ \\ / _` | | '_ \\| | | | / __|");
    Console.WriteLine("    | |  | | | | (_| | | |_) | | |_| \\__ \\");
    Console.WriteLine("    |_|  |_| |_|\\__,_|_| .__/|_|\\__,_|___/");
    Console.WriteLine("                       | |                ");
    Console.WriteLine("                       |_|                ");
    Console.WriteLine("");
    Console.WriteLine("Microsoft GitHub Keyword Scanner v0.1");
    Console.WriteLine("");
}

static string Base64Decode(string Base64string)
{
    byte[] data = Convert.FromBase64String(Base64string);
    string decodedString = Encoding.UTF8.GetString(data);
    return decodedString;
}

public class Tree
{
    public string path { get; set; }
    public string mode { get; set; }
    public string type { get; set; }
    public string sha { get; set; }
    public int size { get; set; }
    public string url { get; set; }
}
public class Repo
{
    public string sha { get; set; }
    public string url { get; set; }
    public List<Tree> tree { get; set; }
    public Boolean truncated { get; set; }

}
public class Content
{
    public string sha { get; set; }
    public string node_id { get; set; }
    public int size { get; set; }
    public string url { get; set; }
    public string content { get; set; }
    public string encoding { get; set; }

}