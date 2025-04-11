using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000662 RID: 1634
	public class KeySpecificInfo : Asn1Encodable
	{
		// Token: 0x06003D11 RID: 15633 RVA: 0x0017590D File Offset: 0x00173B0D
		public KeySpecificInfo(DerObjectIdentifier algorithm, Asn1OctetString counter)
		{
			this.algorithm = algorithm;
			this.counter = counter;
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x00175924 File Offset: 0x00173B24
		public KeySpecificInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.algorithm = (DerObjectIdentifier)enumerator.Current;
			enumerator.MoveNext();
			this.counter = (Asn1OctetString)enumerator.Current;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x0017596E File Offset: 0x00173B6E
		public DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x00175976 File Offset: 0x00173B76
		public Asn1OctetString Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x0017597E File Offset: 0x00173B7E
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algorithm,
				this.counter
			});
		}

		// Token: 0x040025DB RID: 9691
		private DerObjectIdentifier algorithm;

		// Token: 0x040025DC RID: 9692
		private Asn1OctetString counter;
	}
}
