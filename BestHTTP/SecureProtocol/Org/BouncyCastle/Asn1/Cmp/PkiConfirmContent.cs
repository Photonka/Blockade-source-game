using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A7 RID: 1959
	public class PkiConfirmContent : Asn1Encodable
	{
		// Token: 0x06004623 RID: 17955 RVA: 0x0019541B File Offset: 0x0019361B
		public static PkiConfirmContent GetInstance(object obj)
		{
			if (obj is PkiConfirmContent)
			{
				return (PkiConfirmContent)obj;
			}
			if (obj is Asn1Null)
			{
				return new PkiConfirmContent();
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00195454 File Offset: 0x00193654
		public override Asn1Object ToAsn1Object()
		{
			return DerNull.Instance;
		}
	}
}
