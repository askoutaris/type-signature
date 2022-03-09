using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypeSignature
{
	public interface ITypeScanner
	{
		string GetTypeInfo<TType>();
		string GetTypeInfo(Type type);
	}

	public class TypeScanner : ITypeScanner
	{
		private readonly IReadOnlyCollection<Type> _simpleTypes;

		public TypeScanner()
		{
			_simpleTypes = Constants.SimpleTypes.Defaults;
		}

		public TypeScanner(Type[] simpleTypes)
		{
			_simpleTypes = simpleTypes;
		}

		public string GetTypeInfo<TType>()
			=> GetTypeInfo(typeof(TType));

		public string GetTypeInfo(Type type)
		{
			var sb = new StringBuilder();
			var scannedTypes = new HashSet<Type>();

			ScanType(type, scannedTypes, sb);

			return sb.ToString();
		}

		private void ScanType(Type type, HashSet<Type> scannedTypes, StringBuilder sb)
		{
			if (scannedTypes.Contains(type))
				return;
			else
				scannedTypes.Add(type);

			sb.Append(type.FullName);

			if (IsSimpleType(type))
				return;

			if (type.IsEnum)
			{
				foreach (var value in Enum.GetValues(type))
					sb.Append(value.ToString());

				return;
			}
			else if (type.IsGenericType)
			{
				foreach (var genericArgumentType in type.GetGenericArguments())
					ScanType(genericArgumentType, scannedTypes, sb);
			}
			else if (type.BaseType != null && type.BaseType != typeof(object))
			{
				ScanType(type.BaseType, scannedTypes, sb);
			}

			ScanProperties(type, scannedTypes, sb);
			ScanFields(type, scannedTypes, sb);
		}

		private void ScanProperties(Type type, HashSet<Type> scannedTypes, StringBuilder sb)
		{
			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

			foreach (var property in properties)
			{
				sb.Append($"{property.Name}-{property.PropertyType.FullName}");
				ScanType(property.PropertyType, scannedTypes, sb);
			}
		}

		private void ScanFields(Type type, HashSet<Type> scannedTypes, StringBuilder sb)
		{
			var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			foreach (var field in fields)
			{
				sb.Append($"{field.Name}-{field.FieldType.FullName}");
				ScanType(field.FieldType, scannedTypes, sb);
			}
		}

		private bool IsSimpleType(Type type)
		{
			return type.IsPrimitive || _simpleTypes.Contains(type);
		}
	}
}
