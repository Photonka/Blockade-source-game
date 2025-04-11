using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E0 RID: 1760
	public class MacData : Asn1Encodable
	{
		// Token: 0x060040C7 RID: 16583 RVA: 0x001835D5 File Offset: 0x001817D5
		public static MacData GetInstance(object obj)
		{
			if (obj is MacData)
			{
				return (MacData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MacData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x00183614 File Offset: 0x00181814
		private MacData(Asn1Sequence seq)
		{
			this.digInfo = DigestInfo.GetInstance(seq[0]);
			this.salt = ((Asn1OctetString)seq[1]).GetOctets();
			if (seq.Count == 3)
			{
				this.iterationCount = ((DerInteger)seq[2]).Value;
				return;
			}
			this.iterationCount = BigInteger.One;
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0018367C File Offset: 0x0018187C
		public MacData(DigestInfo digInfo, byte[] salt, int iterationCount)
		{
			this.digInfo = digInfo;
			this.salt = (byte[])salt.Clone();
			this.iterationCount = BigInteger.ValueOf((long)iterationCount);
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x001836A9 File Offset: 0x001818A9
		public DigestInfo Mac
		{
			get
			{
				return this.digInfo;
			}
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x001836B1 File Offset: 0x001818B1
		public byte[] GetSalt()
		{
			return (byte[])this.salt.Clone();
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x001836C3 File Offset: 0x001818C3
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x001836CC File Offset: 0x001818CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.digInfo,
				new DerOctetString(this.salt)
			});
			if (!this.iterationCount.Equals(BigInteger.One))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerInteger(this.iterationCount)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002889 RID: 10377
		internal DigestInfo digInfo;

		// Token: 0x0400288A RID: 10378
		internal byte[] salt;

		// Token: 0x0400288B RID: 10379
		internal BigInteger iterationCount;
	}
}
