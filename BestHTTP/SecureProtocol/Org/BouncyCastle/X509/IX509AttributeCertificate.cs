using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000224 RID: 548
	public interface IX509AttributeCertificate : IX509Extension
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001423 RID: 5155
		int Version { get; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001424 RID: 5156
		BigInteger SerialNumber { get; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001425 RID: 5157
		DateTime NotBefore { get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001426 RID: 5158
		DateTime NotAfter { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001427 RID: 5159
		AttributeCertificateHolder Holder { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001428 RID: 5160
		AttributeCertificateIssuer Issuer { get; }

		// Token: 0x06001429 RID: 5161
		X509Attribute[] GetAttributes();

		// Token: 0x0600142A RID: 5162
		X509Attribute[] GetAttributes(string oid);

		// Token: 0x0600142B RID: 5163
		bool[] GetIssuerUniqueID();

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600142C RID: 5164
		bool IsValidNow { get; }

		// Token: 0x0600142D RID: 5165
		bool IsValid(DateTime date);

		// Token: 0x0600142E RID: 5166
		void CheckValidity();

		// Token: 0x0600142F RID: 5167
		void CheckValidity(DateTime date);

		// Token: 0x06001430 RID: 5168
		byte[] GetSignature();

		// Token: 0x06001431 RID: 5169
		void Verify(AsymmetricKeyParameter publicKey);

		// Token: 0x06001432 RID: 5170
		byte[] GetEncoded();
	}
}
