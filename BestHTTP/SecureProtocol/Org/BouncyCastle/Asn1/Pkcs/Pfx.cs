using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E4 RID: 1764
	public class Pfx : Asn1Encodable
	{
		// Token: 0x060040E7 RID: 16615 RVA: 0x00183B80 File Offset: 0x00181D80
		public Pfx(Asn1Sequence seq)
		{
			if (((DerInteger)seq[0]).Value.IntValue != 3)
			{
				throw new ArgumentException("wrong version for PFX PDU");
			}
			this.contentInfo = ContentInfo.GetInstance(seq[1]);
			if (seq.Count == 3)
			{
				this.macData = MacData.GetInstance(seq[2]);
			}
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00183BE4 File Offset: 0x00181DE4
		public Pfx(ContentInfo contentInfo, MacData macData)
		{
			this.contentInfo = contentInfo;
			this.macData = macData;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x00183BFA File Offset: 0x00181DFA
		public ContentInfo AuthSafe
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x00183C02 File Offset: 0x00181E02
		public MacData MacData
		{
			get
			{
				return this.macData;
			}
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x00183C0C File Offset: 0x00181E0C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(3),
				this.contentInfo
			});
			if (this.macData != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.macData
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04002895 RID: 10389
		private ContentInfo contentInfo;

		// Token: 0x04002896 RID: 10390
		private MacData macData;
	}
}
