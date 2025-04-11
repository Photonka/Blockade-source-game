using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000644 RID: 1604
	public class DerOutputStream : FilterStream
	{
		// Token: 0x06003C5F RID: 15455 RVA: 0x00173F24 File Offset: 0x00172124
		public DerOutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x00173F30 File Offset: 0x00172130
		private void WriteLength(int length)
		{
			if (length > 127)
			{
				int num = 1;
				uint num2 = (uint)length;
				while ((num2 >>= 8) != 0U)
				{
					num++;
				}
				this.WriteByte((byte)(num | 128));
				for (int i = (num - 1) * 8; i >= 0; i -= 8)
				{
					this.WriteByte((byte)(length >> i));
				}
				return;
			}
			this.WriteByte((byte)length);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x00173F87 File Offset: 0x00172187
		internal void WriteEncoded(int tag, byte[] bytes)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(bytes.Length);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x00173FA5 File Offset: 0x001721A5
		internal void WriteEncoded(int tag, byte first, byte[] bytes)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(bytes.Length + 1);
			this.WriteByte(first);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00173FCC File Offset: 0x001721CC
		internal void WriteEncoded(int tag, byte[] bytes, int offset, int length)
		{
			this.WriteByte((byte)tag);
			this.WriteLength(length);
			this.Write(bytes, offset, length);
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x00173FE8 File Offset: 0x001721E8
		internal void WriteTag(int flags, int tagNo)
		{
			if (tagNo < 31)
			{
				this.WriteByte((byte)(flags | tagNo));
				return;
			}
			this.WriteByte((byte)(flags | 31));
			if (tagNo < 128)
			{
				this.WriteByte((byte)tagNo);
				return;
			}
			byte[] array = new byte[5];
			int num = array.Length;
			array[--num] = (byte)(tagNo & 127);
			do
			{
				tagNo >>= 7;
				array[--num] = (byte)((tagNo & 127) | 128);
			}
			while (tagNo > 127);
			this.Write(array, num, array.Length - num);
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x00174061 File Offset: 0x00172261
		internal void WriteEncoded(int flags, int tagNo, byte[] bytes)
		{
			this.WriteTag(flags, tagNo);
			this.WriteLength(bytes.Length);
			this.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x0017407F File Offset: 0x0017227F
		protected void WriteNull()
		{
			this.WriteByte(5);
			this.WriteByte(0);
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00174090 File Offset: 0x00172290
		[Obsolete("Use version taking an Asn1Encodable arg instead")]
		public virtual void WriteObject(object obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			if (obj is Asn1Object)
			{
				((Asn1Object)obj).Encode(this);
				return;
			}
			if (obj is Asn1Encodable)
			{
				((Asn1Encodable)obj).ToAsn1Object().Encode(this);
				return;
			}
			throw new IOException("object not Asn1Object");
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x001740E0 File Offset: 0x001722E0
		public virtual void WriteObject(Asn1Encodable obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			obj.ToAsn1Object().Encode(this);
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x001740F8 File Offset: 0x001722F8
		public virtual void WriteObject(Asn1Object obj)
		{
			if (obj == null)
			{
				this.WriteNull();
				return;
			}
			obj.Encode(this);
		}
	}
}
