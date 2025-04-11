using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000636 RID: 1590
	public class DerEnumerated : Asn1Object
	{
		// Token: 0x06003BD5 RID: 15317 RVA: 0x0017277C File Offset: 0x0017097C
		public static DerEnumerated GetInstance(object obj)
		{
			if (obj == null || obj is DerEnumerated)
			{
				return (DerEnumerated)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x001727A8 File Offset: 0x001709A8
		public static DerEnumerated GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerEnumerated)
			{
				return DerEnumerated.GetInstance(@object);
			}
			return DerEnumerated.FromOctetString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x001727DE File Offset: 0x001709DE
		public DerEnumerated(int val)
		{
			this.bytes = BigInteger.ValueOf((long)val).ToByteArray();
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x001727F8 File Offset: 0x001709F8
		public DerEnumerated(BigInteger val)
		{
			this.bytes = val.ToByteArray();
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x0017280C File Offset: 0x00170A0C
		public DerEnumerated(byte[] bytes)
		{
			if (bytes.Length > 1 && ((bytes[0] == 0 && (bytes[1] & 128) == 0) || (bytes[0] == 255 && (bytes[1] & 128) != 0)) && !DerInteger.AllowUnsafe())
			{
				throw new ArgumentException("malformed enumerated");
			}
			this.bytes = Arrays.Clone(bytes);
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x00172868 File Offset: 0x00170A68
		public BigInteger Value
		{
			get
			{
				return new BigInteger(this.bytes);
			}
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x00172875 File Offset: 0x00170A75
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(10, this.bytes);
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x00172888 File Offset: 0x00170A88
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerEnumerated derEnumerated = asn1Object as DerEnumerated;
			return derEnumerated != null && Arrays.AreEqual(this.bytes, derEnumerated.bytes);
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x001728B2 File Offset: 0x00170AB2
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.bytes);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x001728C0 File Offset: 0x00170AC0
		internal static DerEnumerated FromOctetString(byte[] enc)
		{
			if (enc.Length == 0)
			{
				throw new ArgumentException("ENUMERATED has zero length", "enc");
			}
			if (enc.Length == 1)
			{
				int num = (int)enc[0];
				if (num < DerEnumerated.cache.Length)
				{
					DerEnumerated derEnumerated = DerEnumerated.cache[num];
					if (derEnumerated != null)
					{
						return derEnumerated;
					}
					return DerEnumerated.cache[num] = new DerEnumerated(Arrays.Clone(enc));
				}
			}
			return new DerEnumerated(Arrays.Clone(enc));
		}

		// Token: 0x040025A4 RID: 9636
		private readonly byte[] bytes;

		// Token: 0x040025A5 RID: 9637
		private static readonly DerEnumerated[] cache = new DerEnumerated[12];
	}
}
