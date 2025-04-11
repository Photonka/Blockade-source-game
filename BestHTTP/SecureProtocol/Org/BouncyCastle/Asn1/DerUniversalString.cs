using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200064F RID: 1615
	public class DerUniversalString : DerStringBase
	{
		// Token: 0x06003CA4 RID: 15524 RVA: 0x00174817 File Offset: 0x00172A17
		public static DerUniversalString GetInstance(object obj)
		{
			if (obj == null || obj is DerUniversalString)
			{
				return (DerUniversalString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x00174840 File Offset: 0x00172A40
		public static DerUniversalString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerUniversalString)
			{
				return DerUniversalString.GetInstance(@object);
			}
			return new DerUniversalString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x00174876 File Offset: 0x00172A76
		public DerUniversalString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00174894 File Offset: 0x00172A94
		public override string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder("#");
			byte[] derEncoded = base.GetDerEncoded();
			for (int num = 0; num != derEncoded.Length; num++)
			{
				uint num2 = (uint)derEncoded[num];
				stringBuilder.Append(DerUniversalString.table[(int)(num2 >> 4 & 15U)]);
				stringBuilder.Append(DerUniversalString.table[(int)(derEncoded[num] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x001748F1 File Offset: 0x00172AF1
		public byte[] GetOctets()
		{
			return (byte[])this.str.Clone();
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x00174903 File Offset: 0x00172B03
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(28, this.str);
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x00174914 File Offset: 0x00172B14
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUniversalString derUniversalString = asn1Object as DerUniversalString;
			return derUniversalString != null && Arrays.AreEqual(this.str, derUniversalString.str);
		}

		// Token: 0x040025C5 RID: 9669
		private static readonly char[] table = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x040025C6 RID: 9670
		private readonly byte[] str;
	}
}
