using Prometheus;

namespace NotificationAPI;

public static class EndpointMapper
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.UseMetricServer();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        
        app.UseHttpMetrics(options=>
        {
            options.AddCustomLabel("host", context => context.Request.Host.Host);
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        return app;
    }
}