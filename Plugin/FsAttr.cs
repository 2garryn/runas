namespace Plugin;

public record FsAttr
{
    public required string Label { init; get; }
    public required string ID { init; get; }
    public required string Value { init; get; }
}

