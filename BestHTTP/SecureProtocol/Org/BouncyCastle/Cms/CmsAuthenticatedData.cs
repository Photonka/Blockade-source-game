using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005CC RID: 1484
	public class CmsAuthenticatedData
	{
		// Token: 0x0600390C RID: 14604 RVA: 0x00168631 File Offset: 0x00166831
		public CmsAuthenticatedData(byte[] authData) : this(CmsUtilities.ReadContentInfo(authData))
		{
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x0016863F File Offset: 0x0016683F
		public CmsAuthenticatedData(Stream authData) : this(CmsUtilities.ReadContentInfo(authData))
		{
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x00168650 File Offset: 0x00166850
		public CmsAuthenticatedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			AuthenticatedData instance = AuthenticatedData.GetInstance(contentInfo.Content);
			Asn1Set recipientInfos = instance.RecipientInfos;
			this.macAlg = instance.MacAlgorithm;
			CmsReadable readable = new CmsProcessableByteArray(Asn1OctetString.GetInstance(instance.EncapsulatedContentInfo.Content).GetOctets());
			CmsSecureReadable secureReadable = new CmsEnvelopedHelper.CmsAuthenticatedSecureReadable(this.macAlg, readable);
			this.recipientInfoStore = CmsEnvelopedHelper.BuildRecipientInformationStore(recipientInfos, secureReadable);
			this.authAttrs = instance.AuthAttrs;
			this.mac = instance.Mac.GetOctets();
			this.unauthAttrs = instance.UnauthAttrs;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x001686E7 File Offset: 0x001668E7
		public byte[] GetMac()
		{
			return Arrays.Clone(this.mac);
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x001686F4 File Offset: 0x001668F4
		public AlgorithmIdentifier MacAlgorithmID
		{
			get
			{
				return this.macAlg;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x001686FC File Offset: 0x001668FC
		public string MacAlgOid
		{
			get
			{
				return this.macAlg.Algorithm.Id;
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x0016870E File Offset: 0x0016690E
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x00168716 File Offset: 0x00166916
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x0016871E File Offset: 0x0016691E
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable GetAuthAttrs()
		{
			if (this.authAttrs == null)
			{
				return null;
			}
			return new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(this.authAttrs);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x00168735 File Offset: 0x00166935
		public BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnauthAttrs()
		{
			if (this.unauthAttrs == null)
			{
				return null;
			}
			return new BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unauthAttrs);
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x0016874C File Offset: 0x0016694C
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x0400247C RID: 9340
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x0400247D RID: 9341
		internal ContentInfo contentInfo;

		// Token: 0x0400247E RID: 9342
		private AlgorithmIdentifier macAlg;

		// Token: 0x0400247F RID: 9343
		private Asn1Set authAttrs;

		// Token: 0x04002480 RID: 9344
		private Asn1Set unauthAttrs;

		// Token: 0x04002481 RID: 9345
		private byte[] mac;
	}
}
