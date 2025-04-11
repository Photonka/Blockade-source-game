using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200069E RID: 1694
	public class TargetInformation : Asn1Encodable
	{
		// Token: 0x06003ED3 RID: 16083 RVA: 0x0017B4B9 File Offset: 0x001796B9
		public static TargetInformation GetInstance(object obj)
		{
			if (obj is TargetInformation)
			{
				return (TargetInformation)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TargetInformation((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0017B4F8 File Offset: 0x001796F8
		private TargetInformation(Asn1Sequence targets)
		{
			this.targets = targets;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0017B508 File Offset: 0x00179708
		public virtual Targets[] GetTargetsObjects()
		{
			Targets[] array = new Targets[this.targets.Count];
			for (int i = 0; i < this.targets.Count; i++)
			{
				array[i] = Targets.GetInstance(this.targets[i]);
			}
			return array;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0017B551 File Offset: 0x00179751
		public TargetInformation(Targets targets)
		{
			this.targets = new DerSequence(targets);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0017B565 File Offset: 0x00179765
		public TargetInformation(Target[] targets) : this(new Targets(targets))
		{
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0017B573 File Offset: 0x00179773
		public override Asn1Object ToAsn1Object()
		{
			return this.targets;
		}

		// Token: 0x040026DD RID: 9949
		private readonly Asn1Sequence targets;
	}
}
