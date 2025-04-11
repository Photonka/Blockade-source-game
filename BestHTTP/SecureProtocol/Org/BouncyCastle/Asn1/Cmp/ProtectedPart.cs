using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B5 RID: 1973
	public class ProtectedPart : Asn1Encodable
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x00196329 File Offset: 0x00194529
		private ProtectedPart(Asn1Sequence seq)
		{
			this.header = PkiHeader.GetInstance(seq[0]);
			this.body = PkiBody.GetInstance(seq[1]);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00196355 File Offset: 0x00194555
		public static ProtectedPart GetInstance(object obj)
		{
			if (obj is ProtectedPart)
			{
				return (ProtectedPart)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProtectedPart((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00196394 File Offset: 0x00194594
		public ProtectedPart(PkiHeader header, PkiBody body)
		{
			this.header = header;
			this.body = body;
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x001963AA File Offset: 0x001945AA
		public virtual PkiHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x001963B2 File Offset: 0x001945B2
		public virtual PkiBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x001963BA File Offset: 0x001945BA
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.header,
				this.body
			});
		}

		// Token: 0x04002D1A RID: 11546
		private readonly PkiHeader header;

		// Token: 0x04002D1B RID: 11547
		private readonly PkiBody body;
	}
}
