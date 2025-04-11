using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.Ecc
{
	// Token: 0x02000793 RID: 1939
	public class MQVuserKeyingMaterial : Asn1Encodable
	{
		// Token: 0x060045A5 RID: 17829 RVA: 0x00193C8E File Offset: 0x00191E8E
		public MQVuserKeyingMaterial(OriginatorPublicKey ephemeralPublicKey, Asn1OctetString addedukm)
		{
			this.ephemeralPublicKey = ephemeralPublicKey;
			this.addedukm = addedukm;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00193CA4 File Offset: 0x00191EA4
		private MQVuserKeyingMaterial(Asn1Sequence seq)
		{
			this.ephemeralPublicKey = OriginatorPublicKey.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.addedukm = Asn1OctetString.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x00193CDF File Offset: 0x00191EDF
		public static MQVuserKeyingMaterial GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return MQVuserKeyingMaterial.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00193CED File Offset: 0x00191EED
		public static MQVuserKeyingMaterial GetInstance(object obj)
		{
			if (obj == null || obj is MQVuserKeyingMaterial)
			{
				return (MQVuserKeyingMaterial)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MQVuserKeyingMaterial((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid MQVuserKeyingMaterial: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x00193D2A File Offset: 0x00191F2A
		public OriginatorPublicKey EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x00193D32 File Offset: 0x00191F32
		public Asn1OctetString AddedUkm
		{
			get
			{
				return this.addedukm;
			}
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00193D3C File Offset: 0x00191F3C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.ephemeralPublicKey
			});
			if (this.addedukm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.addedukm)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C62 RID: 11362
		private OriginatorPublicKey ephemeralPublicKey;

		// Token: 0x04002C63 RID: 11363
		private Asn1OctetString addedukm;
	}
}
