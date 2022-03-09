namespace TypeSignature.HashGenerators
{
	public interface IHashGenerator
	{
		string ComputeHash(string rawData);
	}
}
