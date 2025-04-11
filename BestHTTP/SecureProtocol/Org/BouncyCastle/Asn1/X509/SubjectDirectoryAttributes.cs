using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200069A RID: 1690
	public class SubjectDirectoryAttributes : Asn1Encodable
	{
		// Token: 0x06003EB4 RID: 16052 RVA: 0x0017B028 File Offset: 0x00179228
		public static SubjectDirectoryAttributes GetInstance(object obj)
		{
			if (obj == null || obj is SubjectDirectoryAttributes)
			{
				return (SubjectDirectoryAttributes)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SubjectDirectoryAttributes((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0017B078 File Offset: 0x00179278
		private SubjectDirectoryAttributes(Asn1Sequence seq)
		{
			this.attributes = Platform.CreateArrayList();
			foreach (object obj in seq)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(obj);
				this.attributes.Add(AttributeX509.GetInstance(instance));
			}
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0017B0E8 File Offset: 0x001792E8
		[Obsolete]
		public SubjectDirectoryAttributes(ArrayList attributes) : this(attributes)
		{
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0017B0F1 File Offset: 0x001792F1
		public SubjectDirectoryAttributes(IList attributes)
		{
			this.attributes = Platform.CreateArrayList(attributes);
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0017B108 File Offset: 0x00179308
		public override Asn1Object ToAsn1Object()
		{
			AttributeX509[] array = new AttributeX509[this.attributes.Count];
			for (int i = 0; i < this.attributes.Count; i++)
			{
				array[i] = (AttributeX509)this.attributes[i];
			}
			Asn1Encodable[] v = array;
			return new DerSequence(v);
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x0017B158 File Offset: 0x00179358
		public IEnumerable Attributes
		{
			get
			{
				return new EnumerableProxy(this.attributes);
			}
		}

		// Token: 0x040026D7 RID: 9943
		private readonly IList attributes;
	}
}
