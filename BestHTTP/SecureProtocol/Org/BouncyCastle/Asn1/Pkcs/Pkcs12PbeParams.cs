using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E5 RID: 1765
	public class Pkcs12PbeParams : Asn1Encodable
	{
		// Token: 0x060040EC RID: 16620 RVA: 0x00183C5A File Offset: 0x00181E5A
		public Pkcs12PbeParams(byte[] salt, int iterations)
		{
			this.iv = new DerOctetString(salt);
			this.iterations = new DerInteger(iterations);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x00183C7C File Offset: 0x00181E7C
		private Pkcs12PbeParams(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = Asn1OctetString.GetInstance(seq[0]);
			this.iterations = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x00183CCC File Offset: 0x00181ECC
		public static Pkcs12PbeParams GetInstance(object obj)
		{
			if (obj is Pkcs12PbeParams)
			{
				return (Pkcs12PbeParams)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Pkcs12PbeParams((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x00183D0B File Offset: 0x00181F0B
		public BigInteger Iterations
		{
			get
			{
				return this.iterations.Value;
			}
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x00183D18 File Offset: 0x00181F18
		public byte[] GetIV()
		{
			return this.iv.GetOctets();
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x00183D25 File Offset: 0x00181F25
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.iterations
			});
		}

		// Token: 0x04002897 RID: 10391
		private readonly DerInteger iterations;

		// Token: 0x04002898 RID: 10392
		private readonly Asn1OctetString iv;
	}
}
