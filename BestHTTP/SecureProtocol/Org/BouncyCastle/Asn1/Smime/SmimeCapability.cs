using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020006CD RID: 1741
	public class SmimeCapability : Asn1Encodable
	{
		// Token: 0x06004053 RID: 16467 RVA: 0x00181E13 File Offset: 0x00180013
		public SmimeCapability(Asn1Sequence seq)
		{
			this.capabilityID = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.parameters = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x00181E4D File Offset: 0x0018004D
		public SmimeCapability(DerObjectIdentifier capabilityID, Asn1Encodable parameters)
		{
			if (capabilityID == null)
			{
				throw new ArgumentNullException("capabilityID");
			}
			this.capabilityID = capabilityID;
			if (parameters != null)
			{
				this.parameters = parameters.ToAsn1Object();
			}
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x00181E79 File Offset: 0x00180079
		public static SmimeCapability GetInstance(object obj)
		{
			if (obj == null || obj is SmimeCapability)
			{
				return (SmimeCapability)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SmimeCapability((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid SmimeCapability");
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x00181EAB File Offset: 0x001800AB
		public DerObjectIdentifier CapabilityID
		{
			get
			{
				return this.capabilityID;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x00181EB3 File Offset: 0x001800B3
		public Asn1Object Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x00181EBC File Offset: 0x001800BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.capabilityID
			});
			if (this.parameters != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.parameters
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002830 RID: 10288
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.PreferSignedData;

		// Token: 0x04002831 RID: 10289
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.CannotDecryptAny;

		// Token: 0x04002832 RID: 10290
		public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = PkcsObjectIdentifiers.SmimeCapabilitiesVersions;

		// Token: 0x04002833 RID: 10291
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x04002834 RID: 10292
		public static readonly DerObjectIdentifier DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc;

		// Token: 0x04002835 RID: 10293
		public static readonly DerObjectIdentifier RC2Cbc = PkcsObjectIdentifiers.RC2Cbc;

		// Token: 0x04002836 RID: 10294
		private DerObjectIdentifier capabilityID;

		// Token: 0x04002837 RID: 10295
		private Asn1Object parameters;
	}
}
