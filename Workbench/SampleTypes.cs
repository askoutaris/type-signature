using System;
using System.Collections.Generic;

namespace Workbench
{
	public interface IKeyedEntity
	{
		public string Key { get; set; }
	}

	public abstract class Entity
	{
		private readonly string _basePassword;
		public string Code { get; }
	}

	public class Person : Entity, IKeyedEntity
	{
		private readonly string _password;

		public string Key { get; set; }
		public Nullable<int> CarsCount { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public Person Parent { get; set; }
		public Gender Gender { get; set; }
		public IReadOnlyCollection<Address> Addresses { get; set; }
		public List<Phone> Phones { get; set; }
		public Dictionary<string, Address> Mappings { get; set; }

		public Person(string password, int id, string name, IReadOnlyCollection<Address> addresses, List<Phone> phones)
		{
			_password = password;
			Id = id;
			Name = name;
			Addresses = addresses;
			Phones = phones;
		}
	}

	public class Address
	{
		public int Id { get; }
		public string City { get; }

		public Address(int id, string city)
		{
			Id = id;
			City = city;
		}
	}

	public struct Phone
	{
		public string Number { get; set; }
	}

	public enum Gender
	{
		Male,
		Felame
	}
}
