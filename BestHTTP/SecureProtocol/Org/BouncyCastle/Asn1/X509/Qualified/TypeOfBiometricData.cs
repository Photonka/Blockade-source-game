using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006BE RID: 1726
	public class TypeOfBiometricData : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003FEB RID: 16363 RVA: 0x0017FAC0 File Offset: 0x0017DCC0
		public static TypeOfBiometricData GetInstance(object obj)
		{
			if (obj == null || obj is TypeOfBiometricData)
			{
				return (TypeOfBiometricData)obj;
			}
			if (obj is DerInteger)
			{
				return new TypeOfBiometricData(DerInteger.GetInstance(obj).Value.IntValue);
			}
			if (obj is DerObjectIdentifier)
			{
				return new TypeOfBiometricData(DerObjectIdentifier.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x0017FB2B File Offset: 0x0017DD2B
		public TypeOfBiometricData(int predefinedBiometricType)
		{
			if (predefinedBiometricType == 0 || predefinedBiometricType == 1)
			{
				this.obj = new DerInteger(predefinedBiometricType);
				return;
			}
			throw new ArgumentException("unknow PredefinedBiometricType : " + predefinedBiometricType);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x0017FB5C File Offset: 0x0017DD5C
		public TypeOfBiometricData(DerObjectIdentifier biometricDataOid)
		{
			this.obj = biometricDataOid;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x0017FB6B File Offset: 0x0017DD6B
		public bool IsPredefined
		{
			get
			{
				return this.obj is DerInteger;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x0017FB7B File Offset: 0x0017DD7B
		public int PredefinedBiometricType
		{
			get
			{
				return ((DerInteger)this.obj).Value.IntValue;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x0017FB92 File Offset: 0x0017DD92
		public DerObjectIdentifier BiometricDataOid
		{
			get
			{
				return (DerObjectIdentifier)this.obj;
			}
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x0017FB9F File Offset: 0x0017DD9F
		public override Asn1Object ToAsn1Object()
		{
			return this.obj.ToAsn1Object();
		}

		// Token: 0x040027B9 RID: 10169
		public const int Picture = 0;

		// Token: 0x040027BA RID: 10170
		public const int HandwrittenSignature = 1;

		// Token: 0x040027BB RID: 10171
		internal Asn1Encodable obj;
	}
}
