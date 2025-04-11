using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020006B4 RID: 1716
	public class NameOrPseudonym : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003FAC RID: 16300 RVA: 0x0017EE24 File Offset: 0x0017D024
		public static NameOrPseudonym GetInstance(object obj)
		{
			if (obj == null || obj is NameOrPseudonym)
			{
				return (NameOrPseudonym)obj;
			}
			if (obj is IAsn1String)
			{
				return new NameOrPseudonym(DirectoryString.GetInstance(obj));
			}
			if (obj is Asn1Sequence)
			{
				return new NameOrPseudonym((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0017EE85 File Offset: 0x0017D085
		public NameOrPseudonym(DirectoryString pseudonym)
		{
			this.pseudonym = pseudonym;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0017EE94 File Offset: 0x0017D094
		private NameOrPseudonym(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (!(seq[0] is IAsn1String))
			{
				throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(seq[0]));
			}
			this.surname = DirectoryString.GetInstance(seq[0]);
			this.givenName = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0017EF19 File Offset: 0x0017D119
		public NameOrPseudonym(string pseudonym) : this(new DirectoryString(pseudonym))
		{
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0017EF27 File Offset: 0x0017D127
		public NameOrPseudonym(DirectoryString surname, Asn1Sequence givenName)
		{
			this.surname = surname;
			this.givenName = givenName;
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x0017EF3D File Offset: 0x0017D13D
		public DirectoryString Pseudonym
		{
			get
			{
				return this.pseudonym;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x0017EF45 File Offset: 0x0017D145
		public DirectoryString Surname
		{
			get
			{
				return this.surname;
			}
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x0017EF50 File Offset: 0x0017D150
		public DirectoryString[] GetGivenName()
		{
			DirectoryString[] array = new DirectoryString[this.givenName.Count];
			int num = 0;
			foreach (object obj in this.givenName)
			{
				array[num++] = DirectoryString.GetInstance(obj);
			}
			return array;
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x0017EFC4 File Offset: 0x0017D1C4
		public override Asn1Object ToAsn1Object()
		{
			if (this.pseudonym != null)
			{
				return this.pseudonym.ToAsn1Object();
			}
			return new DerSequence(new Asn1Encodable[]
			{
				this.surname,
				this.givenName
			});
		}

		// Token: 0x04002792 RID: 10130
		private readonly DirectoryString pseudonym;

		// Token: 0x04002793 RID: 10131
		private readonly DirectoryString surname;

		// Token: 0x04002794 RID: 10132
		private readonly Asn1Sequence givenName;
	}
}
