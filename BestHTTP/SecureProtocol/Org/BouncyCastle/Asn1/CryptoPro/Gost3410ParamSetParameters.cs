using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x0200074C RID: 1868
	public class Gost3410ParamSetParameters : Asn1Encodable
	{
		// Token: 0x06004382 RID: 17282 RVA: 0x0018E0AD File Offset: 0x0018C2AD
		public static Gost3410ParamSetParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost3410ParamSetParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x0018E0BB File Offset: 0x0018C2BB
		public static Gost3410ParamSetParameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost3410ParamSetParameters)
			{
				return (Gost3410ParamSetParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost3410ParamSetParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x0018E0F8 File Offset: 0x0018C2F8
		public Gost3410ParamSetParameters(int keySize, BigInteger p, BigInteger q, BigInteger a)
		{
			this.keySize = keySize;
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.a = new DerInteger(a);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x0018E12C File Offset: 0x0018C32C
		private Gost3410ParamSetParameters(Asn1Sequence seq)
		{
			if (seq.Count != 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.keySize = DerInteger.GetInstance(seq[0]).Value.IntValue;
			this.p = DerInteger.GetInstance(seq[1]);
			this.q = DerInteger.GetInstance(seq[2]);
			this.a = DerInteger.GetInstance(seq[3]);
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x0018E1AA File Offset: 0x0018C3AA
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06004387 RID: 17287 RVA: 0x0018E1B2 File Offset: 0x0018C3B2
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x0018E1BF File Offset: 0x0018C3BF
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x0018E1CC File Offset: 0x0018C3CC
		public BigInteger A
		{
			get
			{
				return this.a.PositiveValue;
			}
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x0018E1D9 File Offset: 0x0018C3D9
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(this.keySize),
				this.p,
				this.q,
				this.a
			});
		}

		// Token: 0x04002B64 RID: 11108
		private readonly int keySize;

		// Token: 0x04002B65 RID: 11109
		private readonly DerInteger p;

		// Token: 0x04002B66 RID: 11110
		private readonly DerInteger q;

		// Token: 0x04002B67 RID: 11111
		private readonly DerInteger a;
	}
}
