using System;
using System.Collections;
using System.Collections.Generic;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003DC RID: 988
	public abstract class AbstractTlsClient : AbstractTlsPeer, TlsClient, TlsPeer
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x0010EA68 File Offset: 0x0010CC68
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x0010EA70 File Offset: 0x0010CC70
		public List<string> HostNames { get; set; }

		// Token: 0x06002864 RID: 10340 RVA: 0x0010EA79 File Offset: 0x0010CC79
		public AbstractTlsClient() : this(new DefaultTlsCipherFactory())
		{
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0010EA86 File Offset: 0x0010CC86
		public AbstractTlsClient(TlsCipherFactory cipherFactory)
		{
			this.mCipherFactory = cipherFactory;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0010EA95 File Offset: 0x0010CC95
		protected virtual bool AllowUnexpectedServerExtension(int extensionType, byte[] extensionData)
		{
			if (extensionType == 10)
			{
				TlsEccUtilities.ReadSupportedEllipticCurvesExtension(extensionData);
				return true;
			}
			if (extensionType != 11)
			{
				return false;
			}
			TlsEccUtilities.ReadSupportedPointFormatsExtension(extensionData);
			return true;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0010EAB8 File Offset: 0x0010CCB8
		protected virtual void CheckForUnexpectedServerExtension(IDictionary serverExtensions, int extensionType)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(serverExtensions, extensionType);
			if (extensionData != null && !this.AllowUnexpectedServerExtension(extensionType, extensionData))
			{
				throw new TlsFatalAlert(47);
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0010EAE2 File Offset: 0x0010CCE2
		public virtual void Init(TlsClientContext context)
		{
			this.mContext = context;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public virtual TlsSession GetSessionToResume()
		{
			return null;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x0010EAEB File Offset: 0x0010CCEB
		public virtual ProtocolVersion ClientHelloRecordLayerVersion
		{
			get
			{
				return this.ClientVersion;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x0010EAF3 File Offset: 0x0010CCF3
		public virtual ProtocolVersion ClientVersion
		{
			get
			{
				return ProtocolVersion.TLSv12;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsFallback
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0010EAFC File Offset: 0x0010CCFC
		public virtual IDictionary GetClientExtensions()
		{
			IDictionary dictionary = null;
			if (TlsUtilities.IsSignatureAlgorithmsExtensionAllowed(this.mContext.ClientVersion))
			{
				this.mSupportedSignatureAlgorithms = TlsUtilities.GetDefaultSupportedSignatureAlgorithms();
				dictionary = TlsExtensionsUtilities.EnsureExtensionsInitialised(dictionary);
				TlsUtilities.AddSignatureAlgorithmsExtension(dictionary, this.mSupportedSignatureAlgorithms);
			}
			if (TlsEccUtilities.ContainsEccCipherSuites(this.GetCipherSuites()))
			{
				this.mNamedCurves = new int[]
				{
					23,
					24
				};
				this.mClientECPointFormats = new byte[]
				{
					0,
					1,
					2
				};
				dictionary = TlsExtensionsUtilities.EnsureExtensionsInitialised(dictionary);
				TlsEccUtilities.AddSupportedEllipticCurvesExtension(dictionary, this.mNamedCurves);
				TlsEccUtilities.AddSupportedPointFormatsExtension(dictionary, this.mClientECPointFormats);
			}
			if (this.HostNames != null && this.HostNames.Count > 0)
			{
				List<ServerName> list = new List<ServerName>(this.HostNames.Count);
				for (int i = 0; i < this.HostNames.Count; i++)
				{
					list.Add(new ServerName(0, this.HostNames[i]));
				}
				TlsExtensionsUtilities.AddServerNameExtension(dictionary, new ServerNameList(list));
			}
			return dictionary;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x0010EBF3 File Offset: 0x0010CDF3
		public virtual ProtocolVersion MinimumVersion
		{
			get
			{
				return ProtocolVersion.TLSv10;
			}
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0010EBFA File Offset: 0x0010CDFA
		public virtual void NotifyServerVersion(ProtocolVersion serverVersion)
		{
			if (!this.MinimumVersion.IsEqualOrEarlierVersionOf(serverVersion))
			{
				throw new TlsFatalAlert(70);
			}
		}

		// Token: 0x06002870 RID: 10352
		public abstract int[] GetCipherSuites();

		// Token: 0x06002871 RID: 10353 RVA: 0x0010EC12 File Offset: 0x0010CE12
		public virtual byte[] GetCompressionMethods()
		{
			return new byte[1];
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void NotifySessionID(byte[] sessionID)
		{
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x0010EC1A File Offset: 0x0010CE1A
		public virtual void NotifySelectedCipherSuite(int selectedCipherSuite)
		{
			this.mSelectedCipherSuite = selectedCipherSuite;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x0010EC23 File Offset: 0x0010CE23
		public virtual void NotifySelectedCompressionMethod(byte selectedCompressionMethod)
		{
			this.mSelectedCompressionMethod = (short)selectedCompressionMethod;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x0010EC2C File Offset: 0x0010CE2C
		public virtual void ProcessServerExtensions(IDictionary serverExtensions)
		{
			if (serverExtensions != null)
			{
				this.CheckForUnexpectedServerExtension(serverExtensions, 13);
				this.CheckForUnexpectedServerExtension(serverExtensions, 10);
				if (TlsEccUtilities.IsEccCipherSuite(this.mSelectedCipherSuite))
				{
					this.mServerECPointFormats = TlsEccUtilities.GetSupportedPointFormatsExtension(serverExtensions);
				}
				else
				{
					this.CheckForUnexpectedServerExtension(serverExtensions, 11);
				}
				this.CheckForUnexpectedServerExtension(serverExtensions, 21);
			}
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0010EC7B File Offset: 0x0010CE7B
		public virtual void ProcessServerSupplementalData(IList serverSupplementalData)
		{
			if (serverSupplementalData != null)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002877 RID: 10359
		public abstract TlsKeyExchange GetKeyExchange();

		// Token: 0x06002878 RID: 10360
		public abstract TlsAuthentication GetAuthentication();

		// Token: 0x06002879 RID: 10361 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public virtual IList GetClientSupplementalData()
		{
			return null;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0010EC88 File Offset: 0x0010CE88
		public override TlsCompression GetCompression()
		{
			short num = this.mSelectedCompressionMethod;
			if (num == 0)
			{
				return new TlsNullCompression();
			}
			if (num != 1)
			{
				throw new TlsFatalAlert(80);
			}
			return new TlsDeflateCompression();
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x0010ECB8 File Offset: 0x0010CEB8
		public override TlsCipher GetCipher()
		{
			int encryptionAlgorithm = TlsUtilities.GetEncryptionAlgorithm(this.mSelectedCipherSuite);
			int macAlgorithm = TlsUtilities.GetMacAlgorithm(this.mSelectedCipherSuite);
			return this.mCipherFactory.CreateCipher(this.mContext, encryptionAlgorithm, macAlgorithm);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void NotifyNewSessionTicket(NewSessionTicket newSessionTicket)
		{
		}

		// Token: 0x040019D1 RID: 6609
		protected TlsCipherFactory mCipherFactory;

		// Token: 0x040019D2 RID: 6610
		protected TlsClientContext mContext;

		// Token: 0x040019D3 RID: 6611
		protected IList mSupportedSignatureAlgorithms;

		// Token: 0x040019D4 RID: 6612
		protected int[] mNamedCurves;

		// Token: 0x040019D5 RID: 6613
		protected byte[] mClientECPointFormats;

		// Token: 0x040019D6 RID: 6614
		protected byte[] mServerECPointFormats;

		// Token: 0x040019D7 RID: 6615
		protected int mSelectedCipherSuite;

		// Token: 0x040019D8 RID: 6616
		protected short mSelectedCompressionMethod;
	}
}
