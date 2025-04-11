using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x02000271 RID: 625
	public class PemWriter
	{
		// Token: 0x06001743 RID: 5955 RVA: 0x000B9C9A File Offset: 0x000B7E9A
		public PemWriter(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.nlLength = Platform.NewLine.Length;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x000B9CD4 File Offset: 0x000B7ED4
		public TextWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000B9CDC File Offset: 0x000B7EDC
		public int GetOutputSize(PemObject obj)
		{
			int num = 2 * (obj.Type.Length + 10 + this.nlLength) + 6 + 4;
			if (obj.Headers.Count > 0)
			{
				foreach (object obj2 in obj.Headers)
				{
					PemHeader pemHeader = (PemHeader)obj2;
					num += pemHeader.Name.Length + ": ".Length + pemHeader.Value.Length + this.nlLength;
				}
				num += this.nlLength;
			}
			int num2 = (obj.Content.Length + 2) / 3 * 4;
			num += num2 + (num2 + 64 - 1) / 64 * this.nlLength;
			return num;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000B9DB8 File Offset: 0x000B7FB8
		public void WriteObject(PemObjectGenerator objGen)
		{
			PemObject pemObject = objGen.Generate();
			this.WritePreEncapsulationBoundary(pemObject.Type);
			if (pemObject.Headers.Count > 0)
			{
				foreach (object obj in pemObject.Headers)
				{
					PemHeader pemHeader = (PemHeader)obj;
					this.writer.Write(pemHeader.Name);
					this.writer.Write(": ");
					this.writer.WriteLine(pemHeader.Value);
				}
				this.writer.WriteLine();
			}
			this.WriteEncoded(pemObject.Content);
			this.WritePostEncapsulationBoundary(pemObject.Type);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000B9E80 File Offset: 0x000B8080
		private void WriteEncoded(byte[] bytes)
		{
			bytes = Base64.Encode(bytes);
			for (int i = 0; i < bytes.Length; i += this.buf.Length)
			{
				int num = 0;
				while (num != this.buf.Length && i + num < bytes.Length)
				{
					this.buf[num] = (char)bytes[i + num];
					num++;
				}
				this.writer.WriteLine(this.buf, 0, num);
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000B9EE5 File Offset: 0x000B80E5
		private void WritePreEncapsulationBoundary(string type)
		{
			this.writer.WriteLine("-----BEGIN " + type + "-----");
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000B9F02 File Offset: 0x000B8102
		private void WritePostEncapsulationBoundary(string type)
		{
			this.writer.WriteLine("-----END " + type + "-----");
		}

		// Token: 0x040016DA RID: 5850
		private const int LineLength = 64;

		// Token: 0x040016DB RID: 5851
		private readonly TextWriter writer;

		// Token: 0x040016DC RID: 5852
		private readonly int nlLength;

		// Token: 0x040016DD RID: 5853
		private char[] buf = new char[64];
	}
}
