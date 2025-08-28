using KafkaLab.Publisher.BL.Publisher;

namespace KafkaLab.Publisher.BL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLDependency(this IServiceCollection services)
    { 
        services.AddSingleton<IKafkaProvider, KafkaProvider>();
        services.AddScoped<IPublisherService, PublisherService>();

        return services;
    }
}
