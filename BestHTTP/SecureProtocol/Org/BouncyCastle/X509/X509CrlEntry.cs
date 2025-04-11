using System;
using System.Collections;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000230 RID: 560
	public class X509CrlEntry : X509ExtensionBase
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x000AE680 File Offset: 0x000AC880
		public X509CrlEntry(CrlEntry c)
		{
			this.c = c;
			this.certificateIssuer = this.loadCertificateIssuer();
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000AE69B File Offset: 0x000AC89B
		public X509CrlEntry(CrlEntry c, bool isIndirect, X509Name previousCertificateIssuer)
		{
			this.c = c;
			this.isIndirect = isIndirect;
			this.previousCertificateIssuer = previousCertificateIssuer;
			this.certificateIssuer = this.loadCertificateIssuer();
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000AE6C4 File Offset: 0x000AC8C4
		private X509Name loadCertificateIssuer()
		{
			if (!this.isIndirect)
			{
				return null;
			}
			Asn1OctetString extensionValue = this.GetExtensionValue(X509Extensions.CertificateIssuer);
			if (extensionValue == null)
			{
				return this.previousCertificateIssuer;
			}
			try
			{
				GeneralName[] names = GeneralNames.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).GetNames();
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].TagNo == 4)
					{
						return X509Name.GetInstance(names[i].Name);
					}
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000AE744 File Offset: 0x000AC944
		public X509Name GetCertificateIssuer()
		{
			return this.certificateIssuer;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000AE74C File Offset: 0x000AC94C
		protected override X509Extensions GetX509Extensions()
		{
			return this.c.Extensions;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000AE75C File Offset: 0x000AC95C
		public byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000AE794 File Offset: 0x000AC994
		public BigInteger SerialNumber
		{
			get
			{
				return this.c.UserCertificate.Value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000AE7A6 File Offset: 0x000AC9A6
		public DateTime RevocationDate
		{
			get
			{
				return this.c.RevocationDate.ToDateTime();
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x000AE7B8 File Offset: 0x000AC9B8
		public bool HasExtensions
		{
			get
			{
				return this.c.Extensions != null;
			}
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000AE7C8 File Offset: 0x000AC9C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("        userCertificate: ").Append(this.SerialNumber).Append(newLine);
			stringBuilder.Append("         revocationDate: ").Append(this.RevocationDate).Append(newLine);
			stringBuilder.Append("      certificateIssuer: ").Append(this.GetCertificateIssuer()).Append(newLine);
			X509Extensions extensions = this.c.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("   crlEntryExtensions:").Append(newLine);
					for (;;)
					{
						DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
						X509Extension extension = extensions.GetExtension(derObjectIdentifier);
						if (extension.Value != null)
						{
							Asn1Object asn1Object = Asn1Object.FromByteArray(extension.Value.GetOctets());
							stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
							try
							{
								if (derObjectIdentifier.Equals(X509Extensions.ReasonCode))
								{
									stringBuilder.Append(new CrlReason(DerEnumerated.GetInstance(asn1Object)));
								}
								else if (derObjectIdentifier.Equals(X509Extensions.CertificateIssuer))
								{
									stringBuilder.Append("Certificate issuer: ").Append(GeneralNames.GetInstance((Asn1Sequence)asn1Object));
								}
								else
								{
									stringBuilder.Append(derObjectIdentifier.Id);
									stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object));
								}
								stringBuilder.Append(newLine);
								goto IL_1B0;
							}
							catch (Exception)
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append("*****").Append(newLine);
								goto IL_1B0;
							}
							goto IL_1A8;
						}
						goto IL_1A8;
						IL_1B0:
						if (!enumerator.MoveNext())
						{
							break;
						}
						continue;
						IL_1A8:
						stringBuilder.Append(newLine);
						goto IL_1B0;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040014F4 RID: 5364
		private CrlEntry c;

		// Token: 0x040014F5 RID: 5365
		private bool isIndirect;

		// Token: 0x040014F6 RID: 5366
		private X509Name previousCertificateIssuer;

		// Token: 0x040014F7 RID: 5367
		private X509Name certificateIssuer;
	}
}
