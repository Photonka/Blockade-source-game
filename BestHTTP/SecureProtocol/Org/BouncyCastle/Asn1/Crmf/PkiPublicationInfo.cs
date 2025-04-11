using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200075C RID: 1884
	public class PkiPublicationInfo : Asn1Encodable
	{
		// Token: 0x060043FF RID: 17407 RVA: 0x0018F298 File Offset: 0x0018D498
		private PkiPublicationInfo(Asn1Sequence seq)
		{
			this.action = DerInteger.GetInstance(seq[0]);
			this.pubInfos = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x0018F2C4 File Offset: 0x0018D4C4
		public static PkiPublicationInfo GetInstance(object obj)
		{
			if (obj is PkiPublicationInfo)
			{
				return (PkiPublicationInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiPublicationInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x0018F303 File Offset: 0x0018D503
		public virtual DerInteger Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0018F30C File Offset: 0x0018D50C
		public virtual SinglePubInfo[] GetPubInfos()
		{
			if (this.pubInfos == null)
			{
				return null;
			}
			SinglePubInfo[] array = new SinglePubInfo[this.pubInfos.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = SinglePubInfo.GetInstance(this.pubInfos[num]);
			}
			return array;
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x0018F357 File Offset: 0x0018D557
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.action,
				this.pubInfos
			});
		}

		// Token: 0x04002BA4 RID: 11172
		private readonly DerInteger action;

		// Token: 0x04002BA5 RID: 11173
		private readonly Asn1Sequence pubInfos;
	}
}
