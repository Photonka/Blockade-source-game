using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200062E RID: 1582
	public class BerTaggedObject : DerTaggedObject
	{
		// Token: 0x06003B8F RID: 15247 RVA: 0x001717A7 File Offset: 0x0016F9A7
		public BerTaggedObject(int tagNo, Asn1Encodable obj) : base(tagNo, obj)
		{
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x001717B1 File Offset: 0x0016F9B1
		public BerTaggedObject(bool explicitly, int tagNo, Asn1Encodable obj) : base(explicitly, tagNo, obj)
		{
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x001717BC File Offset: 0x0016F9BC
		public BerTaggedObject(int tagNo) : base(false, tagNo, BerSequence.Empty)
		{
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x001717CC File Offset: 0x0016F9CC
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteTag(160, this.tagNo);
				derOut.WriteByte(128);
				if (!base.IsEmpty())
				{
					if (!this.explicitly)
					{
						IEnumerable enumerable;
						if (this.obj is Asn1OctetString)
						{
							if (this.obj is BerOctetString)
							{
								enumerable = (BerOctetString)this.obj;
							}
							else
							{
								enumerable = new BerOctetString(((Asn1OctetString)this.obj).GetOctets());
							}
						}
						else if (this.obj is Asn1Sequence)
						{
							enumerable = (Asn1Sequence)this.obj;
						}
						else
						{
							if (!(this.obj is Asn1Set))
							{
								throw Platform.CreateNotImplementedException(Platform.GetTypeName(this.obj));
							}
							enumerable = (Asn1Set)this.obj;
						}
						using (IEnumerator enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Asn1Encodable obj2 = (Asn1Encodable)obj;
								derOut.WriteObject(obj2);
							}
							goto IL_10F;
						}
					}
					derOut.WriteObject(this.obj);
				}
				IL_10F:
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}
	}
}
