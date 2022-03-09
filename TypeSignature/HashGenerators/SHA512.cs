using System.Security.Cryptography;
using System.Text;

namespace TypeSignature.HashGenerators
{
	public class SHA512HashGenerator : IHashGenerator
	{
		public string ComputeHash(string rawData)
		{
			// Create a SHA256
			using SHA512 sha256 = SHA512.Create();

			// ComputeHash - returns byte array
			byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

			// Convert byte array to a string 
			var builder = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
				builder.Append(bytes[i].ToString("x2"));

			return builder.ToString();
		}
	}
}
