namespace cli2api.Models;

public class Command
{
    public required string Path { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public required string CommandName { get; set; }

    public List<InputArgument> InputArguments { get; set; } = new();

    public required CommandOutput Output { get; set; }
}
