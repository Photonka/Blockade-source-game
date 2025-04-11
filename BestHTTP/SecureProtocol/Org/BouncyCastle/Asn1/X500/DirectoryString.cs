using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500
{
	// Token: 0x020006BF RID: 1727
	public class DirectoryString : Asn1Encodable, IAsn1Choice, IAsn1String
	{
		// Token: 0x06003FF2 RID: 16370 RVA: 0x0017FBAC File Offset: 0x0017DDAC
		public static DirectoryString GetInstance(object obj)
		{
			if (obj == null || obj is DirectoryString)
			{
				return (DirectoryString)obj;
			}
			if (obj is DerStringBase && (obj is DerT61String || obj is DerPrintableString || obj is DerUniversalString || obj is DerUtf8String || obj is DerBmpString))
			{
				return new DirectoryString((DerStringBase)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x0017FC21 File Offset: 0x0017DE21
		public static DirectoryString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (!isExplicit)
			{
				throw new ArgumentException("choice item must be explicitly tagged");
			}
			return DirectoryString.GetInstance(obj.GetObject());
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x0017FC3C File Offset: 0x0017DE3C
		private DirectoryString(DerStringBase str)
		{
			this.str = str;
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x0017FC4B File Offset: 0x0017DE4B
		public DirectoryString(string str)
		{
			this.str = new DerUtf8String(str);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x0017FC5F File Offset: 0x0017DE5F
		public string GetString()
		{
			return this.str.GetString();
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x0017FC6C File Offset: 0x0017DE6C
		public override Asn1Object ToAsn1Object()
		{
			return this.str.ToAsn1Object();
		}

		// Token: 0x040027BC RID: 10172
		private readonly DerStringBase str;
	}
}
