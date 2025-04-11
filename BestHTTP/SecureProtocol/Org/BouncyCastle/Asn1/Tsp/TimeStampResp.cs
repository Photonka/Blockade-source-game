using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020006C6 RID: 1734
	public class TimeStampResp : Asn1Encodable
	{
		// Token: 0x06004029 RID: 16425 RVA: 0x00181289 File Offset: 0x0017F489
		public static TimeStampResp GetInstance(object o)
		{
			if (o == null || o is TimeStampResp)
			{
				return (TimeStampResp)o;
			}
			if (o is Asn1Sequence)
			{
				return new TimeStampResp((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'TimeStampResp' factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x001812C6 File Offset: 0x0017F4C6
		private TimeStampResp(Asn1Sequence seq)
		{
			this.pkiStatusInfo = PkiStatusInfo.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.timeStampToken = ContentInfo.GetInstance(seq[1]);
			}
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x001812FB File Offset: 0x0017F4FB
		public TimeStampResp(PkiStatusInfo pkiStatusInfo, ContentInfo timeStampToken)
		{
			this.pkiStatusInfo = pkiStatusInfo;
			this.timeStampToken = timeStampToken;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x00181311 File Offset: 0x0017F511
		public PkiStatusInfo Status
		{
			get
			{
				return this.pkiStatusInfo;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x00181319 File Offset: 0x0017F519
		public ContentInfo TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x00181324 File Offset: 0x0017F524
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pkiStatusInfo
			});
			if (this.timeStampToken != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.timeStampToken
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027F7 RID: 10231
		private readonly PkiStatusInfo pkiStatusInfo;

		// Token: 0x040027F8 RID: 10232
		private readonly ContentInfo timeStampToken;
	}
}
