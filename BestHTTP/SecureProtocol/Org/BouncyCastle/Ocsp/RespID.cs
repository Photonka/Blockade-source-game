using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002EB RID: 747
	public class RespID
	{
		// Token: 0x06001B74 RID: 7028 RVA: 0x000D30E9 File Offset: 0x000D12E9
		public RespID(ResponderID id)
		{
			this.id = id;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000D30F8 File Offset: 0x000D12F8
		public RespID(X509Name name)
		{
			this.id = new ResponderID(name);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000D310C File Offset: 0x000D130C
		public RespID(AsymmetricKeyParameter publicKey)
		{
			try
			{
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
				byte[] str = DigestUtilities.CalculateDigest("SHA1", subjectPublicKeyInfo.PublicKeyData.GetBytes());
				this.id = new ResponderID(new DerOctetString(str));
			}
			catch (Exception ex)
			{
				throw new OcspException("problem creating ID: " + ex, ex);
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000D3174 File Offset: 0x000D1374
		public ResponderID ToAsn1Object()
		{
			return this.id;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000D317C File Offset: 0x000D137C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RespID respID = obj as RespID;
			return respID != null && this.id.Equals(respID.id);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000D31AC File Offset: 0x000D13AC
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x040017D7 RID: 6103
		internal readonly ResponderID id;
	}
}
