using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Mozilla
{
	// Token: 0x02000705 RID: 1797
	public class PublicKeyAndChallenge : Asn1Encodable
	{
		// Token: 0x060041DD RID: 16861 RVA: 0x001872A3 File Offset: 0x001854A3
		public static PublicKeyAndChallenge GetInstance(object obj)
		{
			if (obj is PublicKeyAndChallenge)
			{
				return (PublicKeyAndChallenge)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PublicKeyAndChallenge((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in 'PublicKeyAndChallenge' factory : " + Platform.GetTypeName(obj) + ".");
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x001872E2 File Offset: 0x001854E2
		public PublicKeyAndChallenge(Asn1Sequence seq)
		{
			this.pkacSeq = seq;
			this.spki = SubjectPublicKeyInfo.GetInstance(seq[0]);
			this.challenge = DerIA5String.GetInstance(seq[1]);
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x00187315 File Offset: 0x00185515
		public override Asn1Object ToAsn1Object()
		{
			return this.pkacSeq;
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x0018731D File Offset: 0x0018551D
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.spki;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x00187325 File Offset: 0x00185525
		public DerIA5String Challenge
		{
			get
			{
				return this.challenge;
			}
		}

		// Token: 0x040029D7 RID: 10711
		private Asn1Sequence pkacSeq;

		// Token: 0x040029D8 RID: 10712
		private SubjectPublicKeyInfo spki;

		// Token: 0x040029D9 RID: 10713
		private DerIA5String challenge;
	}
}
