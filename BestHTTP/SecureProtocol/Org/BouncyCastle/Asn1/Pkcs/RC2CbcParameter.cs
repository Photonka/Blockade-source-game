using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E8 RID: 1768
	public class RC2CbcParameter : Asn1Encodable
	{
		// Token: 0x06004102 RID: 16642 RVA: 0x001847FE File Offset: 0x001829FE
		public static RC2CbcParameter GetInstance(object obj)
		{
			if (obj is Asn1Sequence)
			{
				return new RC2CbcParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x0018482E File Offset: 0x00182A2E
		public RC2CbcParameter(byte[] iv)
		{
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x00184842 File Offset: 0x00182A42
		public RC2CbcParameter(int parameterVersion, byte[] iv)
		{
			this.version = new DerInteger(parameterVersion);
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x00184864 File Offset: 0x00182A64
		private RC2CbcParameter(Asn1Sequence seq)
		{
			if (seq.Count == 1)
			{
				this.iv = (Asn1OctetString)seq[0];
				return;
			}
			this.version = (DerInteger)seq[0];
			this.iv = (Asn1OctetString)seq[1];
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x001848B7 File Offset: 0x00182AB7
		public BigInteger RC2ParameterVersion
		{
			get
			{
				if (this.version != null)
				{
					return this.version.Value;
				}
				return null;
			}
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x001848CE File Offset: 0x00182ACE
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv.GetOctets());
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x001848E0 File Offset: 0x00182AE0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.version != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.version
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.iv
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400292E RID: 10542
		internal DerInteger version;

		// Token: 0x0400292F RID: 10543
		internal Asn1OctetString iv;
	}
}
