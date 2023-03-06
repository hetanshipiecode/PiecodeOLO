using Serilog;

namespace DishoutOLO.Helpers.Provider
{
    public class LoggerProvider
    {

        public LoggerProvider()
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()

             .WriteTo.File("logs\\logfile_yyyyDDmm.txt")
            .CreateLogger();
        }
        public void logmsg(string message)
        {
            Log.Information(message);
        }
    }
}
