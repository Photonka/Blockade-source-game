using System;
using System.Globalization;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Net
{
	// Token: 0x02000261 RID: 609
	public class IPAddress
	{
		// Token: 0x060016DC RID: 5852 RVA: 0x000B929B File Offset: 0x000B749B
		public static bool IsValid(string address)
		{
			return IPAddress.IsValidIPv4(address) || IPAddress.IsValidIPv6(address);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000B92AD File Offset: 0x000B74AD
		public static bool IsValidWithNetMask(string address)
		{
			return IPAddress.IsValidIPv4WithNetmask(address) || IPAddress.IsValidIPv6WithNetmask(address);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000B92C0 File Offset: 0x000B74C0
		public static bool IsValidIPv4(string address)
		{
			try
			{
				return IPAddress.unsafeIsValidIPv4(address);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000B92FC File Offset: 0x000B74FC
		private static bool unsafeIsValidIPv4(string address)
		{
			if (address.Length == 0)
			{
				return false;
			}
			int num = 0;
			string text = address + ".";
			int num2 = 0;
			int num3;
			while (num2 < text.Length && (num3 = text.IndexOf('.', num2)) > num2)
			{
				if (num == 4)
				{
					return false;
				}
				int num4 = int.Parse(text.Substring(num2, num3 - num2));
				if (num4 < 0 || num4 > 255)
				{
					return false;
				}
				num2 = num3 + 1;
				num++;
			}
			return num == 4;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000B9370 File Offset: 0x000B7570
		public static bool IsValidIPv4WithNetmask(string address)
		{
			int num = address.IndexOf('/');
			string text = address.Substring(num + 1);
			return num > 0 && IPAddress.IsValidIPv4(address.Substring(0, num)) && (IPAddress.IsValidIPv4(text) || IPAddress.IsMaskValue(text, 32));
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000B93B8 File Offset: 0x000B75B8
		public static bool IsValidIPv6WithNetmask(string address)
		{
			int num = address.IndexOf('/');
			string text = address.Substring(num + 1);
			return num > 0 && IPAddress.IsValidIPv6(address.Substring(0, num)) && (IPAddress.IsValidIPv6(text) || IPAddress.IsMaskValue(text, 128));
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000B9404 File Offset: 0x000B7604
		private static bool IsMaskValue(string component, int size)
		{
			int num = int.Parse(component);
			try
			{
				return num >= 0 && num <= size;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000B9450 File Offset: 0x000B7650
		public static bool IsValidIPv6(string address)
		{
			try
			{
				return IPAddress.unsafeIsValidIPv6(address);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000B948C File Offset: 0x000B768C
		private static bool unsafeIsValidIPv6(string address)
		{
			if (address.Length == 0)
			{
				return false;
			}
			int num = 0;
			string text = address + ":";
			bool flag = false;
			int num2 = 0;
			int num3;
			while (num2 < text.Length && (num3 = text.IndexOf(':', num2)) >= num2)
			{
				if (num == 8)
				{
					return false;
				}
				if (num2 != num3)
				{
					string text2 = text.Substring(num2, num3 - num2);
					if (num3 == text.Length - 1 && text2.IndexOf('.') > 0)
					{
						if (!IPAddress.IsValidIPv4(text2))
						{
							return false;
						}
						num++;
					}
					else
					{
						int num4 = int.Parse(text.Substring(num2, num3 - num2), NumberStyles.AllowHexSpecifier);
						if (num4 < 0 || num4 > 65535)
						{
							return false;
						}
					}
				}
				else
				{
					if (num3 != 1 && num3 != text.Length - 1 && flag)
					{
						return false;
					}
					flag = true;
				}
				num2 = num3 + 1;
				num++;
			}
			return num == 8 || flag;
		}
	}
}
