using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x02000798 RID: 1944
	public class CertRepMessage : Asn1Encodable
	{
		// Token: 0x060045C5 RID: 17861 RVA: 0x00194194 File Offset: 0x00192394
		private CertRepMessage(Asn1Sequence seq)
		{
			int index = 0;
			if (seq.Count > 1)
			{
				this.caPubs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[index++], true);
			}
			this.response = Asn1Sequence.GetInstance(seq[index]);
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x001941E0 File Offset: 0x001923E0
		public static CertRepMessage GetInstance(object obj)
		{
			if (obj is CertRepMessage)
			{
				return (CertRepMessage)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertRepMessage((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x00194220 File Offset: 0x00192420
		public CertRepMessage(CmpCertificate[] caPubs, CertResponse[] response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (caPubs != null)
			{
				this.caPubs = new DerSequence(caPubs);
			}
			this.response = new DerSequence(response);
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x00194260 File Offset: 0x00192460
		public virtual CmpCertificate[] GetCAPubs()
		{
			if (this.caPubs == null)
			{
				return null;
			}
			CmpCertificate[] array = new CmpCertificate[this.caPubs.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CmpCertificate.GetInstance(this.caPubs[num]);
			}
			return array;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x001942AC File Offset: 0x001924AC
		public virtual CertResponse[] GetResponse()
		{
			CertResponse[] array = new CertResponse[this.response.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertResponse.GetInstance(this.response[num]);
			}
			return array;
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x001942F0 File Offset: 0x001924F0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.caPubs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.caPubs)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.response
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C6D RID: 11373
		private readonly Asn1Sequence caPubs;

		// Token: 0x04002C6E RID: 11374
		private readonly Asn1Sequence response;
	}
}
