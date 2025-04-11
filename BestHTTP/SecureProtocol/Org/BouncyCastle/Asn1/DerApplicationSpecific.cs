using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000632 RID: 1586
	public class DerApplicationSpecific : Asn1Object
	{
		// Token: 0x06003BA3 RID: 15267 RVA: 0x00171D73 File Offset: 0x0016FF73
		internal DerApplicationSpecific(bool isConstructed, int tag, byte[] octets)
		{
			this.isConstructed = isConstructed;
			this.tag = tag;
			this.octets = octets;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00171D90 File Offset: 0x0016FF90
		public DerApplicationSpecific(int tag, byte[] octets) : this(false, tag, octets)
		{
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00171D9B File Offset: 0x0016FF9B
		public DerApplicationSpecific(int tag, Asn1Encodable obj) : this(true, tag, obj)
		{
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x00171DA8 File Offset: 0x0016FFA8
		public DerApplicationSpecific(bool isExplicit, int tag, Asn1Encodable obj)
		{
			Asn1Object asn1Object = obj.ToAsn1Object();
			byte[] derEncoded = asn1Object.GetDerEncoded();
			this.isConstructed = Asn1TaggedObject.IsConstructed(isExplicit, asn1Object);
			this.tag = tag;
			if (isExplicit)
			{
				this.octets = derEncoded;
				return;
			}
			int lengthOfHeader = this.GetLengthOfHeader(derEncoded);
			byte[] array = new byte[derEncoded.Length - lengthOfHeader];
			Array.Copy(derEncoded, lengthOfHeader, array, 0, array.Length);
			this.octets = array;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x00171E10 File Offset: 0x00170010
		public DerApplicationSpecific(int tagNo, Asn1EncodableVector vec)
		{
			this.tag = tagNo;
			this.isConstructed = true;
			MemoryStream memoryStream = new MemoryStream();
			for (int num = 0; num != vec.Count; num++)
			{
				try
				{
					byte[] derEncoded = vec[num].GetDerEncoded();
					memoryStream.Write(derEncoded, 0, derEncoded.Length);
				}
				catch (IOException innerException)
				{
					throw new InvalidOperationException("malformed object", innerException);
				}
			}
			this.octets = memoryStream.ToArray();
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x00171E8C File Offset: 0x0017008C
		private int GetLengthOfHeader(byte[] data)
		{
			int num = (int)data[1];
			if (num == 128)
			{
				return 2;
			}
			if (num <= 127)
			{
				return 2;
			}
			int num2 = num & 127;
			if (num2 > 4)
			{
				throw new InvalidOperationException("DER length more than 4 bytes: " + num2);
			}
			return num2 + 2;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x00171ED0 File Offset: 0x001700D0
		public bool IsConstructed()
		{
			return this.isConstructed;
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x00171ED8 File Offset: 0x001700D8
		public byte[] GetContents()
		{
			return this.octets;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x00171EE0 File Offset: 0x001700E0
		public int ApplicationTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x00171EE8 File Offset: 0x001700E8
		public Asn1Object GetObject()
		{
			return Asn1Object.FromByteArray(this.GetContents());
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x00171EF8 File Offset: 0x001700F8
		public Asn1Object GetObject(int derTagNo)
		{
			if (derTagNo >= 31)
			{
				throw new IOException("unsupported tag number");
			}
			byte[] encoded = base.GetEncoded();
			byte[] array = this.ReplaceTagNumber(derTagNo, encoded);
			if ((encoded[0] & 32) != 0)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] |= 32;
			}
			return Asn1Object.FromByteArray(array);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x00171F44 File Offset: 0x00170144
		internal override void Encode(DerOutputStream derOut)
		{
			int num = 64;
			if (this.isConstructed)
			{
				num |= 32;
			}
			derOut.WriteEncoded(num, this.tag, this.octets);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x00171F74 File Offset: 0x00170174
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerApplicationSpecific derApplicationSpecific = asn1Object as DerApplicationSpecific;
			return derApplicationSpecific != null && (this.isConstructed == derApplicationSpecific.isConstructed && this.tag == derApplicationSpecific.tag) && Arrays.AreEqual(this.octets, derApplicationSpecific.octets);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x00171FBC File Offset: 0x001701BC
		protected override int Asn1GetHashCode()
		{
			return this.isConstructed.GetHashCode() ^ this.tag.GetHashCode() ^ Arrays.GetHashCode(this.octets);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x00171FF4 File Offset: 0x001701F4
		private byte[] ReplaceTagNumber(int newTag, byte[] input)
		{
			int num = (int)(input[0] & 31);
			int num2 = 1;
			if (num == 31)
			{
				int num3 = (int)input[num2++];
				if ((num3 & 127) == 0)
				{
					throw new IOException("corrupted stream - invalid high tag number found");
				}
				while ((num3 & 128) != 0)
				{
					num3 = (int)input[num2++];
				}
			}
			int num4 = input.Length - num2;
			byte[] array = new byte[1 + num4];
			array[0] = (byte)newTag;
			Array.Copy(input, num2, array, 1, num4);
			return array;
		}

		// Token: 0x0400259A RID: 9626
		private readonly bool isConstructed;

		// Token: 0x0400259B RID: 9627
		private readonly int tag;

		// Token: 0x0400259C RID: 9628
		private readonly byte[] octets;
	}
}
