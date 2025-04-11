using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000613 RID: 1555
	public abstract class Asn1OctetString : Asn1Object, Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06003B00 RID: 15104 RVA: 0x00170198 File Offset: 0x0016E398
		public static Asn1OctetString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is Asn1OctetString)
			{
				return Asn1OctetString.GetInstance(@object);
			}
			return BerOctetString.FromSequence(Asn1Sequence.GetInstance(@object));
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x001701CC File Offset: 0x0016E3CC
		public static Asn1OctetString GetInstance(object obj)
		{
			if (obj == null || obj is Asn1OctetString)
			{
				return (Asn1OctetString)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return Asn1OctetString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00170219 File Offset: 0x0016E419
		internal Asn1OctetString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00170238 File Offset: 0x0016E438
		internal Asn1OctetString(Asn1Encodable obj)
		{
			try
			{
				this.str = obj.GetEncoded("DER");
			}
			catch (IOException ex)
			{
				throw new ArgumentException("Error processing object : " + ex.ToString());
			}
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00170288 File Offset: 0x0016E488
		public Stream GetOctetStream()
		{
			return new MemoryStream(this.str, false);
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public Asn1OctetStringParser Parser
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00170296 File Offset: 0x0016E496
		public virtual byte[] GetOctets()
		{
			return this.str;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x0017029E File Offset: 0x0016E49E
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.GetOctets());
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x001702AC File Offset: 0x0016E4AC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerOctetString derOctetString = asn1Object as DerOctetString;
			return derOctetString != null && Arrays.AreEqual(this.GetOctets(), derOctetString.GetOctets());
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x001702D6 File Offset: 0x0016E4D6
		public override string ToString()
		{
			return "#" + Hex.ToHexString(this.str);
		}

		// Token: 0x0400255F RID: 9567
		internal byte[] str;
	}
}
