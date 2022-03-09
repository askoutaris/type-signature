using System;
using Microsoft.Extensions.DependencyInjection;
using TemplateBinder.Extensions.DependencyInjection;
using TypeSignature;

namespace Workbench
{
	class Program
	{
		static void Main(string[] args)
		{
			// setup our DI
			var serviceProvider = new ServiceCollection()
				.AddTypeSignatureSHA256()
				.BuildServiceProvider();

			// resolve SignatureBuilder
			var signatureBuilder = serviceProvider.GetService<ISignatureBuilder>();

			var signature = signatureBuilder.GetSignature<Person>();

			Console.WriteLine($"The signature of type Person is: {signature}");

			Console.ReadLine();
		}
	}
}
