using cli2api.Models;
using cli2api.OpenApi;
using Microsoft.AspNetCore.Mvc;

var commands = GetCommands();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.AddCli2OpenApi(commands);

WebApplication app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.MapGet("/debugfile", () => Results.File(
    "hello file"u8.ToArray(),
    contentType: "application/octet-stream",
    "file.txt",
    false));
app.UseCli2OpenApi(commands);


app.Run();

static IEnumerable<Command> GetCommands()
{
    Command command = new Command()
    {
        CommandName = "grep",
        Description = "This is a nice and awesome description about grep",
        Summary = "This is a short summary about grep",
        InputArguments = new()
        {
            new InputArgument()
            {
                Name= "blue",
                Value = ""
            },
             new InputArgument()
            {
                Name= "document.txt",
                Value = ""
            }
        },
        Path = "/grep",
        Output = new CommandOutput()
        {
            Description = "A good long description of the grep result: STDOUT"
        }
    };
    return new List<Command>() { command };
}