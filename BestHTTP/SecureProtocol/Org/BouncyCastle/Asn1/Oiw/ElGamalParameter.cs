using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw
{
	// Token: 0x020006EF RID: 1775
	public class ElGamalParameter : Asn1Encodable
	{
		// Token: 0x06004145 RID: 16709 RVA: 0x001855B4 File Offset: 0x001837B4
		public ElGamalParameter(BigInteger p, BigInteger g)
		{
			this.p = new DerInteger(p);
			this.g = new DerInteger(g);
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x001855D4 File Offset: 0x001837D4
		public ElGamalParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.p = DerInteger.GetInstance(seq[0]);
			this.g = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x00185624 File Offset: 0x00183824
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x00185631 File Offset: 0x00183831
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0018563E File Offset: 0x0018383E
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.p,
				this.g
			});
		}

		// Token: 0x04002956 RID: 10582
		internal DerInteger p;

		// Token: 0x04002957 RID: 10583
		internal DerInteger g;
	}
}
