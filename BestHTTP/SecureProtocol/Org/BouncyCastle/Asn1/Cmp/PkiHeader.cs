using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007AA RID: 1962
	public class PkiHeader : Asn1Encodable
	{
		// Token: 0x06004632 RID: 17970 RVA: 0x00195584 File Offset: 0x00193784
		private PkiHeader(Asn1Sequence seq)
		{
			this.pvno = DerInteger.GetInstance(seq[0]);
			this.sender = GeneralName.GetInstance(seq[1]);
			this.recipient = GeneralName.GetInstance(seq[2]);
			for (int i = 3; i < seq.Count; i++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.messageTime = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.protectionAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.senderKID = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 3:
					this.recipKID = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 4:
					this.transactionID = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 5:
					this.senderNonce = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 6:
					this.recipNonce = Asn1OctetString.GetInstance(asn1TaggedObject, true);
					break;
				case 7:
					this.freeText = PkiFreeText.GetInstance(asn1TaggedObject, true);
					break;
				case 8:
					this.generalInfo = Asn1Sequence.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag number: " + asn1TaggedObject.TagNo, "seq");
				}
			}
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x001956D4 File Offset: 0x001938D4
		public static PkiHeader GetInstance(object obj)
		{
			if (obj is PkiHeader)
			{
				return (PkiHeader)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiHeader((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x00195713 File Offset: 0x00193913
		public PkiHeader(int pvno, GeneralName sender, GeneralName recipient) : this(new DerInteger(pvno), sender, recipient)
		{
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x00195723 File Offset: 0x00193923
		private PkiHeader(DerInteger pvno, GeneralName sender, GeneralName recipient)
		{
			this.pvno = pvno;
			this.sender = sender;
			this.recipient = recipient;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x00195740 File Offset: 0x00193940
		public virtual DerInteger Pvno
		{
			get
			{
				return this.pvno;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x00195748 File Offset: 0x00193948
		public virtual GeneralName Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x00195750 File Offset: 0x00193950
		public virtual GeneralName Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x00195758 File Offset: 0x00193958
		public virtual DerGeneralizedTime MessageTime
		{
			get
			{
				return this.messageTime;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x00195760 File Offset: 0x00193960
		public virtual AlgorithmIdentifier ProtectionAlg
		{
			get
			{
				return this.protectionAlg;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x00195768 File Offset: 0x00193968
		public virtual Asn1OctetString SenderKID
		{
			get
			{
				return this.senderKID;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x00195770 File Offset: 0x00193970
		public virtual Asn1OctetString RecipKID
		{
			get
			{
				return this.recipKID;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x00195778 File Offset: 0x00193978
		public virtual Asn1OctetString TransactionID
		{
			get
			{
				return this.transactionID;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x00195780 File Offset: 0x00193980
		public virtual Asn1OctetString SenderNonce
		{
			get
			{
				return this.senderNonce;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x00195788 File Offset: 0x00193988
		public virtual Asn1OctetString RecipNonce
		{
			get
			{
				return this.recipNonce;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x00195790 File Offset: 0x00193990
		public virtual PkiFreeText FreeText
		{
			get
			{
				return this.freeText;
			}
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x00195798 File Offset: 0x00193998
		public virtual InfoTypeAndValue[] GetGeneralInfo()
		{
			if (this.generalInfo == null)
			{
				return null;
			}
			InfoTypeAndValue[] array = new InfoTypeAndValue[this.generalInfo.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = InfoTypeAndValue.GetInstance(this.generalInfo[i]);
			}
			return array;
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x001957E4 File Offset: 0x001939E4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pvno,
				this.sender,
				this.recipient
			});
			PkiHeader.AddOptional(v, 0, this.messageTime);
			PkiHeader.AddOptional(v, 1, this.protectionAlg);
			PkiHeader.AddOptional(v, 2, this.senderKID);
			PkiHeader.AddOptional(v, 3, this.recipKID);
			PkiHeader.AddOptional(v, 4, this.transactionID);
			PkiHeader.AddOptional(v, 5, this.senderNonce);
			PkiHeader.AddOptional(v, 6, this.recipNonce);
			PkiHeader.AddOptional(v, 7, this.freeText);
			PkiHeader.AddOptional(v, 8, this.generalInfo);
			return new DerSequence(v);
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x00195891 File Offset: 0x00193A91
		private static void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, tagNo, obj)
				});
			}
		}

		// Token: 0x04002CE1 RID: 11489
		public static readonly GeneralName NULL_NAME = new GeneralName(X509Name.GetInstance(new DerSequence()));

		// Token: 0x04002CE2 RID: 11490
		public static readonly int CMP_1999 = 1;

		// Token: 0x04002CE3 RID: 11491
		public static readonly int CMP_2000 = 2;

		// Token: 0x04002CE4 RID: 11492
		private readonly DerInteger pvno;

		// Token: 0x04002CE5 RID: 11493
		private readonly GeneralName sender;

		// Token: 0x04002CE6 RID: 11494
		private readonly GeneralName recipient;

		// Token: 0x04002CE7 RID: 11495
		private readonly DerGeneralizedTime messageTime;

		// Token: 0x04002CE8 RID: 11496
		private readonly AlgorithmIdentifier protectionAlg;

		// Token: 0x04002CE9 RID: 11497
		private readonly Asn1OctetString senderKID;

		// Token: 0x04002CEA RID: 11498
		private readonly Asn1OctetString recipKID;

		// Token: 0x04002CEB RID: 11499
		private readonly Asn1OctetString transactionID;

		// Token: 0x04002CEC RID: 11500
		private readonly Asn1OctetString senderNonce;

		// Token: 0x04002CED RID: 11501
		private readonly Asn1OctetString recipNonce;

		// Token: 0x04002CEE RID: 11502
		private readonly PkiFreeText freeText;

		// Token: 0x04002CEF RID: 11503
		private readonly Asn1Sequence generalInfo;
	}
}
