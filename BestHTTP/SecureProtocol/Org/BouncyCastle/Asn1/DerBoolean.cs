using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000635 RID: 1589
	public class DerBoolean : Asn1Object
	{
		// Token: 0x06003BC9 RID: 15305 RVA: 0x001725F1 File Offset: 0x001707F1
		public static DerBoolean GetInstance(object obj)
		{
			if (obj == null || obj is DerBoolean)
			{
				return (DerBoolean)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x0017261A File Offset: 0x0017081A
		public static DerBoolean GetInstance(bool value)
		{
			if (!value)
			{
				return DerBoolean.False;
			}
			return DerBoolean.True;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x0017262C File Offset: 0x0017082C
		public static DerBoolean GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerBoolean)
			{
				return DerBoolean.GetInstance(@object);
			}
			return DerBoolean.FromOctetString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x00172662 File Offset: 0x00170862
		public DerBoolean(byte[] val)
		{
			if (val.Length != 1)
			{
				throw new ArgumentException("byte value should have 1 byte in it", "val");
			}
			this.value = val[0];
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00172689 File Offset: 0x00170889
		private DerBoolean(bool value)
		{
			this.value = (value ? byte.MaxValue : 0);
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x001726A2 File Offset: 0x001708A2
		public bool IsTrue
		{
			get
			{
				return this.value > 0;
			}
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x001726AD File Offset: 0x001708AD
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(1, new byte[]
			{
				this.value
			});
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x001726C8 File Offset: 0x001708C8
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBoolean derBoolean = asn1Object as DerBoolean;
			return derBoolean != null && this.IsTrue == derBoolean.IsTrue;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x001726F0 File Offset: 0x001708F0
		protected override int Asn1GetHashCode()
		{
			return this.IsTrue.GetHashCode();
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x0017270B File Offset: 0x0017090B
		public override string ToString()
		{
			if (!this.IsTrue)
			{
				return "FALSE";
			}
			return "TRUE";
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00172720 File Offset: 0x00170920
		internal static DerBoolean FromOctetString(byte[] value)
		{
			if (value.Length != 1)
			{
				throw new ArgumentException("BOOLEAN value should have 1 byte in it", "value");
			}
			byte b = value[0];
			if (b == 0)
			{
				return DerBoolean.False;
			}
			if (b != 255)
			{
				return new DerBoolean(value);
			}
			return DerBoolean.True;
		}

		// Token: 0x040025A1 RID: 9633
		private readonly byte value;

		// Token: 0x040025A2 RID: 9634
		public static readonly DerBoolean False = new DerBoolean(false);

		// Token: 0x040025A3 RID: 9635
		public static readonly DerBoolean True = new DerBoolean(true);
	}
}
