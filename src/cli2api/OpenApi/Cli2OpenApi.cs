using cli2api.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace cli2api.OpenApi;

public static class Cli2OpenApi
{

    public static WebApplicationBuilder AddCli2OpenApi(
                this WebApplicationBuilder builder,
                IEnumerable<Command> commands)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "CLI2OpenAPI",
                    Version = "v1",
                    Description = "Run a CLI command through Open API",
                });
            c.DocumentFilter<CustomSwaggerFilter>(commands);
        });
        return builder;

    }

    public static WebApplication UseCli2OpenApi(
        this WebApplication app,
        IEnumerable<Command> commands)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {

        });

        return app;
    }
}

public class CustomSwaggerFilter : IDocumentFilter
{
    private readonly IEnumerable<Command> commands;

    public CustomSwaggerFilter(IEnumerable<Command> commands)
    {
        this.commands = commands;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        //DO YOUR FILTERS
        foreach (var command in commands)
        {
            Dictionary<OperationType, OpenApiOperation> operations = new Dictionary<OperationType, OpenApiOperation>();
            List<OpenApiParameter> parameters = new List<OpenApiParameter>();
            foreach (var inputArguments in command.InputArguments)
            {
                parameters.Add(new OpenApiParameter()
                {
                    Description = inputArguments.Description,
                    Name = inputArguments.Name,
                    In = ParameterLocation.Query,
                    Required = inputArguments.Required,
                    AllowEmptyValue = inputArguments.AllowEmptyValue,
                });
            }

            OpenApiOperation openApiOperation = new OpenApiOperation()
            {
                Parameters = parameters,
                Description = command.Description,
                Summary = command.Summary,
            };


            var content = new Dictionary<string, OpenApiMediaType>();
            content.Add("MyFirstGrepResponse", new OpenApiMediaType()
            {
                Schema = new OpenApiSchema()
                {
                    Description = command.Output.Description,
                                    }
            });

            openApiOperation.Responses.Add("GrepResponse", new OpenApiResponse()
            {
                Content = content
            });

            operations.Add(OperationType.Post, openApiOperation);


            swaggerDoc.Paths.Add(command.Path, new OpenApiPathItem()
            {

                Operations = operations,
            });

        }
    }
}
