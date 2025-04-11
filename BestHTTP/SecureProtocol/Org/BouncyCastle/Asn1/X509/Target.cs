using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200069D RID: 1693
	public class Target : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003ECD RID: 16077 RVA: 0x0017B3DD File Offset: 0x001795DD
		public static Target GetInstance(object obj)
		{
			if (obj is Target)
			{
				return (Target)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new Target((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0017B41C File Offset: 0x0017961C
		private Target(Asn1TaggedObject tagObj)
		{
			Target.Choice tagNo = (Target.Choice)tagObj.TagNo;
			if (tagNo == Target.Choice.Name)
			{
				this.targetName = GeneralName.GetInstance(tagObj, true);
				return;
			}
			if (tagNo != Target.Choice.Group)
			{
				throw new ArgumentException("unknown tag: " + tagObj.TagNo);
			}
			this.targetGroup = GeneralName.GetInstance(tagObj, true);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0017B475 File Offset: 0x00179675
		public Target(Target.Choice type, GeneralName name) : this(new DerTaggedObject((int)type, name))
		{
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x0017B484 File Offset: 0x00179684
		public virtual GeneralName TargetGroup
		{
			get
			{
				return this.targetGroup;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x0017B48C File Offset: 0x0017968C
		public virtual GeneralName TargetName
		{
			get
			{
				return this.targetName;
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0017B494 File Offset: 0x00179694
		public override Asn1Object ToAsn1Object()
		{
			if (this.targetName != null)
			{
				return new DerTaggedObject(true, 0, this.targetName);
			}
			return new DerTaggedObject(true, 1, this.targetGroup);
		}

		// Token: 0x040026DB RID: 9947
		private readonly GeneralName targetName;

		// Token: 0x040026DC RID: 9948
		private readonly GeneralName targetGroup;

		// Token: 0x0200097E RID: 2430
		public enum Choice
		{
			// Token: 0x040035F9 RID: 13817
			Name,
			// Token: 0x040035FA RID: 13818
			Group
		}
	}
}
