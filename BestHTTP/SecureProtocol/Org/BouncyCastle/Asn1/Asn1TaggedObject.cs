using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200061C RID: 1564
	public abstract class Asn1TaggedObject : Asn1Object, Asn1TaggedObjectParser, IAsn1Convertible
	{
		// Token: 0x06003B3B RID: 15163 RVA: 0x00170DF0 File Offset: 0x0016EFF0
		internal static bool IsConstructed(bool isExplicit, Asn1Object obj)
		{
			if (isExplicit || obj is Asn1Sequence || obj is Asn1Set)
			{
				return true;
			}
			Asn1TaggedObject asn1TaggedObject = obj as Asn1TaggedObject;
			return asn1TaggedObject != null && Asn1TaggedObject.IsConstructed(asn1TaggedObject.IsExplicit(), asn1TaggedObject.GetObject());
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x00170E2F File Offset: 0x0016F02F
		public static Asn1TaggedObject GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			if (explicitly)
			{
				return (Asn1TaggedObject)obj.GetObject();
			}
			throw new ArgumentException("implicitly tagged tagged object");
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00170E4A File Offset: 0x0016F04A
		public static Asn1TaggedObject GetInstance(object obj)
		{
			if (obj == null || obj is Asn1TaggedObject)
			{
				return (Asn1TaggedObject)obj;
			}
			throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x00170E78 File Offset: 0x0016F078
		protected Asn1TaggedObject(int tagNo, Asn1Encodable obj)
		{
			this.explicitly = true;
			this.tagNo = tagNo;
			this.obj = obj;
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x00170E9C File Offset: 0x0016F09C
		protected Asn1TaggedObject(bool explicitly, int tagNo, Asn1Encodable obj)
		{
			this.explicitly = (explicitly || obj is IAsn1Choice);
			this.tagNo = tagNo;
			this.obj = obj;
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x00170ED0 File Offset: 0x0016F0D0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			Asn1TaggedObject asn1TaggedObject = asn1Object as Asn1TaggedObject;
			return asn1TaggedObject != null && (this.tagNo == asn1TaggedObject.tagNo && this.explicitly == asn1TaggedObject.explicitly) && object.Equals(this.GetObject(), asn1TaggedObject.GetObject());
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x00170F18 File Offset: 0x0016F118
		protected override int Asn1GetHashCode()
		{
			int num = this.tagNo.GetHashCode();
			if (this.obj != null)
			{
				num ^= this.obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x00170F48 File Offset: 0x0016F148
		public int TagNo
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x00170F50 File Offset: 0x0016F150
		public bool IsExplicit()
		{
			return this.explicitly;
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x00170F58 File Offset: 0x0016F158
		public Asn1Object GetObject()
		{
			if (this.obj != null)
			{
				return this.obj.ToAsn1Object();
			}
			return null;
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x00170F70 File Offset: 0x0016F170
		public IAsn1Convertible GetObjectParser(int tag, bool isExplicit)
		{
			if (tag == 4)
			{
				return Asn1OctetString.GetInstance(this, isExplicit).Parser;
			}
			if (tag == 16)
			{
				return Asn1Sequence.GetInstance(this, isExplicit).Parser;
			}
			if (tag == 17)
			{
				return Asn1Set.GetInstance(this, isExplicit).Parser;
			}
			if (isExplicit)
			{
				return this.GetObject();
			}
			throw Platform.CreateNotImplementedException("implicit tagging for tag: " + tag);
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x00170FD1 File Offset: 0x0016F1D1
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[",
				this.tagNo,
				"]",
				this.obj
			});
		}

		// Token: 0x04002565 RID: 9573
		internal int tagNo;

		// Token: 0x04002566 RID: 9574
		internal bool explicitly = true;

		// Token: 0x04002567 RID: 9575
		internal Asn1Encodable obj;
	}
}
