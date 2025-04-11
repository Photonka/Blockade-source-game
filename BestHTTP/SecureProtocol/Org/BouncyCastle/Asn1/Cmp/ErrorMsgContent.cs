using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200079F RID: 1951
	public class ErrorMsgContent : Asn1Encodable
	{
		// Token: 0x060045EC RID: 17900 RVA: 0x00194A28 File Offset: 0x00192C28
		private ErrorMsgContent(Asn1Sequence seq)
		{
			this.pkiStatusInfo = PkiStatusInfo.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				Asn1Encodable asn1Encodable = seq[i];
				if (asn1Encodable is DerInteger)
				{
					this.errorCode = DerInteger.GetInstance(asn1Encodable);
				}
				else
				{
					this.errorDetails = PkiFreeText.GetInstance(asn1Encodable);
				}
			}
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00194A88 File Offset: 0x00192C88
		public static ErrorMsgContent GetInstance(object obj)
		{
			if (obj is ErrorMsgContent)
			{
				return (ErrorMsgContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ErrorMsgContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00194AC7 File Offset: 0x00192CC7
		public ErrorMsgContent(PkiStatusInfo pkiStatusInfo) : this(pkiStatusInfo, null, null)
		{
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00194AD2 File Offset: 0x00192CD2
		public ErrorMsgContent(PkiStatusInfo pkiStatusInfo, DerInteger errorCode, PkiFreeText errorDetails)
		{
			if (pkiStatusInfo == null)
			{
				throw new ArgumentNullException("pkiStatusInfo");
			}
			this.pkiStatusInfo = pkiStatusInfo;
			this.errorCode = errorCode;
			this.errorDetails = errorDetails;
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x00194AFD File Offset: 0x00192CFD
		public virtual PkiStatusInfo PkiStatusInfo
		{
			get
			{
				return this.pkiStatusInfo;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x00194B05 File Offset: 0x00192D05
		public virtual DerInteger ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x00194B0D File Offset: 0x00192D0D
		public virtual PkiFreeText ErrorDetails
		{
			get
			{
				return this.errorDetails;
			}
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00194B18 File Offset: 0x00192D18
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pkiStatusInfo
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.errorCode,
				this.errorDetails
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C96 RID: 11414
		private readonly PkiStatusInfo pkiStatusInfo;

		// Token: 0x04002C97 RID: 11415
		private readonly DerInteger errorCode;

		// Token: 0x04002C98 RID: 11416
		private readonly PkiFreeText errorDetails;
	}
}
