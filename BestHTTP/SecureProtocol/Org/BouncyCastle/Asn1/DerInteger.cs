using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200063E RID: 1598
	public class DerInteger : Asn1Object
	{
		// Token: 0x06003C27 RID: 15399 RVA: 0x001735BC File Offset: 0x001717BC
		internal static bool AllowUnsafe()
		{
			string environmentVariable = Platform.GetEnvironmentVariable("BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.AllowUnsafeInteger");
			return environmentVariable != null && Platform.EqualsIgnoreCase("true", environmentVariable);
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x001735E4 File Offset: 0x001717E4
		public static DerInteger GetInstance(object obj)
		{
			if (obj == null || obj is DerInteger)
			{
				return (DerInteger)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x00173610 File Offset: 0x00171810
		public static DerInteger GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerInteger)
			{
				return DerInteger.GetInstance(@object);
			}
			return new DerInteger(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x00173654 File Offset: 0x00171854
		public DerInteger(int value)
		{
			this.bytes = BigInteger.ValueOf((long)value).ToByteArray();
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x0017366E File Offset: 0x0017186E
		public DerInteger(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.bytes = value.ToByteArray();
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x00173690 File Offset: 0x00171890
		public DerInteger(byte[] bytes)
		{
			if (bytes.Length > 1 && ((bytes[0] == 0 && (bytes[1] & 128) == 0) || (bytes[0] == 255 && (bytes[1] & 128) != 0)) && !DerInteger.AllowUnsafe())
			{
				throw new ArgumentException("malformed integer");
			}
			this.bytes = Arrays.Clone(bytes);
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x001736EC File Offset: 0x001718EC
		public BigInteger Value
		{
			get
			{
				return new BigInteger(this.bytes);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x001736F9 File Offset: 0x001718F9
		public BigInteger PositiveValue
		{
			get
			{
				return new BigInteger(1, this.bytes);
			}
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x00173707 File Offset: 0x00171907
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(2, this.bytes);
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x00173716 File Offset: 0x00171916
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.bytes);
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00173724 File Offset: 0x00171924
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerInteger derInteger = asn1Object as DerInteger;
			return derInteger != null && Arrays.AreEqual(this.bytes, derInteger.bytes);
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x0017374E File Offset: 0x0017194E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x040025B3 RID: 9651
		public const string AllowUnsafeProperty = "BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.AllowUnsafeInteger";

		// Token: 0x040025B4 RID: 9652
		private readonly byte[] bytes;
	}
}
