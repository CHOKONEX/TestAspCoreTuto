using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace TestAspCoreTuto.Extensions
{
    public static class CompressionExtension
    {
        public static void AddCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                //options.Providers.Add<CustomCompressionProvider>();
                //options.MimeTypes =
                //    ResponseCompressionDefaults.MimeTypes.Concat(
                //        new[] { "image/svg+xml" });
                options.EnableForHttps = true;
            });
        }
    }
}
