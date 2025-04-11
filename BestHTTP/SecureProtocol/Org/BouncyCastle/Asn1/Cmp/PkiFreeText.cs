using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A9 RID: 1961
	public class PkiFreeText : Asn1Encodable
	{
		// Token: 0x06004629 RID: 17961 RVA: 0x00195486 File Offset: 0x00193686
		public static PkiFreeText GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PkiFreeText.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x00195494 File Offset: 0x00193694
		public static PkiFreeText GetInstance(object obj)
		{
			if (obj is PkiFreeText)
			{
				return (PkiFreeText)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiFreeText((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x001954D4 File Offset: 0x001936D4
		public PkiFreeText(Asn1Sequence seq)
		{
			using (IEnumerator enumerator = seq.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is DerUtf8String))
					{
						throw new ArgumentException("attempt to insert non UTF8 STRING into PkiFreeText");
					}
				}
			}
			this.strings = seq;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x0019553C File Offset: 0x0019373C
		public PkiFreeText(DerUtf8String p)
		{
			this.strings = new DerSequence(p);
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x00195550 File Offset: 0x00193750
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.strings.Count;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x00195550 File Offset: 0x00193750
		public int Count
		{
			get
			{
				return this.strings.Count;
			}
		}

		// Token: 0x17000A4F RID: 2639
		public DerUtf8String this[int index]
		{
			get
			{
				return (DerUtf8String)this.strings[index];
			}
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x00195570 File Offset: 0x00193770
		[Obsolete("Use 'object[index]' syntax instead")]
		public DerUtf8String GetStringAt(int index)
		{
			return this[index];
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x00195579 File Offset: 0x00193779
		public override Asn1Object ToAsn1Object()
		{
			return this.strings;
		}

		// Token: 0x04002CE0 RID: 11488
		internal Asn1Sequence strings;
	}
}
