using Newtonsoft.Json;
using Questao2;
using System.Net;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = GetTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = GetTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int GetTotalScoredGoalsAsync(string team, int year)
    {
        int gols = 0;

        for (int t = 1; t <= 2; t++)
        {
            var url = String.Format("https://jsonmock.hackerrank.com/api/football_matches?year={0}&team{1}={2}", year.ToString(), t.ToString(), team.ToString());
            var retorno = GetAsync(url).Result;
            int totalPaginas = retorno.Total_pages;

            for (int i = 2; i <= totalPaginas+1; i++)
            {
                foreach (var item in retorno.Data)
                {
                    gols += (t == 1 ? int.Parse(item.Team1goals) : 0);
                    gols += (t == 2 ? int.Parse(item.Team2goals) : 0);
                }
                if (i == totalPaginas+1) break;
                url = String.Format("https://jsonmock.hackerrank.com/api/football_matches?year={0}&team{1}={2}&page={3}", year.ToString(), t.ToString(), team.ToString(), i);
                retorno = GetAsync(url).Result;
            }
        }
        return gols;

    }

    public static async Task<Root> GetAsync(string url)
    {
        HttpClient client = new HttpClient();
        var resultado = await client.GetAsync(url);

        if (resultado.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException($"{resultado.StatusCode}-{resultado.RequestMessage}");

        var retorno = await resultado.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Root>(retorno);
    }
}