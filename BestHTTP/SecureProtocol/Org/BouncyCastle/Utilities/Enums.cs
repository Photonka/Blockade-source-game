using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x0200024C RID: 588
	internal abstract class Enums
	{
		// Token: 0x060015F1 RID: 5617 RVA: 0x000B2264 File Offset: 0x000B0464
		internal static Enum GetEnumValue(Type enumType, string s)
		{
			if (!Enums.IsEnumType(enumType))
			{
				throw new ArgumentException("Not an enumeration type", "enumType");
			}
			if (s.Length > 0 && char.IsLetter(s[0]) && s.IndexOf(',') < 0)
			{
				s = s.Replace('-', '_');
				s = s.Replace('/', '_');
				return (Enum)Enum.Parse(enumType, s, false);
			}
			throw new ArgumentException();
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000B22D6 File Offset: 0x000B04D6
		internal static Array GetEnumValues(Type enumType)
		{
			if (!Enums.IsEnumType(enumType))
			{
				throw new ArgumentException("Not an enumeration type", "enumType");
			}
			return Enum.GetValues(enumType);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000B22F8 File Offset: 0x000B04F8
		internal static Enum GetArbitraryValue(Type enumType)
		{
			Array enumValues = Enums.GetEnumValues(enumType);
			int index = (int)(DateTimeUtilities.CurrentUnixMs() & 2147483647L) % enumValues.Length;
			return (Enum)enumValues.GetValue(index);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000B232D File Offset: 0x000B052D
		internal static bool IsEnumType(Type t)
		{
			return t.IsEnum;
		}
	}
}
