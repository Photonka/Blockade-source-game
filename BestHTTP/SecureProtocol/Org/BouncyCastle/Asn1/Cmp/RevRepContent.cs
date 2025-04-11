using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B8 RID: 1976
	public class RevRepContent : Asn1Encodable
	{
		// Token: 0x060046A5 RID: 18085 RVA: 0x001965FC File Offset: 0x001947FC
		private RevRepContent(Asn1Sequence seq)
		{
			this.status = Asn1Sequence.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[i]);
				if (instance.TagNo == 0)
				{
					this.revCerts = Asn1Sequence.GetInstance(instance, true);
				}
				else
				{
					this.crls = Asn1Sequence.GetInstance(instance, true);
				}
			}
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00196663 File Offset: 0x00194863
		public static RevRepContent GetInstance(object obj)
		{
			if (obj is RevRepContent)
			{
				return (RevRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x001966A4 File Offset: 0x001948A4
		public virtual PkiStatusInfo[] GetStatus()
		{
			PkiStatusInfo[] array = new PkiStatusInfo[this.status.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = PkiStatusInfo.GetInstance(this.status[num]);
			}
			return array;
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x001966E8 File Offset: 0x001948E8
		public virtual CertId[] GetRevCerts()
		{
			if (this.revCerts == null)
			{
				return null;
			}
			CertId[] array = new CertId[this.revCerts.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertId.GetInstance(this.revCerts[num]);
			}
			return array;
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00196734 File Offset: 0x00194934
		public virtual CertificateList[] GetCrls()
		{
			if (this.crls == null)
			{
				return null;
			}
			CertificateList[] array = new CertificateList[this.crls.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertificateList.GetInstance(this.crls[num]);
			}
			return array;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00196780 File Offset: 0x00194980
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			this.AddOptional(v, 0, this.revCerts);
			this.AddOptional(v, 1, this.crls);
			return new DerSequence(v);
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x00194FBE File Offset: 0x001931BE
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

		// Token: 0x04002D23 RID: 11555
		private readonly Asn1Sequence status;

		// Token: 0x04002D24 RID: 11556
		private readonly Asn1Sequence revCerts;

		// Token: 0x04002D25 RID: 11557
		private readonly Asn1Sequence crls;
	}
}
