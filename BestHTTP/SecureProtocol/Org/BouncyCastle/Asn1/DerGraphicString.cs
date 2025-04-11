using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200063C RID: 1596
	public class DerGraphicString : DerStringBase
	{
		// Token: 0x06003C14 RID: 15380 RVA: 0x00173348 File Offset: 0x00171548
		public static DerGraphicString GetInstance(object obj)
		{
			if (obj == null || obj is DerGraphicString)
			{
				return (DerGraphicString)obj;
			}
			if (obj is byte[])
			{
				try
				{
					return (DerGraphicString)Asn1Object.FromByteArray((byte[])obj);
				}
				catch (Exception ex)
				{
					throw new ArgumentException("encoding error in GetInstance: " + ex.ToString(), "obj");
				}
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x001733CC File Offset: 0x001715CC
		public static DerGraphicString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerGraphicString)
			{
				return DerGraphicString.GetInstance(@object);
			}
			return new DerGraphicString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00173402 File Offset: 0x00171602
		public DerGraphicString(byte[] encoding)
		{
			this.mString = Arrays.Clone(encoding);
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x00173416 File Offset: 0x00171616
		public override string GetString()
		{
			return Strings.FromByteArray(this.mString);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x00173423 File Offset: 0x00171623
		public byte[] GetOctets()
		{
			return Arrays.Clone(this.mString);
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00173430 File Offset: 0x00171630
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(25, this.mString);
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00173440 File Offset: 0x00171640
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.mString);
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x00173450 File Offset: 0x00171650
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGraphicString derGraphicString = asn1Object as DerGraphicString;
			return derGraphicString != null && Arrays.AreEqual(this.mString, derGraphicString.mString);
		}

		// Token: 0x040025B1 RID: 9649
		private readonly byte[] mString;
	}
}
