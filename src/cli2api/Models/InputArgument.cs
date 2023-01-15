namespace cli2api.Models;

public class InputArgument
{
    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Description { get; set; }
    
    public bool Required { get; set; }

    public bool AllowEmptyValue { get; set; }
}
