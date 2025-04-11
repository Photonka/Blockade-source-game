using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000649 RID: 1609
	public class DerSet : Asn1Set
	{
		// Token: 0x06003C83 RID: 15491 RVA: 0x0017447C File Offset: 0x0017267C
		public static DerSet FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new DerSet(v);
			}
			return DerSet.Empty;
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x00174493 File Offset: 0x00172693
		internal static DerSet FromVector(Asn1EncodableVector v, bool needsSorting)
		{
			if (v.Count >= 1)
			{
				return new DerSet(v, needsSorting);
			}
			return DerSet.Empty;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x001744AB File Offset: 0x001726AB
		public DerSet() : base(0)
		{
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x001744B4 File Offset: 0x001726B4
		public DerSet(Asn1Encodable obj) : base(1)
		{
			base.AddObject(obj);
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x001744C4 File Offset: 0x001726C4
		public DerSet(params Asn1Encodable[] v) : base(v.Length)
		{
			foreach (Asn1Encodable obj in v)
			{
				base.AddObject(obj);
			}
			base.Sort();
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x001744FB File Offset: 0x001726FB
		public DerSet(Asn1EncodableVector v) : this(v, true)
		{
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x00174508 File Offset: 0x00172708
		internal DerSet(Asn1EncodableVector v, bool needsSorting) : base(v.Count)
		{
			foreach (object obj in v)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				base.AddObject(obj2);
			}
			if (needsSorting)
			{
				base.Sort();
			}
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x00174574 File Offset: 0x00172774
		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
			foreach (object obj in this)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				derOutputStream.WriteObject(obj2);
			}
			Platform.Dispose(derOutputStream);
			byte[] bytes = memoryStream.ToArray();
			derOut.WriteEncoded(49, bytes);
		}

		// Token: 0x040025C1 RID: 9665
		public static readonly DerSet Empty = new DerSet();
	}
}
