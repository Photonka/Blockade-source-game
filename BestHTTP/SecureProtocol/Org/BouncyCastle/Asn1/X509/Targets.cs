using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200069F RID: 1695
	public class Targets : Asn1Encodable
	{
		// Token: 0x06003ED9 RID: 16089 RVA: 0x0017B57B File Offset: 0x0017977B
		public static Targets GetInstance(object obj)
		{
			if (obj is Targets)
			{
				return (Targets)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Targets((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x0017B5BA File Offset: 0x001797BA
		private Targets(Asn1Sequence targets)
		{
			this.targets = targets;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0017B5CC File Offset: 0x001797CC
		public Targets(Target[] targets)
		{
			this.targets = new DerSequence(targets);
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x0017B5F0 File Offset: 0x001797F0
		public virtual Target[] GetTargets()
		{
			Target[] array = new Target[this.targets.Count];
			for (int i = 0; i < this.targets.Count; i++)
			{
				array[i] = Target.GetInstance(this.targets[i]);
			}
			return array;
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0017B639 File Offset: 0x00179839
		public override Asn1Object ToAsn1Object()
		{
			return this.targets;
		}

		// Token: 0x040026DE RID: 9950
		private readonly Asn1Sequence targets;
	}
}
