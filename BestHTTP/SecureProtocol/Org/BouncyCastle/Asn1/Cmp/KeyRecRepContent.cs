using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A3 RID: 1955
	public class KeyRecRepContent : Asn1Encodable
	{
		// Token: 0x06004605 RID: 17925 RVA: 0x00194DC8 File Offset: 0x00192FC8
		private KeyRecRepContent(Asn1Sequence seq)
		{
			this.status = PkiStatusInfo.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[i]);
				switch (instance.TagNo)
				{
				case 0:
					this.newSigCert = CmpCertificate.GetInstance(instance.GetObject());
					break;
				case 1:
					this.caCerts = Asn1Sequence.GetInstance(instance.GetObject());
					break;
				case 2:
					this.keyPairHist = Asn1Sequence.GetInstance(instance.GetObject());
					break;
				default:
					throw new ArgumentException("unknown tag number: " + instance.TagNo, "seq");
				}
			}
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x00194E85 File Offset: 0x00193085
		public static KeyRecRepContent GetInstance(object obj)
		{
			if (obj is KeyRecRepContent)
			{
				return (KeyRecRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyRecRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x00194EC4 File Offset: 0x001930C4
		public virtual PkiStatusInfo Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x00194ECC File Offset: 0x001930CC
		public virtual CmpCertificate NewSigCert
		{
			get
			{
				return this.newSigCert;
			}
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x00194ED4 File Offset: 0x001930D4
		public virtual CmpCertificate[] GetCACerts()
		{
			if (this.caCerts == null)
			{
				return null;
			}
			CmpCertificate[] array = new CmpCertificate[this.caCerts.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CmpCertificate.GetInstance(this.caCerts[num]);
			}
			return array;
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00194F20 File Offset: 0x00193120
		public virtual CertifiedKeyPair[] GetKeyPairHist()
		{
			if (this.keyPairHist == null)
			{
				return null;
			}
			CertifiedKeyPair[] array = new CertifiedKeyPair[this.keyPairHist.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertifiedKeyPair.GetInstance(this.keyPairHist[num]);
			}
			return array;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x00194F6C File Offset: 0x0019316C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			this.AddOptional(v, 0, this.newSigCert);
			this.AddOptional(v, 1, this.caCerts);
			this.AddOptional(v, 2, this.keyPairHist);
			return new DerSequence(v);
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00194FBE File Offset: 0x001931BE
		private void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, tagNo, obj)
				});
			}
		}

		// Token: 0x04002C9D RID: 11421
		private readonly PkiStatusInfo status;

		// Token: 0x04002C9E RID: 11422
		private readonly CmpCertificate newSigCert;

		// Token: 0x04002C9F RID: 11423
		private readonly Asn1Sequence caCerts;

		// Token: 0x04002CA0 RID: 11424
		private readonly Asn1Sequence keyPairHist;
	}
}
