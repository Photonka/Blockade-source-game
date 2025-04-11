using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E2 RID: 994
	public abstract class AbstractTlsServer : AbstractTlsPeer, TlsServer, TlsPeer
	{
		// Token: 0x060028AB RID: 10411 RVA: 0x0010F04D File Offset: 0x0010D24D
		public AbstractTlsServer() : this(new DefaultTlsCipherFactory())
		{
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0010F05A File Offset: 0x0010D25A
		public AbstractTlsServer(TlsCipherFactory cipherFactory)
		{
			this.mCipherFactory = cipherFactory;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0006CF70 File Offset: 0x0006B170
		protected virtual bool AllowEncryptThenMac
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		protected virtual bool AllowTruncatedHMac
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0010F06C File Offset: 0x0010D26C
		protected virtual IDictionary CheckServerExtensions()
		{
			return this.mServerExtensions = TlsExtensionsUtilities.EnsureExtensionsInitialised(this.mServerExtensions);
		}

		// Token: 0x060028B0 RID: 10416
		protected abstract int[] GetCipherSuites();

		// Token: 0x060028B1 RID: 10417 RVA: 0x0010EC12 File Offset: 0x0010CE12
		protected byte[] GetCompressionMethods()
		{
			return new byte[1];
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0010F08D File Offset: 0x0010D28D
		protected virtual ProtocolVersion MaximumVersion
		{
			get
			{
				return ProtocolVersion.TLSv11;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0010EBF3 File Offset: 0x0010CDF3
		protected virtual ProtocolVersion MinimumVersion
		{
			get
			{
				return ProtocolVersion.TLSv10;
			}
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0010F094 File Offset: 0x0010D294
		protected virtual bool SupportsClientEccCapabilities(int[] namedCurves, byte[] ecPointFormats)
		{
			if (namedCurves == null)
			{
				return TlsEccUtilities.HasAnySupportedNamedCurves();
			}
			foreach (int namedCurve in namedCurves)
			{
				if (NamedCurve.IsValid(namedCurve) && (!NamedCurve.RefersToASpecificNamedCurve(namedCurve) || TlsEccUtilities.IsSupportedNamedCurve(namedCurve)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0010F0D7 File Offset: 0x0010D2D7
		public virtual void Init(TlsServerContext context)
		{
			this.mContext = context;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0010F0E0 File Offset: 0x0010D2E0
		public virtual void NotifyClientVersion(ProtocolVersion clientVersion)
		{
			this.mClientVersion = clientVersion;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x0010F0E9 File Offset: 0x0010D2E9
		public virtual void NotifyFallback(bool isFallback)
		{
			if (isFallback && this.MaximumVersion.IsLaterVersionOf(this.mClientVersion))
			{
				throw new TlsFatalAlert(86);
			}
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0010F109 File Offset: 0x0010D309
		public virtual void NotifyOfferedCipherSuites(int[] offeredCipherSuites)
		{
			this.mOfferedCipherSuites = offeredCipherSuites;
			this.mEccCipherSuitesOffered = TlsEccUtilities.ContainsEccCipherSuites(this.mOfferedCipherSuites);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0010F123 File Offset: 0x0010D323
		public virtual void NotifyOfferedCompressionMethods(byte[] offeredCompressionMethods)
		{
			this.mOfferedCompressionMethods = offeredCompressionMethods;
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0010F12C File Offset: 0x0010D32C
		public virtual void ProcessClientExtensions(IDictionary clientExtensions)
		{
			this.mClientExtensions = clientExtensions;
			if (clientExtensions != null)
			{
				this.mEncryptThenMacOffered = TlsExtensionsUtilities.HasEncryptThenMacExtension(clientExtensions);
				this.mMaxFragmentLengthOffered = TlsExtensionsUtilities.GetMaxFragmentLengthExtension(clientExtensions);
				if (this.mMaxFragmentLengthOffered >= 0 && !MaxFragmentLength.IsValid((byte)this.mMaxFragmentLengthOffered))
				{
					throw new TlsFatalAlert(47);
				}
				this.mTruncatedHMacOffered = TlsExtensionsUtilities.HasTruncatedHMacExtension(clientExtensions);
				this.mSupportedSignatureAlgorithms = TlsUtilities.GetSignatureAlgorithmsExtension(clientExtensions);
				if (this.mSupportedSignatureAlgorithms != null && !TlsUtilities.IsSignatureAlgorithmsExtensionAllowed(this.mClientVersion))
				{
					throw new TlsFatalAlert(47);
				}
				this.mNamedCurves = TlsEccUtilities.GetSupportedEllipticCurvesExtension(clientExtensions);
				this.mClientECPointFormats = TlsEccUtilities.GetSupportedPointFormatsExtension(clientExtensions);
			}
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0010F1CC File Offset: 0x0010D3CC
		public virtual ProtocolVersion GetServerVersion()
		{
			if (this.MinimumVersion.IsEqualOrEarlierVersionOf(this.mClientVersion))
			{
				ProtocolVersion maximumVersion = this.MaximumVersion;
				if (this.mClientVersion.IsEqualOrEarlierVersionOf(maximumVersion))
				{
					return this.mServerVersion = this.mClientVersion;
				}
				if (this.mClientVersion.IsLaterVersionOf(maximumVersion))
				{
					return this.mServerVersion = maximumVersion;
				}
			}
			throw new TlsFatalAlert(70);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0010F234 File Offset: 0x0010D434
		public virtual int GetSelectedCipherSuite()
		{
			IList usableSignatureAlgorithms = TlsUtilities.GetUsableSignatureAlgorithms(this.mSupportedSignatureAlgorithms);
			bool flag = this.SupportsClientEccCapabilities(this.mNamedCurves, this.mClientECPointFormats);
			foreach (int num in this.GetCipherSuites())
			{
				if (Arrays.Contains(this.mOfferedCipherSuites, num) && (flag || !TlsEccUtilities.IsEccCipherSuite(num)) && TlsUtilities.IsValidCipherSuiteForVersion(num, this.mServerVersion) && TlsUtilities.IsValidCipherSuiteForSignatureAlgorithms(num, usableSignatureAlgorithms))
				{
					return this.mSelectedCipherSuite = num;
				}
			}
			throw new TlsFatalAlert(40);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0010F2C4 File Offset: 0x0010D4C4
		public virtual byte GetSelectedCompressionMethod()
		{
			byte[] compressionMethods = this.GetCompressionMethods();
			for (int i = 0; i < compressionMethods.Length; i++)
			{
				if (Arrays.Contains(this.mOfferedCompressionMethods, compressionMethods[i]))
				{
					return this.mSelectedCompressionMethod = compressionMethods[i];
				}
			}
			throw new TlsFatalAlert(40);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0010F30C File Offset: 0x0010D50C
		public virtual IDictionary GetServerExtensions()
		{
			if (this.mEncryptThenMacOffered && this.AllowEncryptThenMac && TlsUtilities.IsBlockCipherSuite(this.mSelectedCipherSuite))
			{
				TlsExtensionsUtilities.AddEncryptThenMacExtension(this.CheckServerExtensions());
			}
			if (this.mMaxFragmentLengthOffered >= 0 && TlsUtilities.IsValidUint8((int)this.mMaxFragmentLengthOffered) && MaxFragmentLength.IsValid((byte)this.mMaxFragmentLengthOffered))
			{
				TlsExtensionsUtilities.AddMaxFragmentLengthExtension(this.CheckServerExtensions(), (byte)this.mMaxFragmentLengthOffered);
			}
			if (this.mTruncatedHMacOffered && this.AllowTruncatedHMac)
			{
				TlsExtensionsUtilities.AddTruncatedHMacExtension(this.CheckServerExtensions());
			}
			if (this.mClientECPointFormats != null && TlsEccUtilities.IsEccCipherSuite(this.mSelectedCipherSuite))
			{
				this.mServerECPointFormats = new byte[]
				{
					0,
					1,
					2
				};
				TlsEccUtilities.AddSupportedPointFormatsExtension(this.CheckServerExtensions(), this.mServerECPointFormats);
			}
			return this.mServerExtensions;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public virtual IList GetServerSupplementalData()
		{
			return null;
		}

		// Token: 0x060028C0 RID: 10432
		public abstract TlsCredentials GetCredentials();

		// Token: 0x060028C1 RID: 10433 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public virtual CertificateStatus GetCertificateStatus()
		{
			return null;
		}

		// Token: 0x060028C2 RID: 10434
		public abstract TlsKeyExchange GetKeyExchange();

		// Token: 0x060028C3 RID: 10435 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public virtual CertificateRequest GetCertificateRequest()
		{
			return null;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0010EC7B File Offset: 0x0010CE7B
		public virtual void ProcessClientSupplementalData(IList clientSupplementalData)
		{
			if (clientSupplementalData != null)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public virtual void NotifyClientCertificate(Certificate clientCertificate)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0010F3D4 File Offset: 0x0010D5D4
		public override TlsCompression GetCompression()
		{
			if (this.mSelectedCompressionMethod == 0)
			{
				return new TlsNullCompression();
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0010F3F8 File Offset: 0x0010D5F8
		public override TlsCipher GetCipher()
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(this.mSelectedCipherSuite);
			int macAlgorithm = TlsUtilities.GetMacAlgorithm(this.mSelectedCipherSuite);
			return this.mCipherFactory.CreateCipher(this.mContext, encryptionAlgorithm, macAlgorithm);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0010F430 File Offset: 0x0010D630
		public virtual NewSessionTicket GetNewSessionTicket()
		{
			return new NewSessionTicket(0L, TlsUtilities.EmptyBytes);
		}

		// Token: 0x040019E5 RID: 6629
		protected TlsCipherFactory mCipherFactory;

		// Token: 0x040019E6 RID: 6630
		protected TlsServerContext mContext;

		// Token: 0x040019E7 RID: 6631
		protected ProtocolVersion mClientVersion;

		// Token: 0x040019E8 RID: 6632
		protected int[] mOfferedCipherSuites;

		// Token: 0x040019E9 RID: 6633
		protected byte[] mOfferedCompressionMethods;

		// Token: 0x040019EA RID: 6634
		protected IDictionary mClientExtensions;

		// Token: 0x040019EB RID: 6635
		protected bool mEncryptThenMacOffered;

		// Token: 0x040019EC RID: 6636
		protected short mMaxFragmentLengthOffered;

		// Token: 0x040019ED RID: 6637
		protected bool mTruncatedHMacOffered;

		// Token: 0x040019EE RID: 6638
		protected IList mSupportedSignatureAlgorithms;

		// Token: 0x040019EF RID: 6639
		protected bool mEccCipherSuitesOffered;

		// Token: 0x040019F0 RID: 6640
		protected int[] mNamedCurves;

		// Token: 0x040019F1 RID: 6641
		protected byte[] mClientECPointFormats;

		// Token: 0x040019F2 RID: 6642
		protected byte[] mServerECPointFormats;

		// Token: 0x040019F3 RID: 6643
		protected ProtocolVersion mServerVersion;

		// Token: 0x040019F4 RID: 6644
		protected int mSelectedCipherSuite;

		// Token: 0x040019F5 RID: 6645
		protected byte mSelectedCompressionMethod;

		// Token: 0x040019F6 RID: 6646
		protected IDictionary mServerExtensions;
	}
}
