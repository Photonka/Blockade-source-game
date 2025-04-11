using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007AB RID: 1963
	public class PkiHeaderBuilder
	{
		// Token: 0x06004645 RID: 17989 RVA: 0x001958CF File Offset: 0x00193ACF
		public PkiHeaderBuilder(int pvno, GeneralName sender, GeneralName recipient) : this(new DerInteger(pvno), sender, recipient)
		{
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x001958DF File Offset: 0x00193ADF
		private PkiHeaderBuilder(DerInteger pvno, GeneralName sender, GeneralName recipient)
		{
			this.pvno = pvno;
			this.sender = sender;
			this.recipient = recipient;
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x001958FC File Offset: 0x00193AFC
		public virtual PkiHeaderBuilder SetMessageTime(DerGeneralizedTime time)
		{
			this.messageTime = time;
			return this;
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x00195906 File Offset: 0x00193B06
		public virtual PkiHeaderBuilder SetProtectionAlg(AlgorithmIdentifier aid)
		{
			this.protectionAlg = aid;
			return this;
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x00195910 File Offset: 0x00193B10
		public virtual PkiHeaderBuilder SetSenderKID(byte[] kid)
		{
			return this.SetSenderKID((kid == null) ? null : new DerOctetString(kid));
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x00195924 File Offset: 0x00193B24
		public virtual PkiHeaderBuilder SetSenderKID(Asn1OctetString kid)
		{
			this.senderKID = kid;
			return this;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0019592E File Offset: 0x00193B2E
		public virtual PkiHeaderBuilder SetRecipKID(byte[] kid)
		{
			return this.SetRecipKID((kid == null) ? null : new DerOctetString(kid));
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00195942 File Offset: 0x00193B42
		public virtual PkiHeaderBuilder SetRecipKID(DerOctetString kid)
		{
			this.recipKID = kid;
			return this;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0019594C File Offset: 0x00193B4C
		public virtual PkiHeaderBuilder SetTransactionID(byte[] tid)
		{
			return this.SetTransactionID((tid == null) ? null : new DerOctetString(tid));
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x00195960 File Offset: 0x00193B60
		public virtual PkiHeaderBuilder SetTransactionID(Asn1OctetString tid)
		{
			this.transactionID = tid;
			return this;
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x0019596A File Offset: 0x00193B6A
		public virtual PkiHeaderBuilder SetSenderNonce(byte[] nonce)
		{
			return this.SetSenderNonce((nonce == null) ? null : new DerOctetString(nonce));
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x0019597E File Offset: 0x00193B7E
		public virtual PkiHeaderBuilder SetSenderNonce(Asn1OctetString nonce)
		{
			this.senderNonce = nonce;
			return this;
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x00195988 File Offset: 0x00193B88
		public virtual PkiHeaderBuilder SetRecipNonce(byte[] nonce)
		{
			return this.SetRecipNonce((nonce == null) ? null : new DerOctetString(nonce));
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x0019599C File Offset: 0x00193B9C
		public virtual PkiHeaderBuilder SetRecipNonce(Asn1OctetString nonce)
		{
			this.recipNonce = nonce;
			return this;
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x001959A6 File Offset: 0x00193BA6
		public virtual PkiHeaderBuilder SetFreeText(PkiFreeText text)
		{
			this.freeText = text;
			return this;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x001959B0 File Offset: 0x00193BB0
		public virtual PkiHeaderBuilder SetGeneralInfo(InfoTypeAndValue genInfo)
		{
			return this.SetGeneralInfo(PkiHeaderBuilder.MakeGeneralInfoSeq(genInfo));
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x001959BE File Offset: 0x00193BBE
		public virtual PkiHeaderBuilder SetGeneralInfo(InfoTypeAndValue[] genInfos)
		{
			return this.SetGeneralInfo(PkiHeaderBuilder.MakeGeneralInfoSeq(genInfos));
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x001959CC File Offset: 0x00193BCC
		public virtual PkiHeaderBuilder SetGeneralInfo(Asn1Sequence seqOfInfoTypeAndValue)
		{
			this.generalInfo = seqOfInfoTypeAndValue;
			return this;
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x001959D6 File Offset: 0x00193BD6
		private static Asn1Sequence MakeGeneralInfoSeq(InfoTypeAndValue generalInfo)
		{
			return new DerSequence(generalInfo);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x001959E0 File Offset: 0x00193BE0
		private static Asn1Sequence MakeGeneralInfoSeq(InfoTypeAndValue[] generalInfos)
		{
			Asn1Sequence result = null;
			if (generalInfos != null)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
				for (int i = 0; i < generalInfos.Length; i++)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						generalInfos[i]
					});
				}
				result = new DerSequence(asn1EncodableVector);
			}
			return result;
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x00195A28 File Offset: 0x00193C28
		public virtual PkiHeader Build()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pvno,
				this.sender,
				this.recipient
			});
			this.AddOptional(v, 0, this.messageTime);
			this.AddOptional(v, 1, this.protectionAlg);
			this.AddOptional(v, 2, this.senderKID);
			this.AddOptional(v, 3, this.recipKID);
			this.AddOptional(v, 4, this.transactionID);
			this.AddOptional(v, 5, this.senderNonce);
			this.AddOptional(v, 6, this.recipNonce);
			this.AddOptional(v, 7, this.freeText);
			this.AddOptional(v, 8, this.generalInfo);
			this.messageTime = null;
			this.protectionAlg = null;
			this.senderKID = null;
			this.recipKID = null;
			this.transactionID = null;
			this.senderNonce = null;
			this.recipNonce = null;
			this.freeText = null;
			this.generalInfo = null;
			return PkiHeader.GetInstance(new DerSequence(v));
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x00194FBE File Offset: 0x001931BE
		private void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, tagNo, obj)
				});
			}
		}

		// Token: 0x04002CF0 RID: 11504
		private DerInteger pvno;

		// Token: 0x04002CF1 RID: 11505
		private GeneralName sender;

		// Token: 0x04002CF2 RID: 11506
		private GeneralName recipient;

		// Token: 0x04002CF3 RID: 11507
		private DerGeneralizedTime messageTime;

		// Token: 0x04002CF4 RID: 11508
		private AlgorithmIdentifier protectionAlg;

		// Token: 0x04002CF5 RID: 11509
		private Asn1OctetString senderKID;

		// Token: 0x04002CF6 RID: 11510
		private Asn1OctetString recipKID;

		// Token: 0x04002CF7 RID: 11511
		private Asn1OctetString transactionID;

		// Token: 0x04002CF8 RID: 11512
		private Asn1OctetString senderNonce;

		// Token: 0x04002CF9 RID: 11513
		private Asn1OctetString recipNonce;

		// Token: 0x04002CFA RID: 11514
		private PkiFreeText freeText;

		// Token: 0x04002CFB RID: 11515
		private Asn1Sequence generalInfo;
	}
}
