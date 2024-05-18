using Microsoft.Extensions.Logging;

namespace YouZip;

public class YouZip
{
    public static int Main(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(
            builder => 
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("YouZip.Unzipper", LogLevel.Debug)
                    .AddConsole();
            });
        
        var unzipLogger = loggerFactory.CreateLogger<Unzipper>();
        var unzipper = new Unzipper(unzipLogger);
        unzipper.Unzip(args[0]);
        
        return 0;
    }
}