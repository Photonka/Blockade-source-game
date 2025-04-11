using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x02000250 RID: 592
	internal abstract class Platform
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x000B235E File Offset: 0x000B055E
		private static string GetNewLine()
		{
			return Environment.NewLine;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000B2365 File Offset: 0x000B0565
		internal static bool EqualsIgnoreCase(string a, string b)
		{
			return Platform.ToUpperInvariant(a) == Platform.ToUpperInvariant(b);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000B2378 File Offset: 0x000B0578
		internal static string GetEnvironmentVariable(string variable)
		{
			string result;
			try
			{
				result = Environment.GetEnvironmentVariable(variable);
			}
			catch (SecurityException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000B23A4 File Offset: 0x000B05A4
		internal static Exception CreateNotImplementedException(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000B23AC File Offset: 0x000B05AC
		internal static IList CreateArrayList()
		{
			return new ArrayList();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000B23B3 File Offset: 0x000B05B3
		internal static IList CreateArrayList(int capacity)
		{
			return new ArrayList(capacity);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000B23BB File Offset: 0x000B05BB
		internal static IList CreateArrayList(ICollection collection)
		{
			return new ArrayList(collection);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000B23C4 File Offset: 0x000B05C4
		internal static IList CreateArrayList(IEnumerable collection)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object value in collection)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000B241C File Offset: 0x000B061C
		internal static IDictionary CreateHashtable()
		{
			return new Hashtable();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000B2423 File Offset: 0x000B0623
		internal static IDictionary CreateHashtable(int capacity)
		{
			return new Hashtable(capacity);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000B242B File Offset: 0x000B062B
		internal static IDictionary CreateHashtable(IDictionary dictionary)
		{
			return new Hashtable(dictionary);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000B2433 File Offset: 0x000B0633
		internal static string ToLowerInvariant(string s)
		{
			return s.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000B2440 File Offset: 0x000B0640
		internal static string ToUpperInvariant(string s)
		{
			return s.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000B244D File Offset: 0x000B064D
		internal static void Dispose(Stream s)
		{
			s.Close();
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000B2455 File Offset: 0x000B0655
		internal static void Dispose(TextWriter t)
		{
			t.Close();
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000B245D File Offset: 0x000B065D
		internal static int IndexOf(string source, string value)
		{
			return Platform.InvariantCompareInfo.IndexOf(source, value, CompareOptions.Ordinal);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000B2470 File Offset: 0x000B0670
		internal static int LastIndexOf(string source, string value)
		{
			return Platform.InvariantCompareInfo.LastIndexOf(source, value, CompareOptions.Ordinal);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000B2483 File Offset: 0x000B0683
		internal static bool StartsWith(string source, string prefix)
		{
			return Platform.InvariantCompareInfo.IsPrefix(source, prefix, CompareOptions.Ordinal);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000B2496 File Offset: 0x000B0696
		internal static bool EndsWith(string source, string suffix)
		{
			return Platform.InvariantCompareInfo.IsSuffix(source, suffix, CompareOptions.Ordinal);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000B24A9 File Offset: 0x000B06A9
		internal static string GetTypeName(object obj)
		{
			return obj.GetType().FullName;
		}

		// Token: 0x0400154A RID: 5450
		private static readonly CompareInfo InvariantCompareInfo = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x0400154B RID: 5451
		internal static readonly string NewLine = Platform.GetNewLine();
	}
}
