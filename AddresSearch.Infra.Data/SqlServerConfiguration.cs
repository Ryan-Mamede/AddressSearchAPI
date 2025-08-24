namespace AddressSearch.Infra.Data;

public class SqlServerConfiguration
{
    public string? ConnectionString { get; set; }
    public int RetryCount { get; set; } = 5;
    public int RetryDelaySeconds { get; set; } = 10;
}
