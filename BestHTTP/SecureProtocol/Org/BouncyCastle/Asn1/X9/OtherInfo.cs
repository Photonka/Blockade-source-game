using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000663 RID: 1635
	public class OtherInfo : Asn1Encodable
	{
		// Token: 0x06003D16 RID: 15638 RVA: 0x0017599D File Offset: 0x00173B9D
		public OtherInfo(KeySpecificInfo keyInfo, Asn1OctetString partyAInfo, Asn1OctetString suppPubInfo)
		{
			this.keyInfo = keyInfo;
			this.partyAInfo = partyAInfo;
			this.suppPubInfo = suppPubInfo;
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x001759BC File Offset: 0x00173BBC
		public OtherInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.keyInfo = new KeySpecificInfo((Asn1Sequence)enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DerTaggedObject derTaggedObject = (DerTaggedObject)obj;
				if (derTaggedObject.TagNo == 0)
				{
					this.partyAInfo = (Asn1OctetString)derTaggedObject.GetObject();
				}
				else if (derTaggedObject.TagNo == 2)
				{
					this.suppPubInfo = (Asn1OctetString)derTaggedObject.GetObject();
				}
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x00175A3E File Offset: 0x00173C3E
		public KeySpecificInfo KeyInfo
		{
			get
			{
				return this.keyInfo;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x00175A46 File Offset: 0x00173C46
		public Asn1OctetString PartyAInfo
		{
			get
			{
				return this.partyAInfo;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x00175A4E File Offset: 0x00173C4E
		public Asn1OctetString SuppPubInfo
		{
			get
			{
				return this.suppPubInfo;
			}
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x00175A58 File Offset: 0x00173C58
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.keyInfo
			});
			if (this.partyAInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.partyAInfo)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(2, this.suppPubInfo)
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040025DD RID: 9693
		private KeySpecificInfo keyInfo;

		// Token: 0x040025DE RID: 9694
		private Asn1OctetString partyAInfo;

		// Token: 0x040025DF RID: 9695
		private Asn1OctetString suppPubInfo;
	}
}
