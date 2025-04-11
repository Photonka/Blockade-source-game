using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A8 RID: 1704
	public class V2TbsCertListGenerator
	{
		// Token: 0x06003F32 RID: 16178 RVA: 0x0017C40F File Offset: 0x0017A60F
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x0017C418 File Offset: 0x0017A618
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x0017C421 File Offset: 0x0017A621
		public void SetThisUpdate(DerUtcTime thisUpdate)
		{
			this.thisUpdate = new Time(thisUpdate);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0017C42F File Offset: 0x0017A62F
		public void SetNextUpdate(DerUtcTime nextUpdate)
		{
			this.nextUpdate = ((nextUpdate != null) ? new Time(nextUpdate) : null);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0017C443 File Offset: 0x0017A643
		public void SetThisUpdate(Time thisUpdate)
		{
			this.thisUpdate = thisUpdate;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0017C44C File Offset: 0x0017A64C
		public void SetNextUpdate(Time nextUpdate)
		{
			this.nextUpdate = nextUpdate;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x0017C455 File Offset: 0x0017A655
		public void AddCrlEntry(Asn1Sequence crlEntry)
		{
			if (this.crlEntries == null)
			{
				this.crlEntries = Platform.CreateArrayList();
			}
			this.crlEntries.Add(crlEntry);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0017C477 File Offset: 0x0017A677
		public void AddCrlEntry(DerInteger userCertificate, DerUtcTime revocationDate, int reason)
		{
			this.AddCrlEntry(userCertificate, new Time(revocationDate), reason);
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0017C487 File Offset: 0x0017A687
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, int reason)
		{
			this.AddCrlEntry(userCertificate, revocationDate, reason, null);
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0017C494 File Offset: 0x0017A694
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, int reason, DerGeneralizedTime invalidityDate)
		{
			IList list = Platform.CreateArrayList();
			IList list2 = Platform.CreateArrayList();
			if (reason != 0)
			{
				CrlReason crlReason = new CrlReason(reason);
				try
				{
					list.Add(X509Extensions.ReasonCode);
					list2.Add(new X509Extension(false, new DerOctetString(crlReason.GetEncoded())));
				}
				catch (IOException arg)
				{
					throw new ArgumentException("error encoding reason: " + arg);
				}
			}
			if (invalidityDate != null)
			{
				try
				{
					list.Add(X509Extensions.InvalidityDate);
					list2.Add(new X509Extension(false, new DerOctetString(invalidityDate.GetEncoded())));
				}
				catch (IOException arg2)
				{
					throw new ArgumentException("error encoding invalidityDate: " + arg2);
				}
			}
			if (list.Count != 0)
			{
				this.AddCrlEntry(userCertificate, revocationDate, new X509Extensions(list, list2));
				return;
			}
			this.AddCrlEntry(userCertificate, revocationDate, null);
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0017C56C File Offset: 0x0017A76C
		public void AddCrlEntry(DerInteger userCertificate, Time revocationDate, X509Extensions extensions)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				userCertificate,
				revocationDate
			});
			if (extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					extensions
				});
			}
			this.AddCrlEntry(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x0017C5AC File Offset: 0x0017A7AC
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0017C5B8 File Offset: 0x0017A7B8
		public TbsCertificateList GenerateTbsCertList()
		{
			if (this.signature == null || this.issuer == null || this.thisUpdate == null)
			{
				throw new InvalidOperationException("Not all mandatory fields set in V2 TbsCertList generator.");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.signature,
				this.issuer,
				this.thisUpdate
			});
			if (this.nextUpdate != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.nextUpdate
				});
			}
			if (this.crlEntries != null)
			{
				Asn1Sequence[] array = new Asn1Sequence[this.crlEntries.Count];
				for (int i = 0; i < this.crlEntries.Count; i++)
				{
					array[i] = (Asn1Sequence)this.crlEntries[i];
				}
				Asn1EncodableVector asn1EncodableVector2 = asn1EncodableVector;
				Asn1Encodable[] array2 = new Asn1Encodable[1];
				int num = 0;
				Asn1Encodable[] v = array;
				array2[num] = new DerSequence(v);
				asn1EncodableVector2.Add(array2);
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.extensions)
				});
			}
			return new TbsCertificateList(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x0400270F RID: 9999
		private DerInteger version = new DerInteger(1);

		// Token: 0x04002710 RID: 10000
		private AlgorithmIdentifier signature;

		// Token: 0x04002711 RID: 10001
		private X509Name issuer;

		// Token: 0x04002712 RID: 10002
		private Time thisUpdate;

		// Token: 0x04002713 RID: 10003
		private Time nextUpdate;

		// Token: 0x04002714 RID: 10004
		private X509Extensions extensions;

		// Token: 0x04002715 RID: 10005
		private IList crlEntries;
	}
}
