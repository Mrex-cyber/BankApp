using BankApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

var app = builder.Build();

app.UseSwagger();  
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapGet("/", async () =>
{
    string currencies = "";

    try
    {
        using var http = new HttpClient();

        var response = await http.GetAsync("https://api.monobank.ua/bank/currency");

        currencies = response.IsSuccessStatusCode
            ? await response.Content.ReadAsStringAsync()
            : await CurrencyFileProvider.ReadFromFileAsync();

        if (currencies.Trim().Length > 0)
        {
            await CurrencyFileProvider.WriteToFileAsync(currencies);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return currencies;
});

app.MapGet("/{code}", async Task<Currency?>([FromRoute] int code) =>
{
    Currency? currency = null;

    try
    {
        using var http = new HttpClient();

        var response = await http.GetAsync($"https://api.monobank.ua/bank/currency/{code}");

        if (response.IsSuccessStatusCode)
        {
            string jsonData = await response.Content.ReadAsStringAsync();

            currency = JsonSerializer.Deserialize<Currency>(jsonData);

            if (currency is null)
            {
                throw new NullReferenceException("Не знайдено валюти");
            }
        }
        else
        {
            string fileData = await CurrencyFileProvider.ReadFromFileAsync();

            currency = (JsonSerializer.Deserialize<List<Currency>>(fileData) ?? [])
                .FirstOrDefault(c => c.CurrencyCodeA == code);

            if (currency is null)
            {
                throw new NullReferenceException($"Не знайдено валюти з кодом {code}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return currency;
});

app.Run();