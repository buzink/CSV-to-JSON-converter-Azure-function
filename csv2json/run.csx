#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["csv"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.csv;
    name = name.Trim(new char[]{'\uFEFF'}).Replace("\r", "");

    IEnumerable<string> csv = name.Split('\n');

    log.LogInformation(csv.First());

    var responseMessage = CsvToJson(csv);

    return new OkObjectResult(responseMessage);


}

      public static IEnumerable<JObject> CsvToJson(IEnumerable<string> csvLines)
    {
        var csvLinesList = csvLines.ToList();

        var header = csvLinesList[0].Split(';');
        for (int i = 1; i < csvLinesList.Count; i++)
        {
            var thisLineSplit = csvLinesList[i].Split(';');
            var pairedWithHeader = header.Zip(thisLineSplit, (h, v) => new KeyValuePair<string, string>(h, v));

            yield return new JObject(pairedWithHeader.Select(j => new JProperty(j.Key, j.Value)));
        }
    }