using System;
using System.Text;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000698 RID: 1688
	public class RoleSyntax : Asn1Encodable
	{
		// Token: 0x06003EA2 RID: 16034 RVA: 0x0017AC12 File Offset: 0x00178E12
		public static RoleSyntax GetInstance(object obj)
		{
			if (obj is RoleSyntax)
			{
				return (RoleSyntax)obj;
			}
			if (obj != null)
			{
				return new RoleSyntax(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0017AC34 File Offset: 0x00178E34
		public RoleSyntax(GeneralNames roleAuthority, GeneralName roleName)
		{
			if (roleName == null || roleName.TagNo != 6 || ((IAsn1String)roleName.Name).GetString().Equals(""))
			{
				throw new ArgumentException("the role name MUST be non empty and MUST use the URI option of GeneralName");
			}
			this.roleAuthority = roleAuthority;
			this.roleName = roleName;
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0017AC88 File Offset: 0x00178E88
		public RoleSyntax(GeneralName roleName) : this(null, roleName)
		{
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x0017AC92 File Offset: 0x00178E92
		public RoleSyntax(string roleName) : this(new GeneralName(6, (roleName == null) ? "" : roleName))
		{
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0017ACAC File Offset: 0x00178EAC
		private RoleSyntax(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				int tagNo = instance.TagNo;
				if (tagNo != 0)
				{
					if (tagNo != 1)
					{
						throw new ArgumentException("Unknown tag in RoleSyntax");
					}
					this.roleName = GeneralName.GetInstance(instance, true);
				}
				else
				{
					this.roleAuthority = GeneralNames.GetInstance(instance, false);
				}
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x0017AD43 File Offset: 0x00178F43
		public GeneralNames RoleAuthority
		{
			get
			{
				return this.roleAuthority;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x0017AD4B File Offset: 0x00178F4B
		public GeneralName RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0017AD53 File Offset: 0x00178F53
		public string GetRoleNameAsString()
		{
			return ((IAsn1String)this.roleName.Name).GetString();
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x0017AD6C File Offset: 0x00178F6C
		public string[] GetRoleAuthorityAsString()
		{
			if (this.roleAuthority == null)
			{
				return new string[0];
			}
			GeneralName[] names = this.roleAuthority.GetNames();
			string[] array = new string[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				Asn1Encodable name = names[i].Name;
				if (name is IAsn1String)
				{
					array[i] = ((IAsn1String)name).GetString();
				}
				else
				{
					array[i] = name.ToString();
				}
			}
			return array;
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x0017ADD8 File Offset: 0x00178FD8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.roleAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.roleAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(true, 1, this.roleName)
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0017AE38 File Offset: 0x00179038
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Name: " + this.GetRoleNameAsString() + " - Auth: ");
			if (this.roleAuthority == null || this.roleAuthority.GetNames().Length == 0)
			{
				stringBuilder.Append("N/A");
			}
			else
			{
				string[] roleAuthorityAsString = this.GetRoleAuthorityAsString();
				stringBuilder.Append('[').Append(roleAuthorityAsString[0]);
				for (int i = 1; i < roleAuthorityAsString.Length; i++)
				{
					stringBuilder.Append(", ").Append(roleAuthorityAsString[i]);
				}
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040026D3 RID: 9939
		private readonly GeneralNames roleAuthority;

		// Token: 0x040026D4 RID: 9940
		private readonly GeneralName roleName;
	}
}
