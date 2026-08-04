namespace CodebaseAtom.WebUI.Infrastructure.FileStorage;

public static class ConfigureFileStorage
{
    public static IServiceCollection AddFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.Configure<FileStorageOptions>(configuration.GetRequiredSection(FileStorageOptions.SectionKey));
        _ = services.AddTransient<IFileStorageService, FileStorageService>();

        return services;
    }
}
