using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000749 RID: 1865
	public class ECGost3410ParamSetParameters : Asn1Encodable
	{
		// Token: 0x06004370 RID: 17264 RVA: 0x0018DCAB File Offset: 0x0018BEAB
		public static ECGost3410ParamSetParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ECGost3410ParamSetParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x0018DCB9 File Offset: 0x0018BEB9
		public static ECGost3410ParamSetParameters GetInstance(object obj)
		{
			if (obj == null || obj is ECGost3410ParamSetParameters)
			{
				return (ECGost3410ParamSetParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ECGost3410ParamSetParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x0018DCF8 File Offset: 0x0018BEF8
		public ECGost3410ParamSetParameters(BigInteger a, BigInteger b, BigInteger p, BigInteger q, int x, BigInteger y)
		{
			this.a = new DerInteger(a);
			this.b = new DerInteger(b);
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.x = new DerInteger(x);
			this.y = new DerInteger(y);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x0018DD58 File Offset: 0x0018BF58
		public ECGost3410ParamSetParameters(Asn1Sequence seq)
		{
			if (seq.Count != 6)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.a = DerInteger.GetInstance(seq[0]);
			this.b = DerInteger.GetInstance(seq[1]);
			this.p = DerInteger.GetInstance(seq[2]);
			this.q = DerInteger.GetInstance(seq[3]);
			this.x = DerInteger.GetInstance(seq[4]);
			this.y = DerInteger.GetInstance(seq[5]);
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x0018DDF0 File Offset: 0x0018BFF0
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x0018DDFD File Offset: 0x0018BFFD
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x0018DE0A File Offset: 0x0018C00A
		public BigInteger A
		{
			get
			{
				return this.a.PositiveValue;
			}
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x0018DE18 File Offset: 0x0018C018
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.a,
				this.b,
				this.p,
				this.q,
				this.x,
				this.y
			});
		}

		// Token: 0x04002B57 RID: 11095
		internal readonly DerInteger p;

		// Token: 0x04002B58 RID: 11096
		internal readonly DerInteger q;

		// Token: 0x04002B59 RID: 11097
		internal readonly DerInteger a;

		// Token: 0x04002B5A RID: 11098
		internal readonly DerInteger b;

		// Token: 0x04002B5B RID: 11099
		internal readonly DerInteger x;

		// Token: 0x04002B5C RID: 11100
		internal readonly DerInteger y;
	}
}
