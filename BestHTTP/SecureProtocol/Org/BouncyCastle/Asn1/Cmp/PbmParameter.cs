using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A5 RID: 1957
	public class PbmParameter : Asn1Encodable
	{
		// Token: 0x06004613 RID: 17939 RVA: 0x001950EC File Offset: 0x001932EC
		private PbmParameter(Asn1Sequence seq)
		{
			this.salt = Asn1OctetString.GetInstance(seq[0]);
			this.owf = AlgorithmIdentifier.GetInstance(seq[1]);
			this.iterationCount = DerInteger.GetInstance(seq[2]);
			this.mac = AlgorithmIdentifier.GetInstance(seq[3]);
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x00195147 File Offset: 0x00193347
		public static PbmParameter GetInstance(object obj)
		{
			if (obj is PbmParameter)
			{
				return (PbmParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PbmParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00195186 File Offset: 0x00193386
		public PbmParameter(byte[] salt, AlgorithmIdentifier owf, int iterationCount, AlgorithmIdentifier mac) : this(new DerOctetString(salt), owf, new DerInteger(iterationCount), mac)
		{
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x0019519D File Offset: 0x0019339D
		public PbmParameter(Asn1OctetString salt, AlgorithmIdentifier owf, DerInteger iterationCount, AlgorithmIdentifier mac)
		{
			this.salt = salt;
			this.owf = owf;
			this.iterationCount = iterationCount;
			this.mac = mac;
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x001951C2 File Offset: 0x001933C2
		public virtual Asn1OctetString Salt
		{
			get
			{
				return this.salt;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x001951CA File Offset: 0x001933CA
		public virtual AlgorithmIdentifier Owf
		{
			get
			{
				return this.owf;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x001951D2 File Offset: 0x001933D2
		public virtual DerInteger IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x001951DA File Offset: 0x001933DA
		public virtual AlgorithmIdentifier Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x001951E2 File Offset: 0x001933E2
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.salt,
				this.owf,
				this.iterationCount,
				this.mac
			});
		}

		// Token: 0x04002CA4 RID: 11428
		private Asn1OctetString salt;

		// Token: 0x04002CA5 RID: 11429
		private AlgorithmIdentifier owf;

		// Token: 0x04002CA6 RID: 11430
		private DerInteger iterationCount;

		// Token: 0x04002CA7 RID: 11431
		private AlgorithmIdentifier mac;
	}
}
