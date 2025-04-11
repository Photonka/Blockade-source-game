using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003EC RID: 1004
	public class Certificate
	{
		// Token: 0x06002906 RID: 10502 RVA: 0x0010FC0E File Offset: 0x0010DE0E
		public Certificate(X509CertificateStructure[] certificateList)
		{
			if (certificateList == null)
			{
				throw new ArgumentNullException("certificateList");
			}
			this.mCertificateList = certificateList;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0010FC2B File Offset: 0x0010DE2B
		public virtual X509CertificateStructure[] GetCertificateList()
		{
			return this.CloneCertificateList();
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0010FC33 File Offset: 0x0010DE33
		public virtual X509CertificateStructure GetCertificateAt(int index)
		{
			return this.mCertificateList[index];
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x0010FC3D File Offset: 0x0010DE3D
		public virtual int Length
		{
			get
			{
				return this.mCertificateList.Length;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x0010FC47 File Offset: 0x0010DE47
		public virtual bool IsEmpty
		{
			get
			{
				return this.mCertificateList.Length == 0;
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0010FC54 File Offset: 0x0010DE54
		public virtual void Encode(Stream output)
		{
			IList list = Platform.CreateArrayList(this.mCertificateList.Length);
			int num = 0;
			X509CertificateStructure[] array = this.mCertificateList;
			for (int i = 0; i < array.Length; i++)
			{
				byte[] encoded = array[i].GetEncoded("DER");
				list.Add(encoded);
				num += encoded.Length + 3;
			}
			TlsUtilities.CheckUint24(num);
			TlsUtilities.WriteUint24(num, output);
			foreach (object obj in list)
			{
				TlsUtilities.WriteOpaque24((byte[])obj, output);
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0010FD00 File Offset: 0x0010DF00
		public static Certificate Parse(Stream input)
		{
			int num = TlsUtilities.ReadUint24(input);
			if (num == 0)
			{
				return Certificate.EmptyChain;
			}
			MemoryStream memoryStream = new MemoryStream(TlsUtilities.ReadFully(num, input), false);
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				Asn1Object obj = TlsUtilities.ReadAsn1Object(TlsUtilities.ReadOpaque24(memoryStream));
				list.Add(X509CertificateStructure.GetInstance(obj));
			}
			X509CertificateStructure[] array = new X509CertificateStructure[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = (X509CertificateStructure)list[i];
			}
			return new Certificate(array);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x0010FD94 File Offset: 0x0010DF94
		protected virtual X509CertificateStructure[] CloneCertificateList()
		{
			return (X509CertificateStructure[])this.mCertificateList.Clone();
		}

		// Token: 0x04001A2B RID: 6699
		public static readonly Certificate EmptyChain = new Certificate(new X509CertificateStructure[0]);

		// Token: 0x04001A2C RID: 6700
		protected readonly X509CertificateStructure[] mCertificateList;
	}
}
