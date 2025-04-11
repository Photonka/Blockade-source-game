using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000633 RID: 1587
	public class DerBitString : DerStringBase
	{
		// Token: 0x06003BB2 RID: 15282 RVA: 0x00172058 File Offset: 0x00170258
		public static DerBitString GetInstance(object obj)
		{
			if (obj == null || obj is DerBitString)
			{
				return (DerBitString)obj;
			}
			if (obj is byte[])
			{
				try
				{
					return (DerBitString)Asn1Object.FromByteArray((byte[])obj);
				}
				catch (Exception ex)
				{
					throw new ArgumentException("encoding error in GetInstance: " + ex.ToString());
				}
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x001720D0 File Offset: 0x001702D0
		public static DerBitString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerBitString)
			{
				return DerBitString.GetInstance(@object);
			}
			return DerBitString.FromAsn1Octets(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x00172108 File Offset: 0x00170308
		public DerBitString(byte[] data, int padBits)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padBits < 0 || padBits > 7)
			{
				throw new ArgumentException("must be in the range 0 to 7", "padBits");
			}
			if (data.Length == 0 && padBits != 0)
			{
				throw new ArgumentException("if 'data' is empty, 'padBits' must be 0");
			}
			this.mData = Arrays.Clone(data);
			this.mPadBits = padBits;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x00172166 File Offset: 0x00170366
		public DerBitString(byte[] data) : this(data, 0)
		{
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x00172170 File Offset: 0x00170370
		public DerBitString(int namedBits)
		{
			if (namedBits == 0)
			{
				this.mData = new byte[0];
				this.mPadBits = 0;
				return;
			}
			int num = (BigInteger.BitLen(namedBits) + 7) / 8;
			byte[] array = new byte[num];
			num--;
			for (int i = 0; i < num; i++)
			{
				array[i] = (byte)namedBits;
				namedBits >>= 8;
			}
			array[num] = (byte)namedBits;
			int num2 = 0;
			while ((namedBits & 1 << num2) == 0)
			{
				num2++;
			}
			this.mData = array;
			this.mPadBits = num2;
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x001721EB File Offset: 0x001703EB
		public DerBitString(Asn1Encodable obj) : this(obj.GetDerEncoded())
		{
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x001721F9 File Offset: 0x001703F9
		public virtual byte[] GetOctets()
		{
			if (this.mPadBits != 0)
			{
				throw new InvalidOperationException("attempt to get non-octet aligned data from BIT STRING");
			}
			return Arrays.Clone(this.mData);
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x0017221C File Offset: 0x0017041C
		public virtual byte[] GetBytes()
		{
			byte[] array = Arrays.Clone(this.mData);
			if (this.mPadBits > 0)
			{
				byte[] array2 = array;
				int num = array.Length - 1;
				array2[num] &= (byte)(255 << this.mPadBits);
			}
			return array;
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x0017225F File Offset: 0x0017045F
		public virtual int PadBits
		{
			get
			{
				return this.mPadBits;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x00172268 File Offset: 0x00170468
		public virtual int IntValue
		{
			get
			{
				int num = 0;
				int num2 = Math.Min(4, this.mData.Length);
				for (int i = 0; i < num2; i++)
				{
					num |= (int)this.mData[i] << 8 * i;
				}
				if (this.mPadBits > 0 && num2 == this.mData.Length)
				{
					int num3 = (1 << this.mPadBits) - 1;
					num &= ~(num3 << 8 * (num2 - 1));
				}
				return num;
			}
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x001722D8 File Offset: 0x001704D8
		internal override void Encode(DerOutputStream derOut)
		{
			if (this.mPadBits > 0)
			{
				int num = (int)this.mData[this.mData.Length - 1];
				int num2 = (1 << this.mPadBits) - 1;
				int num3 = num & num2;
				if (num3 != 0)
				{
					byte[] array = Arrays.Prepend(this.mData, (byte)this.mPadBits);
					array[array.Length - 1] = (byte)(num ^ num3);
					derOut.WriteEncoded(3, array);
					return;
				}
			}
			derOut.WriteEncoded(3, (byte)this.mPadBits, this.mData);
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00172350 File Offset: 0x00170550
		protected override int Asn1GetHashCode()
		{
			return this.mPadBits.GetHashCode() ^ Arrays.GetHashCode(this.mData);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x00172378 File Offset: 0x00170578
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBitString derBitString = asn1Object as DerBitString;
			return derBitString != null && this.mPadBits == derBitString.mPadBits && Arrays.AreEqual(this.mData, derBitString.mData);
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x001723B4 File Offset: 0x001705B4
		public override string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder("#");
			byte[] derEncoded = base.GetDerEncoded();
			for (int num = 0; num != derEncoded.Length; num++)
			{
				uint num2 = (uint)derEncoded[num];
				stringBuilder.Append(DerBitString.table[(int)(num2 >> 4 & 15U)]);
				stringBuilder.Append(DerBitString.table[(int)(derEncoded[num] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x00172414 File Offset: 0x00170614
		internal static DerBitString FromAsn1Octets(byte[] octets)
		{
			if (octets.Length < 1)
			{
				throw new ArgumentException("truncated BIT STRING detected", "octets");
			}
			int num = (int)octets[0];
			byte[] array = Arrays.CopyOfRange(octets, 1, octets.Length);
			if (num > 0 && num < 8 && array.Length != 0)
			{
				bool flag = array[array.Length - 1] != 0;
				int num2 = (1 << num) - 1;
				if (((flag ? 1 : 0) & num2) != 0)
				{
					return new BerBitString(array, num);
				}
			}
			return new DerBitString(array, num);
		}

		// Token: 0x0400259D RID: 9629
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

		// Token: 0x0400259E RID: 9630
		protected readonly byte[] mData;

		// Token: 0x0400259F RID: 9631
		protected readonly int mPadBits;
	}
}
