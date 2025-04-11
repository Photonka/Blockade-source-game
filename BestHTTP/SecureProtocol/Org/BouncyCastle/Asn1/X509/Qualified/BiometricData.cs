using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006B7 RID: 1719
	public class BiometricData : Asn1Encodable
	{
		// Token: 0x06003FC1 RID: 16321 RVA: 0x0017F35C File Offset: 0x0017D55C
		public static BiometricData GetInstance(object obj)
		{
			if (obj == null || obj is BiometricData)
			{
				return (BiometricData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BiometricData(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x0017F3AC File Offset: 0x0017D5AC
		private BiometricData(Asn1Sequence seq)
		{
			this.typeOfBiometricData = TypeOfBiometricData.GetInstance(seq[0]);
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.biometricDataHash = Asn1OctetString.GetInstance(seq[2]);
			if (seq.Count > 3)
			{
				this.sourceDataUri = DerIA5String.GetInstance(seq[3]);
			}
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0017F410 File Offset: 0x0017D610
		public BiometricData(TypeOfBiometricData typeOfBiometricData, AlgorithmIdentifier hashAlgorithm, Asn1OctetString biometricDataHash, DerIA5String sourceDataUri)
		{
			this.typeOfBiometricData = typeOfBiometricData;
			this.hashAlgorithm = hashAlgorithm;
			this.biometricDataHash = biometricDataHash;
			this.sourceDataUri = sourceDataUri;
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x0017F435 File Offset: 0x0017D635
		public BiometricData(TypeOfBiometricData typeOfBiometricData, AlgorithmIdentifier hashAlgorithm, Asn1OctetString biometricDataHash)
		{
			this.typeOfBiometricData = typeOfBiometricData;
			this.hashAlgorithm = hashAlgorithm;
			this.biometricDataHash = biometricDataHash;
			this.sourceDataUri = null;
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x0017F459 File Offset: 0x0017D659
		public TypeOfBiometricData TypeOfBiometricData
		{
			get
			{
				return this.typeOfBiometricData;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x0017F461 File Offset: 0x0017D661
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0017F469 File Offset: 0x0017D669
		public Asn1OctetString BiometricDataHash
		{
			get
			{
				return this.biometricDataHash;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x0017F471 File Offset: 0x0017D671
		public DerIA5String SourceDataUri
		{
			get
			{
				return this.sourceDataUri;
			}
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0017F47C File Offset: 0x0017D67C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.typeOfBiometricData,
				this.hashAlgorithm,
				this.biometricDataHash
			});
			if (this.sourceDataUri != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.sourceDataUri
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027A2 RID: 10146
		private readonly TypeOfBiometricData typeOfBiometricData;

		// Token: 0x040027A3 RID: 10147
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x040027A4 RID: 10148
		private readonly Asn1OctetString biometricDataHash;

		// Token: 0x040027A5 RID: 10149
		private readonly DerIA5String sourceDataUri;
	}
}
