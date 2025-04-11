using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x0200023F RID: 575
	public class X509AttrCertStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x000B03BC File Offset: 0x000AE5BC
		public X509AttrCertStoreSelector()
		{
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000B03DC File Offset: 0x000AE5DC
		private X509AttrCertStoreSelector(X509AttrCertStoreSelector o)
		{
			this.attributeCert = o.attributeCert;
			this.attributeCertificateValid = o.attributeCertificateValid;
			this.holder = o.holder;
			this.issuer = o.issuer;
			this.serialNumber = o.serialNumber;
			this.targetGroups = new HashSet(o.targetGroups);
			this.targetNames = new HashSet(o.targetNames);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000B0464 File Offset: 0x000AE664
		public bool Match(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			IX509AttributeCertificate ix509AttributeCertificate = obj as IX509AttributeCertificate;
			if (ix509AttributeCertificate == null)
			{
				return false;
			}
			if (this.attributeCert != null && !this.attributeCert.Equals(ix509AttributeCertificate))
			{
				return false;
			}
			if (this.serialNumber != null && !ix509AttributeCertificate.SerialNumber.Equals(this.serialNumber))
			{
				return false;
			}
			if (this.holder != null && !ix509AttributeCertificate.Holder.Equals(this.holder))
			{
				return false;
			}
			if (this.issuer != null && !ix509AttributeCertificate.Issuer.Equals(this.issuer))
			{
				return false;
			}
			if (this.attributeCertificateValid != null && !ix509AttributeCertificate.IsValid(this.attributeCertificateValid.Value))
			{
				return false;
			}
			if (this.targetNames.Count > 0 || this.targetGroups.Count > 0)
			{
				Asn1OctetString extensionValue = ix509AttributeCertificate.GetExtensionValue(X509Extensions.TargetInformation);
				if (extensionValue != null)
				{
					TargetInformation instance;
					try
					{
						instance = TargetInformation.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
					}
					catch (Exception)
					{
						return false;
					}
					Targets[] targetsObjects = instance.GetTargetsObjects();
					if (this.targetNames.Count > 0)
					{
						bool flag = false;
						int num = 0;
						while (num < targetsObjects.Length && !flag)
						{
							Target[] targets = targetsObjects[num].GetTargets();
							for (int i = 0; i < targets.Length; i++)
							{
								GeneralName targetName = targets[i].TargetName;
								if (targetName != null && this.targetNames.Contains(targetName))
								{
									flag = true;
									break;
								}
							}
							num++;
						}
						if (!flag)
						{
							return false;
						}
					}
					if (this.targetGroups.Count <= 0)
					{
						return true;
					}
					bool flag2 = false;
					int num2 = 0;
					while (num2 < targetsObjects.Length && !flag2)
					{
						Target[] targets2 = targetsObjects[num2].GetTargets();
						for (int j = 0; j < targets2.Length; j++)
						{
							GeneralName targetGroup = targets2[j].TargetGroup;
							if (targetGroup != null && this.targetGroups.Contains(targetGroup))
							{
								flag2 = true;
								break;
							}
						}
						num2++;
					}
					if (!flag2)
					{
						return false;
					}
					return true;
				}
			}
			return true;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000B0658 File Offset: 0x000AE858
		public object Clone()
		{
			return new X509AttrCertStoreSelector(this);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x000B0660 File Offset: 0x000AE860
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x000B0668 File Offset: 0x000AE868
		public IX509AttributeCertificate AttributeCert
		{
			get
			{
				return this.attributeCert;
			}
			set
			{
				this.attributeCert = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x000B0671 File Offset: 0x000AE871
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x000B0679 File Offset: 0x000AE879
		[Obsolete("Use AttributeCertificateValid instead")]
		public DateTimeObject AttribueCertificateValid
		{
			get
			{
				return this.attributeCertificateValid;
			}
			set
			{
				this.attributeCertificateValid = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000B0671 File Offset: 0x000AE871
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x000B0679 File Offset: 0x000AE879
		public DateTimeObject AttributeCertificateValid
		{
			get
			{
				return this.attributeCertificateValid;
			}
			set
			{
				this.attributeCertificateValid = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000B0682 File Offset: 0x000AE882
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x000B068A File Offset: 0x000AE88A
		public AttributeCertificateHolder Holder
		{
			get
			{
				return this.holder;
			}
			set
			{
				this.holder = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000B0693 File Offset: 0x000AE893
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x000B069B File Offset: 0x000AE89B
		public AttributeCertificateIssuer Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x000B06A4 File Offset: 0x000AE8A4
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x000B06AC File Offset: 0x000AE8AC
		public BigInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000B06B5 File Offset: 0x000AE8B5
		public void AddTargetName(GeneralName name)
		{
			this.targetNames.Add(name);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000B06C3 File Offset: 0x000AE8C3
		public void AddTargetName(byte[] name)
		{
			this.AddTargetName(GeneralName.GetInstance(Asn1Object.FromByteArray(name)));
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000B06D6 File Offset: 0x000AE8D6
		public void SetTargetNames(IEnumerable names)
		{
			this.targetNames = this.ExtractGeneralNames(names);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000B06E5 File Offset: 0x000AE8E5
		public IEnumerable GetTargetNames()
		{
			return new EnumerableProxy(this.targetNames);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000B06F2 File Offset: 0x000AE8F2
		public void AddTargetGroup(GeneralName group)
		{
			this.targetGroups.Add(group);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000B0700 File Offset: 0x000AE900
		public void AddTargetGroup(byte[] name)
		{
			this.AddTargetGroup(GeneralName.GetInstance(Asn1Object.FromByteArray(name)));
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000B0713 File Offset: 0x000AE913
		public void SetTargetGroups(IEnumerable names)
		{
			this.targetGroups = this.ExtractGeneralNames(names);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000B0722 File Offset: 0x000AE922
		public IEnumerable GetTargetGroups()
		{
			return new EnumerableProxy(this.targetGroups);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000B0730 File Offset: 0x000AE930
		private ISet ExtractGeneralNames(IEnumerable names)
		{
			ISet set = new HashSet();
			if (names != null)
			{
				foreach (object obj in names)
				{
					if (obj is GeneralName)
					{
						set.Add(obj);
					}
					else
					{
						set.Add(GeneralName.GetInstance(Asn1Object.FromByteArray((byte[])obj)));
					}
				}
			}
			return set;
		}

		// Token: 0x04001521 RID: 5409
		private IX509AttributeCertificate attributeCert;

		// Token: 0x04001522 RID: 5410
		private DateTimeObject attributeCertificateValid;

		// Token: 0x04001523 RID: 5411
		private AttributeCertificateHolder holder;

		// Token: 0x04001524 RID: 5412
		private AttributeCertificateIssuer issuer;

		// Token: 0x04001525 RID: 5413
		private BigInteger serialNumber;

		// Token: 0x04001526 RID: 5414
		private ISet targetNames = new HashSet();

		// Token: 0x04001527 RID: 5415
		private ISet targetGroups = new HashSet();
	}
}
