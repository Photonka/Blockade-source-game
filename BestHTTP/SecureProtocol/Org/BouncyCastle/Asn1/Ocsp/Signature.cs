using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006FF RID: 1791
	public class Signature : Asn1Encodable
	{
		// Token: 0x060041B3 RID: 16819 RVA: 0x00186701 File Offset: 0x00184901
		public static Signature GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Signature.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00186710 File Offset: 0x00184910
		public static Signature GetInstance(object obj)
		{
			if (obj == null || obj is Signature)
			{
				return (Signature)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Signature((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x0018675D File Offset: 0x0018495D
		public Signature(AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue) : this(signatureAlgorithm, signatureValue, null)
		{
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x00186768 File Offset: 0x00184968
		public Signature(AlgorithmIdentifier signatureAlgorithm, DerBitString signatureValue, Asn1Sequence certs)
		{
			if (signatureAlgorithm == null)
			{
				throw new ArgumentException("signatureAlgorithm");
			}
			if (signatureValue == null)
			{
				throw new ArgumentException("signatureValue");
			}
			this.signatureAlgorithm = signatureAlgorithm;
			this.signatureValue = signatureValue;
			this.certs = certs;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x001867A4 File Offset: 0x001849A4
		private Signature(Asn1Sequence seq)
		{
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.signatureValue = (DerBitString)seq[1];
			if (seq.Count == 3)
			{
				this.certs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[2], true);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x001867FC File Offset: 0x001849FC
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x00186804 File Offset: 0x00184A04
		public DerBitString SignatureValue
		{
			get
			{
				return this.signatureValue;
			}
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0018680C File Offset: 0x00184A0C
		public byte[] GetSignatureOctets()
		{
			return this.signatureValue.GetOctets();
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x00186819 File Offset: 0x00184A19
		public Asn1Sequence Certs
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x00186824 File Offset: 0x00184A24
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.signatureAlgorithm,
				this.signatureValue
			});
			if (this.certs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.certs)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002994 RID: 10644
		internal AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x04002995 RID: 10645
		internal DerBitString signatureValue;

		// Token: 0x04002996 RID: 10646
		internal Asn1Sequence certs;
	}
}
