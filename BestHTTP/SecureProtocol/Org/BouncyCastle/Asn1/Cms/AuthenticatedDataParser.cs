﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000768 RID: 1896
	public class AuthenticatedDataParser
	{
		// Token: 0x0600445B RID: 17499 RVA: 0x001903B0 File Offset: 0x0018E5B0
		public AuthenticatedDataParser(Asn1SequenceParser seq)
		{
			this.seq = seq;
			this.version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x001903D0 File Offset: 0x0018E5D0
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x001903D8 File Offset: 0x0018E5D8
		public OriginatorInfo GetOriginatorInfo()
		{
			this.originatorInfoCalled = true;
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this.nextObject).TagNo == 0)
			{
				IAsn1Convertible asn1Convertible = (Asn1SequenceParser)((Asn1TaggedObjectParser)this.nextObject).GetObjectParser(16, false);
				this.nextObject = null;
				return OriginatorInfo.GetInstance(asn1Convertible.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x0019044F File Offset: 0x0018E64F
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this.originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this.nextObject;
			this.nextObject = null;
			return result;
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x0019048C File Offset: 0x0018E68C
		public AlgorithmIdentifier GetMacAlgorithm()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				IAsn1Convertible asn1Convertible = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return AlgorithmIdentifier.GetInstance(asn1Convertible.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x001904D8 File Offset: 0x0018E6D8
		public AlgorithmIdentifier GetDigestAlgorithm()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser)
			{
				AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)this.nextObject.ToAsn1Object(), false);
				this.nextObject = null;
				return instance;
			}
			return null;
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x0019052A File Offset: 0x0018E72A
		public ContentInfoParser GetEnapsulatedContentInfo()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return new ContentInfoParser(asn1SequenceParser);
			}
			return null;
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x00190568 File Offset: 0x0018E768
		public Asn1SetParser GetAuthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x001905BC File Offset: 0x0018E7BC
		public Asn1OctetString GetMac()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			IAsn1Convertible asn1Convertible = this.nextObject;
			this.nextObject = null;
			return Asn1OctetString.GetInstance(asn1Convertible.ToAsn1Object());
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x001905F0 File Offset: 0x0018E7F0
		public Asn1SetParser GetUnauthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x04002BCC RID: 11212
		private Asn1SequenceParser seq;

		// Token: 0x04002BCD RID: 11213
		private DerInteger version;

		// Token: 0x04002BCE RID: 11214
		private IAsn1Convertible nextObject;

		// Token: 0x04002BCF RID: 11215
		private bool originatorInfoCalled;
	}
}
