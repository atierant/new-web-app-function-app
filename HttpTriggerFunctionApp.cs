using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    var username =  Environment.GetEnvironmentVariable("UsernameFromKeyVault", EnvironmentVariableTarget.Process);
    var password =  Environment.GetEnvironmentVariable("PasswordFromKeyVault", EnvironmentVariableTarget.Process);

    log.LogInformation($"Username: {username}");
    log.LogInformation($"Username: {password}");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    return name != null
        ? (ActionResult)new OkObjectResult($"Hello, {name}")
        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
}
