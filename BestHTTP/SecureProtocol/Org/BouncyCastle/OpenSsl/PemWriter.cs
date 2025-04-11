using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.OpenSsl
{
	// Token: 0x020002BF RID: 703
	public class PemWriter : PemWriter
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x000C92DE File Offset: 0x000C74DE
		public PemWriter(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000C92E8 File Offset: 0x000C74E8
		public void WriteObject(object obj)
		{
			try
			{
				base.WriteObject(new MiscPemGenerator(obj));
			}
			catch (PemGenerationException ex)
			{
				if (ex.InnerException is IOException)
				{
					throw (IOException)ex.InnerException;
				}
				throw ex;
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000C9330 File Offset: 0x000C7530
		public void WriteObject(object obj, string algorithm, char[] password, SecureRandom random)
		{
			base.WriteObject(new MiscPemGenerator(obj, algorithm, password, random));
		}
	}
}
