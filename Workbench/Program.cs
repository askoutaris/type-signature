using System;
using Microsoft.Extensions.DependencyInjection;
using TemplateBinder.Extensions.DependencyInjection;
using TypeSignature;
using TypeSignature.HashGenerators;

namespace Workbench
{
	class Program
	{
		static void Main(string[] args)
		{
			DependencyInjection();
			SimpleUsage();
		}

		private static void SimpleUsage()
		{
			ITypeScanner typeScanner = new TypeScanner();
			IHashGenerator hashGenerator = new SHA512HashGenerator();
			ISignatureBuilder signatureBuilder = new SignatureBuilder(typeScanner, hashGenerator);

			string signature = signatureBuilder.GetSignature<Person>();

			Console.WriteLine($"The signature of type Person is: {signature}");

			Console.ReadLine();
		}

		private static void DependencyInjection()
		{
			// setup our DI
			var serviceProvider = new ServiceCollection()
				.AddTypeSignatureSHA256()
				.BuildServiceProvider();

			// resolve SignatureBuilder
			ISignatureBuilder signatureBuilder = serviceProvider.GetService<ISignatureBuilder>();

			string signature = signatureBuilder.GetSignature<Person>();

			Console.WriteLine($"The signature of type Person is: {signature}");

			Console.ReadLine();
		}
	}
}
