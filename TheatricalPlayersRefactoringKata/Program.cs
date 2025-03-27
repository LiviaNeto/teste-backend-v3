using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models; 
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories; 
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories; 
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Extensions;
using TheatricalPlayersRefactoringKata.Application.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Theatrical Players API", 
        Version = "v1",
        Description = "API for managing theatrical plays, performances and generate invoice statements"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Dependency Injection
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.DefaultModelExpandDepth(-1);  
        c.DefaultModelsExpandDepth(-1); 
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

/*var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços de controladores
builder.Services.AddControllers();

// Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Theatrical Players API", 
        Version = "v1",
        Description = "API para gerenciamento de faturas"
    });

    // Habilita comentários XML para documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    // Adiciona documentação XML se o arquivo existir
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Registra seus serviços de dependência
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Constrói a aplicação
var app = builder.Build();

// Configura o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical Players API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

// Configura middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();*/

 /*using System;

using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Presentation.Controllers;
using TheatricalPlayersRefactoringKata.Presentation.Formatters;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

class Program
{
    static void Main()
    {
        // Configuração do serviço de injeção de dependência
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IStatementGeneratorService, StatementService>()
            .AddSingleton<TextStatementFormatter>()  // Injeta o TextStatementFormatter diretamente
            .AddSingleton<XmlStatementFormatter>()  // Injeta o XmlStatementFormatter diretamente
            .AddSingleton<StatementController>()    // Injeta o StatementController
            .BuildServiceProvider();

        // Criar instância de PlayTypeConfiguration para gerenciar tipos de peça
        var playTypeConfig = new PlayTypeConfiguration();

        // Criar peças usando PlayTypeConfiguration
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, playTypeConfig.GetPlayType("tragedy")) },
            { "as-you-like-it", new Play("As You Like It", 2000, playTypeConfig.GetPlayType("comedy")) },
            { "othello", new Play("Othello", 1800, playTypeConfig.GetPlayType("tragedy")) }
        };

        // Criar uma fatura (Invoice)
        var invoice = new Invoice("John Doe", new List<Performance>
        {
            new Performance("hamlet", 55),
            new Performance("as-you-like-it", 35),
            new Performance("othello", 40)
        });

        // Recuperar o controlador
        var statementController = serviceProvider.GetRequiredService<StatementController>();

        // Gerar e imprimir extrato em texto
        string textStatement = statementController.GenerateTextStatement(invoice, plays);
        Console.WriteLine("--- Extrato em Texto ---");
        Console.WriteLine(textStatement);

        // Gerar e imprimir extrato em XML
        string xmlStatement = statementController.GenerateXmlStatement(invoice, plays);
        Console.WriteLine("\n--- Extrato em XML ---");
        Console.WriteLine(xmlStatement);    
    }
}*/
 