using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006DA RID: 1754
	public class DHParameter : Asn1Encodable
	{
		// Token: 0x060040A5 RID: 16549 RVA: 0x0018310F File Offset: 0x0018130F
		public DHParameter(BigInteger p, BigInteger g, int l)
		{
			this.p = new DerInteger(p);
			this.g = new DerInteger(g);
			if (l != 0)
			{
				this.l = new DerInteger(l);
			}
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x00183140 File Offset: 0x00181340
		public DHParameter(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.p = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.g = (DerInteger)enumerator.Current;
			if (enumerator.MoveNext())
			{
				this.l = (DerInteger)enumerator.Current;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x001831A3 File Offset: 0x001813A3
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x001831B0 File Offset: 0x001813B0
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x001831BD File Offset: 0x001813BD
		public BigInteger L
		{
			get
			{
				if (this.l != null)
				{
					return this.l.PositiveValue;
				}
				return null;
			}
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x001831D4 File Offset: 0x001813D4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.p,
				this.g
			});
			if (this.l != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.l
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002881 RID: 10369
		internal DerInteger p;

		// Token: 0x04002882 RID: 10370
		internal DerInteger g;

		// Token: 0x04002883 RID: 10371
		internal DerInteger l;
	}
}
