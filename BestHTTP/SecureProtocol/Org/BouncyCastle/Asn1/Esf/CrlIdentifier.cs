using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000731 RID: 1841
	public class CrlIdentifier : Asn1Encodable
	{
		// Token: 0x060042D9 RID: 17113 RVA: 0x0018B278 File Offset: 0x00189478
		public static CrlIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is CrlIdentifier)
			{
				return (CrlIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlIdentifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0018B2C8 File Offset: 0x001894C8
		private CrlIdentifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crlIssuer = X509Name.GetInstance(seq[0]);
			this.crlIssuedTime = DerUtcTime.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.crlNumber = DerInteger.GetInstance(seq[2]);
			}
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0018B35A File Offset: 0x0018955A
		public CrlIdentifier(X509Name crlIssuer, DateTime crlIssuedTime) : this(crlIssuer, crlIssuedTime, null)
		{
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0018B365 File Offset: 0x00189565
		public CrlIdentifier(X509Name crlIssuer, DateTime crlIssuedTime, BigInteger crlNumber)
		{
			if (crlIssuer == null)
			{
				throw new ArgumentNullException("crlIssuer");
			}
			this.crlIssuer = crlIssuer;
			this.crlIssuedTime = new DerUtcTime(crlIssuedTime);
			if (crlNumber != null)
			{
				this.crlNumber = new DerInteger(crlNumber);
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x0018B39D File Offset: 0x0018959D
		public X509Name CrlIssuer
		{
			get
			{
				return this.crlIssuer;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x0018B3A5 File Offset: 0x001895A5
		public DateTime CrlIssuedTime
		{
			get
			{
				return this.crlIssuedTime.ToAdjustedDateTime();
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0018B3B2 File Offset: 0x001895B2
		public BigInteger CrlNumber
		{
			get
			{
				if (this.crlNumber != null)
				{
					return this.crlNumber.Value;
				}
				return null;
			}
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0018B3CC File Offset: 0x001895CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.crlIssuer.ToAsn1Object(),
				this.crlIssuedTime
			});
			if (this.crlNumber != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.crlNumber
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002AEC RID: 10988
		private readonly X509Name crlIssuer;

		// Token: 0x04002AED RID: 10989
		private readonly DerUtcTime crlIssuedTime;

		// Token: 0x04002AEE RID: 10990
		private readonly DerInteger crlNumber;
	}
}
