using System;
using System.Collections;
using System.Text;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200027F RID: 639
	public abstract class CollectionUtilities
	{
		// Token: 0x06001792 RID: 6034 RVA: 0x000BAFB8 File Offset: 0x000B91B8
		public static void AddRange(IList to, IEnumerable range)
		{
			foreach (object value in range)
			{
				to.Add(value);
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000BB008 File Offset: 0x000B9208
		public static bool CheckElementsAreOfType(IEnumerable e, Type t)
		{
			foreach (object o in e)
			{
				if (!t.IsInstanceOfType(o))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000BB060 File Offset: 0x000B9260
		public static IDictionary ReadOnly(IDictionary d)
		{
			return new UnmodifiableDictionaryProxy(d);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000BB068 File Offset: 0x000B9268
		public static IList ReadOnly(IList l)
		{
			return new UnmodifiableListProxy(l);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000BB070 File Offset: 0x000B9270
		public static ISet ReadOnly(ISet s)
		{
			return new UnmodifiableSetProxy(s);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000BB078 File Offset: 0x000B9278
		public static object RequireNext(IEnumerator e)
		{
			if (!e.MoveNext())
			{
				throw new InvalidOperationException();
			}
			return e.Current;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000BB090 File Offset: 0x000B9290
		public static string ToString(IEnumerable c)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			IEnumerator enumerator = c.GetEnumerator();
			if (enumerator.MoveNext())
			{
				stringBuilder.Append(enumerator.Current.ToString());
				while (enumerator.MoveNext())
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(enumerator.Current.ToString());
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
