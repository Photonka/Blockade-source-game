using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000646 RID: 1606
	public class DerSequence : Asn1Sequence
	{
		// Token: 0x06003C74 RID: 15476 RVA: 0x001742A8 File Offset: 0x001724A8
		public static DerSequence FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new DerSequence(v);
			}
			return DerSequence.Empty;
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x001742BF File Offset: 0x001724BF
		public DerSequence() : base(0)
		{
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x001742C8 File Offset: 0x001724C8
		public DerSequence(Asn1Encodable obj) : base(1)
		{
			base.AddObject(obj);
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x001742D8 File Offset: 0x001724D8
		public DerSequence(params Asn1Encodable[] v) : base(v.Length)
		{
			foreach (Asn1Encodable obj in v)
			{
				base.AddObject(obj);
			}
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x0017430C File Offset: 0x0017250C
		public DerSequence(Asn1EncodableVector v) : base(v.Count)
		{
			foreach (object obj in v)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				base.AddObject(obj2);
			}
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x0017436C File Offset: 0x0017256C
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
			derOut.WriteEncoded(48, bytes);
		}

		// Token: 0x040025BE RID: 9662
		public static readonly DerSequence Empty = new DerSequence();
	}
}
