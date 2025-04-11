using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200065F RID: 1631
	public class DHPublicKey : Asn1Encodable
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x00175598 File Offset: 0x00173798
		public static DHPublicKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return DHPublicKey.GetInstance(DerInteger.GetInstance(obj, isExplicit));
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x001755A8 File Offset: 0x001737A8
		public static DHPublicKey GetInstance(object obj)
		{
			if (obj == null || obj is DHPublicKey)
			{
				return (DHPublicKey)obj;
			}
			if (obj is DerInteger)
			{
				return new DHPublicKey((DerInteger)obj);
			}
			throw new ArgumentException("Invalid DHPublicKey: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x001755F5 File Offset: 0x001737F5
		public DHPublicKey(DerInteger y)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x00175612 File Offset: 0x00173812
		public DerInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x00175612 File Offset: 0x00173812
		public override Asn1Object ToAsn1Object()
		{
			return this.y;
		}

		// Token: 0x040025D8 RID: 9688
		private readonly DerInteger y;
	}
}
