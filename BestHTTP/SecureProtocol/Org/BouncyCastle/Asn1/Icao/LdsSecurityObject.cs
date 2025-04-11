using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200071D RID: 1821
	public class LdsSecurityObject : Asn1Encodable
	{
		// Token: 0x0600425D RID: 16989 RVA: 0x00189324 File Offset: 0x00187524
		public static LdsSecurityObject GetInstance(object obj)
		{
			if (obj is LdsSecurityObject)
			{
				return (LdsSecurityObject)obj;
			}
			if (obj != null)
			{
				return new LdsSecurityObject(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x00189348 File Offset: 0x00187548
		private LdsSecurityObject(Asn1Sequence seq)
		{
			if (seq == null || seq.Count == 0)
			{
				throw new ArgumentException("null or empty sequence passed.");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digestAlgorithmIdentifier = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			Asn1Sequence instance = Asn1Sequence.GetInstance(enumerator.Current);
			if (this.version.Value.Equals(BigInteger.One))
			{
				enumerator.MoveNext();
				this.versionInfo = LdsVersionInfo.GetInstance(enumerator.Current);
			}
			this.CheckDatagroupHashSeqSize(instance.Count);
			this.datagroupHash = new DataGroupHash[instance.Count];
			for (int i = 0; i < instance.Count; i++)
			{
				this.datagroupHash[i] = DataGroupHash.GetInstance(instance[i]);
			}
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x00189438 File Offset: 0x00187638
		public LdsSecurityObject(AlgorithmIdentifier digestAlgorithmIdentifier, DataGroupHash[] datagroupHash)
		{
			this.version = new DerInteger(0);
			this.digestAlgorithmIdentifier = digestAlgorithmIdentifier;
			this.datagroupHash = datagroupHash;
			this.CheckDatagroupHashSeqSize(datagroupHash.Length);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0018946F File Offset: 0x0018766F
		public LdsSecurityObject(AlgorithmIdentifier digestAlgorithmIdentifier, DataGroupHash[] datagroupHash, LdsVersionInfo versionInfo)
		{
			this.version = new DerInteger(1);
			this.digestAlgorithmIdentifier = digestAlgorithmIdentifier;
			this.datagroupHash = datagroupHash;
			this.versionInfo = versionInfo;
			this.CheckDatagroupHashSeqSize(datagroupHash.Length);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x001894AD File Offset: 0x001876AD
		private void CheckDatagroupHashSeqSize(int size)
		{
			if (size < 2 || size > 16)
			{
				throw new ArgumentException("wrong size in DataGroupHashValues : not in (2.." + 16 + ")");
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x001894D4 File Offset: 0x001876D4
		public BigInteger Version
		{
			get
			{
				return this.version.Value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x001894E1 File Offset: 0x001876E1
		public AlgorithmIdentifier DigestAlgorithmIdentifier
		{
			get
			{
				return this.digestAlgorithmIdentifier;
			}
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x001894E9 File Offset: 0x001876E9
		public DataGroupHash[] GetDatagroupHash()
		{
			return this.datagroupHash;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x001894F1 File Offset: 0x001876F1
		public LdsVersionInfo VersionInfo
		{
			get
			{
				return this.versionInfo;
			}
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x001894FC File Offset: 0x001876FC
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] v = this.datagroupHash;
			DerSequence derSequence = new DerSequence(v);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithmIdentifier,
				derSequence
			});
			if (this.versionInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.versionInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002A65 RID: 10853
		public const int UBDataGroups = 16;

		// Token: 0x04002A66 RID: 10854
		private DerInteger version = new DerInteger(0);

		// Token: 0x04002A67 RID: 10855
		private AlgorithmIdentifier digestAlgorithmIdentifier;

		// Token: 0x04002A68 RID: 10856
		private DataGroupHash[] datagroupHash;

		// Token: 0x04002A69 RID: 10857
		private LdsVersionInfo versionInfo;
	}
}
