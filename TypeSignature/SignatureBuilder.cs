using System;
using TypeSignature.HashGenerators;

namespace TypeSignature
{
	public interface ISignatureBuilder
	{
		string GetSignature(Type type);
		string GetSignature<TType>();
	}

	public class SignatureBuilder : ISignatureBuilder
	{
		private readonly ITypeScanner _typeScanner;
		private readonly IHashGenerator _hashGenerator;

		public SignatureBuilder(ITypeScanner typeScanner, IHashGenerator hashGenerator)
		{
			_typeScanner = typeScanner;
			_hashGenerator = hashGenerator;
		}

		public string GetSignature<TType>() => GetSignature(typeof(TType));

		public string GetSignature(Type type)
		{
			var typeInfo = _typeScanner.GetTypeInfo(type);

			var signature = _hashGenerator.ComputeHash(typeInfo);

			return signature;
		}
	}
}
