using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200070F RID: 1807
	public class AdditionalInformationSyntax : Asn1Encodable
	{
		// Token: 0x060041FC RID: 16892 RVA: 0x00187A9D File Offset: 0x00185C9D
		public static AdditionalInformationSyntax GetInstance(object obj)
		{
			if (obj is AdditionalInformationSyntax)
			{
				return (AdditionalInformationSyntax)obj;
			}
			if (obj is IAsn1String)
			{
				return new AdditionalInformationSyntax(DirectoryString.GetInstance(obj));
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x00187ADC File Offset: 0x00185CDC
		private AdditionalInformationSyntax(DirectoryString information)
		{
			this.information = information;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x00187AEB File Offset: 0x00185CEB
		public AdditionalInformationSyntax(string information)
		{
			this.information = new DirectoryString(information);
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x00187AFF File Offset: 0x00185CFF
		public virtual DirectoryString Information
		{
			get
			{
				return this.information;
			}
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00187B07 File Offset: 0x00185D07
		public override Asn1Object ToAsn1Object()
		{
			return this.information.ToAsn1Object();
		}

		// Token: 0x04002A27 RID: 10791
		private readonly DirectoryString information;
	}
}
