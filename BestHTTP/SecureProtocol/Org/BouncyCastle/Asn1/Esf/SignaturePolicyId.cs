using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000740 RID: 1856
	public class SignaturePolicyId : Asn1Encodable
	{
		// Token: 0x0600433C RID: 17212 RVA: 0x0018CAE0 File Offset: 0x0018ACE0
		public static SignaturePolicyId GetInstance(object obj)
		{
			if (obj == null || obj is SignaturePolicyId)
			{
				return (SignaturePolicyId)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignaturePolicyId((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'SignaturePolicyId' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x0018CB30 File Offset: 0x0018AD30
		private SignaturePolicyId(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.sigPolicyIdentifier = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.sigPolicyHash = OtherHashAlgAndValue.GetInstance(seq[1].ToAsn1Object());
			if (seq.Count > 2)
			{
				this.sigPolicyQualifiers = (Asn1Sequence)seq[2].ToAsn1Object();
			}
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0018CBD1 File Offset: 0x0018ADD1
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash) : this(sigPolicyIdentifier, sigPolicyHash, null)
		{
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x0018CBDC File Offset: 0x0018ADDC
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash, params SigPolicyQualifierInfo[] sigPolicyQualifiers)
		{
			if (sigPolicyIdentifier == null)
			{
				throw new ArgumentNullException("sigPolicyIdentifier");
			}
			if (sigPolicyHash == null)
			{
				throw new ArgumentNullException("sigPolicyHash");
			}
			this.sigPolicyIdentifier = sigPolicyIdentifier;
			this.sigPolicyHash = sigPolicyHash;
			if (sigPolicyQualifiers != null)
			{
				this.sigPolicyQualifiers = new DerSequence(sigPolicyQualifiers);
			}
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0018CC2C File Offset: 0x0018AE2C
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash, IEnumerable sigPolicyQualifiers)
		{
			if (sigPolicyIdentifier == null)
			{
				throw new ArgumentNullException("sigPolicyIdentifier");
			}
			if (sigPolicyHash == null)
			{
				throw new ArgumentNullException("sigPolicyHash");
			}
			this.sigPolicyIdentifier = sigPolicyIdentifier;
			this.sigPolicyHash = sigPolicyHash;
			if (sigPolicyQualifiers != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(sigPolicyQualifiers, typeof(SigPolicyQualifierInfo)))
				{
					throw new ArgumentException("Must contain only 'SigPolicyQualifierInfo' objects", "sigPolicyQualifiers");
				}
				this.sigPolicyQualifiers = new DerSequence(Asn1EncodableVector.FromEnumerable(sigPolicyQualifiers));
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x0018CC9F File Offset: 0x0018AE9F
		public DerObjectIdentifier SigPolicyIdentifier
		{
			get
			{
				return this.sigPolicyIdentifier;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x0018CCA7 File Offset: 0x0018AEA7
		public OtherHashAlgAndValue SigPolicyHash
		{
			get
			{
				return this.sigPolicyHash;
			}
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x0018CCB0 File Offset: 0x0018AEB0
		public SigPolicyQualifierInfo[] GetSigPolicyQualifiers()
		{
			if (this.sigPolicyQualifiers == null)
			{
				return null;
			}
			SigPolicyQualifierInfo[] array = new SigPolicyQualifierInfo[this.sigPolicyQualifiers.Count];
			for (int i = 0; i < this.sigPolicyQualifiers.Count; i++)
			{
				array[i] = SigPolicyQualifierInfo.GetInstance(this.sigPolicyQualifiers[i]);
			}
			return array;
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x0018CD04 File Offset: 0x0018AF04
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.sigPolicyIdentifier,
				this.sigPolicyHash.ToAsn1Object()
			});
			if (this.sigPolicyQualifiers != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.sigPolicyQualifiers.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B17 RID: 11031
		private readonly DerObjectIdentifier sigPolicyIdentifier;

		// Token: 0x04002B18 RID: 11032
		private readonly OtherHashAlgAndValue sigPolicyHash;

		// Token: 0x04002B19 RID: 11033
		private readonly Asn1Sequence sigPolicyQualifiers;
	}
}
