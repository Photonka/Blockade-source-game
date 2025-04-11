using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200077D RID: 1917
	public class MetaData : Asn1Encodable
	{
		// Token: 0x060044EE RID: 17646 RVA: 0x00191D35 File Offset: 0x0018FF35
		public MetaData(DerBoolean hashProtected, DerUtf8String fileName, DerIA5String mediaType, Attributes otherMetaData)
		{
			this.hashProtected = hashProtected;
			this.fileName = fileName;
			this.mediaType = mediaType;
			this.otherMetaData = otherMetaData;
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x00191D5C File Offset: 0x0018FF5C
		private MetaData(Asn1Sequence seq)
		{
			this.hashProtected = DerBoolean.GetInstance(seq[0]);
			int num = 1;
			if (num < seq.Count && seq[num] is DerUtf8String)
			{
				this.fileName = DerUtf8String.GetInstance(seq[num++]);
			}
			if (num < seq.Count && seq[num] is DerIA5String)
			{
				this.mediaType = DerIA5String.GetInstance(seq[num++]);
			}
			if (num < seq.Count)
			{
				this.otherMetaData = Attributes.GetInstance(seq[num++]);
			}
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x00191DFC File Offset: 0x0018FFFC
		public static MetaData GetInstance(object obj)
		{
			if (obj is MetaData)
			{
				return (MetaData)obj;
			}
			if (obj != null)
			{
				return new MetaData(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x00191E20 File Offset: 0x00190020
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.hashProtected
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.fileName,
				this.mediaType,
				this.otherMetaData
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x00191E6F File Offset: 0x0019006F
		public virtual bool IsHashProtected
		{
			get
			{
				return this.hashProtected.IsTrue;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x00191E7C File Offset: 0x0019007C
		public virtual DerUtf8String FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x00191E84 File Offset: 0x00190084
		public virtual DerIA5String MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060044F5 RID: 17653 RVA: 0x00191E8C File Offset: 0x0019008C
		public virtual Attributes OtherMetaData
		{
			get
			{
				return this.otherMetaData;
			}
		}

		// Token: 0x04002C1E RID: 11294
		private DerBoolean hashProtected;

		// Token: 0x04002C1F RID: 11295
		private DerUtf8String fileName;

		// Token: 0x04002C20 RID: 11296
		private DerIA5String mediaType;

		// Token: 0x04002C21 RID: 11297
		private Attributes otherMetaData;
	}
}
