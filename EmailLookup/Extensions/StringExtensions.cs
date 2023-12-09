using System.Globalization;

namespace EmailLookup.Extensions;

internal static class StringExtensions
{
	internal static string ToPascalCase(this string v) => v.Length switch
	{
		0 => string.Empty,
		1 => v.ToUpper(CultureInfo.InvariantCulture),
		_ => string.Concat(v[..1].ToUpper(CultureInfo.InvariantCulture), v.AsSpan(1))
	};
}
