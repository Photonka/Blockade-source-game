using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B7 RID: 1975
	public class RevDetails : Asn1Encodable
	{
		// Token: 0x0600469E RID: 18078 RVA: 0x00196514 File Offset: 0x00194714
		private RevDetails(Asn1Sequence seq)
		{
			this.certDetails = CertTemplate.GetInstance(seq[0]);
			this.crlEntryDetails = ((seq.Count <= 1) ? null : X509Extensions.GetInstance(seq[1]));
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x0019654C File Offset: 0x0019474C
		public static RevDetails GetInstance(object obj)
		{
			if (obj is RevDetails)
			{
				return (RevDetails)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevDetails((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x0019658B File Offset: 0x0019478B
		public RevDetails(CertTemplate certDetails) : this(certDetails, null)
		{
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00196595 File Offset: 0x00194795
		public RevDetails(CertTemplate certDetails, X509Extensions crlEntryDetails)
		{
			this.certDetails = certDetails;
			this.crlEntryDetails = crlEntryDetails;
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x001965AB File Offset: 0x001947AB
		public virtual CertTemplate CertDetails
		{
			get
			{
				return this.certDetails;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x001965B3 File Offset: 0x001947B3
		public virtual X509Extensions CrlEntryDetails
		{
			get
			{
				return this.crlEntryDetails;
			}
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x001965BC File Offset: 0x001947BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certDetails
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crlEntryDetails
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002D21 RID: 11553
		private readonly CertTemplate certDetails;

		// Token: 0x04002D22 RID: 11554
		private readonly X509Extensions crlEntryDetails;
	}
}
