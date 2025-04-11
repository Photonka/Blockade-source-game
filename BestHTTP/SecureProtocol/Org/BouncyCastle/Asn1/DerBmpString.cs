using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000634 RID: 1588
	public class DerBmpString : DerStringBase
	{
		// Token: 0x06003BC2 RID: 15298 RVA: 0x0017248E File Offset: 0x0017068E
		public static DerBmpString GetInstance(object obj)
		{
			if (obj == null || obj is DerBmpString)
			{
				return (DerBmpString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x001724B8 File Offset: 0x001706B8
		public static DerBmpString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerBmpString)
			{
				return DerBmpString.GetInstance(@object);
			}
			return new DerBmpString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x001724F0 File Offset: 0x001706F0
		public DerBmpString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] array = new char[str.Length / 2];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = (char)((int)str[2 * num] << 8 | (int)(str[2 * num + 1] & byte.MaxValue));
			}
			this.str = new string(array);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x0017254F File Offset: 0x0017074F
		public DerBmpString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x0017256C File Offset: 0x0017076C
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00172574 File Offset: 0x00170774
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBmpString derBmpString = asn1Object as DerBmpString;
			return derBmpString != null && this.str.Equals(derBmpString.str);
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x001725A0 File Offset: 0x001707A0
		internal override void Encode(DerOutputStream derOut)
		{
			char[] array = this.str.ToCharArray();
			byte[] array2 = new byte[array.Length * 2];
			for (int num = 0; num != array.Length; num++)
			{
				array2[2 * num] = (byte)(array[num] >> 8);
				array2[2 * num + 1] = (byte)array[num];
			}
			derOut.WriteEncoded(30, array2);
		}

		// Token: 0x040025A0 RID: 9632
		private readonly string str;
	}
}
