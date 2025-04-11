using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006BD RID: 1725
	public class SemanticsInformation : Asn1Encodable
	{
		// Token: 0x06003FE3 RID: 16355 RVA: 0x0017F928 File Offset: 0x0017DB28
		public static SemanticsInformation GetInstance(object obj)
		{
			if (obj == null || obj is SemanticsInformation)
			{
				return (SemanticsInformation)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SemanticsInformation(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0017F978 File Offset: 0x0017DB78
		public SemanticsInformation(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("no objects in SemanticsInformation");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is DerObjectIdentifier)
			{
				this.semanticsIdentifier = DerObjectIdentifier.GetInstance(obj);
				if (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
				else
				{
					obj = null;
				}
			}
			if (obj != null)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(obj);
				this.nameRegistrationAuthorities = new GeneralName[instance.Count];
				for (int i = 0; i < instance.Count; i++)
				{
					this.nameRegistrationAuthorities[i] = GeneralName.GetInstance(instance[i]);
				}
			}
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0017FA1B File Offset: 0x0017DC1B
		public SemanticsInformation(DerObjectIdentifier semanticsIdentifier, GeneralName[] generalNames)
		{
			this.semanticsIdentifier = semanticsIdentifier;
			this.nameRegistrationAuthorities = generalNames;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0017FA31 File Offset: 0x0017DC31
		public SemanticsInformation(DerObjectIdentifier semanticsIdentifier)
		{
			this.semanticsIdentifier = semanticsIdentifier;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0017FA40 File Offset: 0x0017DC40
		public SemanticsInformation(GeneralName[] generalNames)
		{
			this.nameRegistrationAuthorities = generalNames;
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x0017FA4F File Offset: 0x0017DC4F
		public DerObjectIdentifier SemanticsIdentifier
		{
			get
			{
				return this.semanticsIdentifier;
			}
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x0017FA57 File Offset: 0x0017DC57
		public GeneralName[] GetNameRegistrationAuthorities()
		{
			return this.nameRegistrationAuthorities;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x0017FA60 File Offset: 0x0017DC60
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.semanticsIdentifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.semanticsIdentifier
				});
			}
			if (this.nameRegistrationAuthorities != null)
			{
				Asn1EncodableVector asn1EncodableVector2 = asn1EncodableVector;
				Asn1Encodable[] array = new Asn1Encodable[1];
				int num = 0;
				Asn1Encodable[] v = this.nameRegistrationAuthorities;
				array[num] = new DerSequence(v);
				asn1EncodableVector2.Add(array);
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027B7 RID: 10167
		private readonly DerObjectIdentifier semanticsIdentifier;

		// Token: 0x040027B8 RID: 10168
		private readonly GeneralName[] nameRegistrationAuthorities;
	}
}
