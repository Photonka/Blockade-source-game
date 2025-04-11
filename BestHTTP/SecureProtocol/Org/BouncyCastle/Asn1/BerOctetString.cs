using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000624 RID: 1572
	public class BerOctetString : DerOctetString, IEnumerable
	{
		// Token: 0x06003B61 RID: 15201 RVA: 0x00171200 File Offset: 0x0016F400
		public static BerOctetString FromSequence(Asn1Sequence seq)
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in seq)
			{
				Asn1Encodable value = (Asn1Encodable)obj;
				list.Add(value);
			}
			return new BerOctetString(list);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x00171264 File Offset: 0x0016F464
		private static byte[] ToBytes(IEnumerable octs)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (object obj in octs)
			{
				byte[] octets = ((DerOctetString)obj).GetOctets();
				memoryStream.Write(octets, 0, octets.Length);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x001712D0 File Offset: 0x0016F4D0
		public BerOctetString(byte[] str) : base(str)
		{
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x001712D9 File Offset: 0x0016F4D9
		public BerOctetString(IEnumerable octets) : base(BerOctetString.ToBytes(octets))
		{
			this.octs = octets;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x001712EE File Offset: 0x0016F4EE
		public BerOctetString(Asn1Object obj) : base(obj)
		{
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x001712F7 File Offset: 0x0016F4F7
		public BerOctetString(Asn1Encodable obj) : base(obj.ToAsn1Object())
		{
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x00170296 File Offset: 0x0016E496
		public override byte[] GetOctets()
		{
			return this.str;
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x00171305 File Offset: 0x0016F505
		public IEnumerator GetEnumerator()
		{
			if (this.octs == null)
			{
				return this.GenerateOcts().GetEnumerator();
			}
			return this.octs.GetEnumerator();
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x00171326 File Offset: 0x0016F526
		[Obsolete("Use GetEnumerator() instead")]
		public IEnumerator GetObjects()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x00171330 File Offset: 0x0016F530
		private IList GenerateOcts()
		{
			IList list = Platform.CreateArrayList();
			for (int i = 0; i < this.str.Length; i += 1000)
			{
				byte[] array = new byte[Math.Min(this.str.Length, i + 1000) - i];
				Array.Copy(this.str, i, array, 0, array.Length);
				list.Add(new DerOctetString(array));
			}
			return list;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x00171398 File Offset: 0x0016F598
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(36);
				derOut.WriteByte(128);
				foreach (object obj in this)
				{
					DerOctetString obj2 = (DerOctetString)obj;
					derOut.WriteObject(obj2);
				}
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x0400258A RID: 9610
		private const int MaxLength = 1000;

		// Token: 0x0400258B RID: 9611
		private readonly IEnumerable octs;
	}
}
