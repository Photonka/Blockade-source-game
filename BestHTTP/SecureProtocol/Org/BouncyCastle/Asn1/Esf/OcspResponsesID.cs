using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000738 RID: 1848
	public class OcspResponsesID : Asn1Encodable
	{
		// Token: 0x06004303 RID: 17155 RVA: 0x0018BCE8 File Offset: 0x00189EE8
		public static OcspResponsesID GetInstance(object obj)
		{
			if (obj == null || obj is OcspResponsesID)
			{
				return (OcspResponsesID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspResponsesID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspResponsesID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x0018BD38 File Offset: 0x00189F38
		private OcspResponsesID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspIdentifier = OcspIdentifier.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.ocspRepHash = OtherHash.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0018BDC2 File Offset: 0x00189FC2
		public OcspResponsesID(OcspIdentifier ocspIdentifier) : this(ocspIdentifier, null)
		{
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x0018BDCC File Offset: 0x00189FCC
		public OcspResponsesID(OcspIdentifier ocspIdentifier, OtherHash ocspRepHash)
		{
			if (ocspIdentifier == null)
			{
				throw new ArgumentNullException("ocspIdentifier");
			}
			this.ocspIdentifier = ocspIdentifier;
			this.ocspRepHash = ocspRepHash;
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x0018BDF0 File Offset: 0x00189FF0
		public OcspIdentifier OcspIdentifier
		{
			get
			{
				return this.ocspIdentifier;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x0018BDF8 File Offset: 0x00189FF8
		public OtherHash OcspRepHash
		{
			get
			{
				return this.ocspRepHash;
			}
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x0018BE00 File Offset: 0x0018A000
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.ocspIdentifier.ToAsn1Object()
			});
			if (this.ocspRepHash != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.ocspRepHash.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B06 RID: 11014
		private readonly OcspIdentifier ocspIdentifier;

		// Token: 0x04002B07 RID: 11015
		private readonly OtherHash ocspRepHash;
	}
}
