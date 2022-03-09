using Microsoft.Extensions.DependencyInjection;
using TypeSignature;
using TypeSignature.HashGenerators;

namespace TemplateBinder.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddTypeSignatureSHA256(this IServiceCollection services)
			=> services.AddTypeSignature<SHA256HashGenerator>();

		public static IServiceCollection AddTypeSignatureSHA512(this IServiceCollection services)
			=> services.AddTypeSignature<SHA512HashGenerator>();

		public static IServiceCollection AddTypeSignature<THashGenerator>(this IServiceCollection services) where THashGenerator : class, IHashGenerator
		{
			services.AddSingleton<IHashGenerator, THashGenerator>();
			services.AddSingleton<ITypeScanner, TypeScanner>();
			services.AddSingleton<ISignatureBuilder, SignatureBuilder>();
			return services;
		}
	}
}
