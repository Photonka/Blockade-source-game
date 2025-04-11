using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200068A RID: 1674
	public class IetfAttrSyntax : Asn1Encodable
	{
		// Token: 0x06003E4D RID: 15949 RVA: 0x00179944 File Offset: 0x00177B44
		public IetfAttrSyntax(Asn1Sequence seq)
		{
			int num = 0;
			if (seq[0] is Asn1TaggedObject)
			{
				this.policyAuthority = GeneralNames.GetInstance((Asn1TaggedObject)seq[0], false);
				num++;
			}
			else if (seq.Count == 2)
			{
				this.policyAuthority = GeneralNames.GetInstance(seq[0]);
				num++;
			}
			if (!(seq[num] is Asn1Sequence))
			{
				throw new ArgumentException("Non-IetfAttrSyntax encoding");
			}
			seq = (Asn1Sequence)seq[num];
			foreach (object obj in seq)
			{
				Asn1Object asn1Object = (Asn1Object)obj;
				int num2;
				if (asn1Object is DerObjectIdentifier)
				{
					num2 = 2;
				}
				else if (asn1Object is DerUtf8String)
				{
					num2 = 3;
				}
				else
				{
					if (!(asn1Object is DerOctetString))
					{
						throw new ArgumentException("Bad value type encoding IetfAttrSyntax");
					}
					num2 = 1;
				}
				if (this.valueChoice < 0)
				{
					this.valueChoice = num2;
				}
				if (num2 != this.valueChoice)
				{
					throw new ArgumentException("Mix of value types in IetfAttrSyntax");
				}
				this.values.Add(new Asn1Encodable[]
				{
					asn1Object
				});
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x00179A90 File Offset: 0x00177C90
		public GeneralNames PolicyAuthority
		{
			get
			{
				return this.policyAuthority;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003E4F RID: 15951 RVA: 0x00179A98 File Offset: 0x00177C98
		public int ValueType
		{
			get
			{
				return this.valueChoice;
			}
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00179AA0 File Offset: 0x00177CA0
		public object[] GetValues()
		{
			if (this.ValueType == 1)
			{
				Asn1OctetString[] array = new Asn1OctetString[this.values.Count];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = (Asn1OctetString)this.values[num];
				}
				return array;
			}
			if (this.ValueType == 2)
			{
				DerObjectIdentifier[] array2 = new DerObjectIdentifier[this.values.Count];
				for (int num2 = 0; num2 != array2.Length; num2++)
				{
					array2[num2] = (DerObjectIdentifier)this.values[num2];
				}
				return array2;
			}
			DerUtf8String[] array3 = new DerUtf8String[this.values.Count];
			for (int num3 = 0; num3 != array3.Length; num3++)
			{
				array3[num3] = (DerUtf8String)this.values[num3];
			}
			return array3;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00179B74 File Offset: 0x00177D74
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.policyAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.policyAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(this.values)
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400268F RID: 9871
		public const int ValueOctets = 1;

		// Token: 0x04002690 RID: 9872
		public const int ValueOid = 2;

		// Token: 0x04002691 RID: 9873
		public const int ValueUtf8 = 3;

		// Token: 0x04002692 RID: 9874
		internal readonly GeneralNames policyAuthority;

		// Token: 0x04002693 RID: 9875
		internal readonly Asn1EncodableVector values = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());

		// Token: 0x04002694 RID: 9876
		internal int valueChoice = -1;
	}
}
