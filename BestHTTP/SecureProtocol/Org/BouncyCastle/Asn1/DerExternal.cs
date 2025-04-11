using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000637 RID: 1591
	public class DerExternal : Asn1Object
	{
		// Token: 0x06003BE0 RID: 15328 RVA: 0x00172934 File Offset: 0x00170B34
		public DerExternal(Asn1EncodableVector vector)
		{
			int num = 0;
			Asn1Object objFromVector = DerExternal.GetObjFromVector(vector, num);
			if (objFromVector is DerObjectIdentifier)
			{
				this.directReference = (DerObjectIdentifier)objFromVector;
				num++;
				objFromVector = DerExternal.GetObjFromVector(vector, num);
			}
			if (objFromVector is DerInteger)
			{
				this.indirectReference = (DerInteger)objFromVector;
				num++;
				objFromVector = DerExternal.GetObjFromVector(vector, num);
			}
			if (!(objFromVector is Asn1TaggedObject))
			{
				this.dataValueDescriptor = objFromVector;
				num++;
				objFromVector = DerExternal.GetObjFromVector(vector, num);
			}
			if (vector.Count != num + 1)
			{
				throw new ArgumentException("input vector too large", "vector");
			}
			if (!(objFromVector is Asn1TaggedObject))
			{
				throw new ArgumentException("No tagged object found in vector. Structure doesn't seem to be of type External", "vector");
			}
			Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)objFromVector;
			this.Encoding = asn1TaggedObject.TagNo;
			if (this.encoding < 0 || this.encoding > 2)
			{
				throw new InvalidOperationException("invalid encoding value");
			}
			this.externalContent = asn1TaggedObject.GetObject();
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00172A1B File Offset: 0x00170C1B
		public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference, Asn1Object dataValueDescriptor, DerTaggedObject externalData) : this(directReference, indirectReference, dataValueDescriptor, externalData.TagNo, externalData.ToAsn1Object())
		{
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x00172A34 File Offset: 0x00170C34
		public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference, Asn1Object dataValueDescriptor, int encoding, Asn1Object externalData)
		{
			this.DirectReference = directReference;
			this.IndirectReference = indirectReference;
			this.DataValueDescriptor = dataValueDescriptor;
			this.Encoding = encoding;
			this.ExternalContent = externalData.ToAsn1Object();
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x00172A68 File Offset: 0x00170C68
		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerExternal.WriteEncodable(memoryStream, this.directReference);
			DerExternal.WriteEncodable(memoryStream, this.indirectReference);
			DerExternal.WriteEncodable(memoryStream, this.dataValueDescriptor);
			DerExternal.WriteEncodable(memoryStream, new DerTaggedObject(8, this.externalContent));
			derOut.WriteEncoded(32, 8, memoryStream.ToArray());
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x00172AC0 File Offset: 0x00170CC0
		protected override int Asn1GetHashCode()
		{
			int num = this.externalContent.GetHashCode();
			if (this.directReference != null)
			{
				num ^= this.directReference.GetHashCode();
			}
			if (this.indirectReference != null)
			{
				num ^= this.indirectReference.GetHashCode();
			}
			if (this.dataValueDescriptor != null)
			{
				num ^= this.dataValueDescriptor.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x00172B1C File Offset: 0x00170D1C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			if (this == asn1Object)
			{
				return true;
			}
			DerExternal derExternal = asn1Object as DerExternal;
			return derExternal != null && (object.Equals(this.directReference, derExternal.directReference) && object.Equals(this.indirectReference, derExternal.indirectReference) && object.Equals(this.dataValueDescriptor, derExternal.dataValueDescriptor)) && this.externalContent.Equals(derExternal.externalContent);
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x00172B87 File Offset: 0x00170D87
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x00172B8F File Offset: 0x00170D8F
		public Asn1Object DataValueDescriptor
		{
			get
			{
				return this.dataValueDescriptor;
			}
			set
			{
				this.dataValueDescriptor = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x00172B98 File Offset: 0x00170D98
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x00172BA0 File Offset: 0x00170DA0
		public DerObjectIdentifier DirectReference
		{
			get
			{
				return this.directReference;
			}
			set
			{
				this.directReference = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x00172BA9 File Offset: 0x00170DA9
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x00172BB1 File Offset: 0x00170DB1
		public int Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (this.encoding < 0 || this.encoding > 2)
				{
					throw new InvalidOperationException("invalid encoding value: " + this.encoding);
				}
				this.encoding = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x00172BE7 File Offset: 0x00170DE7
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x00172BEF File Offset: 0x00170DEF
		public Asn1Object ExternalContent
		{
			get
			{
				return this.externalContent;
			}
			set
			{
				this.externalContent = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x00172BF8 File Offset: 0x00170DF8
		// (set) Token: 0x06003BEF RID: 15343 RVA: 0x00172C00 File Offset: 0x00170E00
		public DerInteger IndirectReference
		{
			get
			{
				return this.indirectReference;
			}
			set
			{
				this.indirectReference = value;
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x00172C09 File Offset: 0x00170E09
		private static Asn1Object GetObjFromVector(Asn1EncodableVector v, int index)
		{
			if (v.Count <= index)
			{
				throw new ArgumentException("too few objects in input vector", "v");
			}
			return v[index].ToAsn1Object();
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00172C30 File Offset: 0x00170E30
		private static void WriteEncodable(MemoryStream ms, Asn1Encodable e)
		{
			if (e != null)
			{
				byte[] derEncoded = e.GetDerEncoded();
				ms.Write(derEncoded, 0, derEncoded.Length);
			}
		}

		// Token: 0x040025A6 RID: 9638
		private DerObjectIdentifier directReference;

		// Token: 0x040025A7 RID: 9639
		private DerInteger indirectReference;

		// Token: 0x040025A8 RID: 9640
		private Asn1Object dataValueDescriptor;

		// Token: 0x040025A9 RID: 9641
		private int encoding;

		// Token: 0x040025AA RID: 9642
		private Asn1Object externalContent;
	}
}
