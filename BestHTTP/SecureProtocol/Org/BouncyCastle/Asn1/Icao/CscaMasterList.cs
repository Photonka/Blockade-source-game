using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200071A RID: 1818
	public class CscaMasterList : Asn1Encodable
	{
		// Token: 0x0600424E RID: 16974 RVA: 0x0018903C File Offset: 0x0018723C
		public static CscaMasterList GetInstance(object obj)
		{
			if (obj is CscaMasterList)
			{
				return (CscaMasterList)obj;
			}
			if (obj != null)
			{
				return new CscaMasterList(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00189060 File Offset: 0x00187260
		private CscaMasterList(Asn1Sequence seq)
		{
			if (seq == null || seq.Count == 0)
			{
				throw new ArgumentException("null or empty sequence passed.");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Incorrect sequence size: " + seq.Count);
			}
			this.version = DerInteger.GetInstance(seq[0]);
			Asn1Set instance = Asn1Set.GetInstance(seq[1]);
			this.certList = new X509CertificateStructure[instance.Count];
			for (int i = 0; i < this.certList.Length; i++)
			{
				this.certList[i] = X509CertificateStructure.GetInstance(instance[i]);
			}
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x00189110 File Offset: 0x00187310
		public CscaMasterList(X509CertificateStructure[] certStructs)
		{
			this.certList = CscaMasterList.CopyCertList(certStructs);
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x00189130 File Offset: 0x00187330
		public virtual int Version
		{
			get
			{
				return this.version.Value.IntValue;
			}
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x00189142 File Offset: 0x00187342
		public X509CertificateStructure[] GetCertStructs()
		{
			return CscaMasterList.CopyCertList(this.certList);
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x0018914F File Offset: 0x0018734F
		private static X509CertificateStructure[] CopyCertList(X509CertificateStructure[] orig)
		{
			return (X509CertificateStructure[])orig.Clone();
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x0018915C File Offset: 0x0018735C
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] array = new Asn1Encodable[2];
			array[0] = this.version;
			int num = 1;
			Asn1Encodable[] v = this.certList;
			array[num] = new DerSet(v);
			return new DerSequence(array);
		}

		// Token: 0x04002A57 RID: 10839
		private DerInteger version = new DerInteger(0);

		// Token: 0x04002A58 RID: 10840
		private X509CertificateStructure[] certList;
	}
}
