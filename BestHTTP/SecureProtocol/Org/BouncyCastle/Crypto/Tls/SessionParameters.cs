using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000431 RID: 1073
	public sealed class SessionParameters
	{
		// Token: 0x06002AB4 RID: 10932 RVA: 0x00115B24 File Offset: 0x00113D24
		private SessionParameters(int cipherSuite, byte compressionAlgorithm, byte[] masterSecret, Certificate peerCertificate, byte[] pskIdentity, byte[] srpIdentity, byte[] encodedServerExtensions, bool extendedMasterSecret)
		{
			this.mCipherSuite = cipherSuite;
			this.mCompressionAlgorithm = compressionAlgorithm;
			this.mMasterSecret = Arrays.Clone(masterSecret);
			this.mPeerCertificate = peerCertificate;
			this.mPskIdentity = Arrays.Clone(pskIdentity);
			this.mSrpIdentity = Arrays.Clone(srpIdentity);
			this.mEncodedServerExtensions = encodedServerExtensions;
			this.mExtendedMasterSecret = extendedMasterSecret;
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00115B83 File Offset: 0x00113D83
		public void Clear()
		{
			if (this.mMasterSecret != null)
			{
				Arrays.Fill(this.mMasterSecret, 0);
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x00115B99 File Offset: 0x00113D99
		public SessionParameters Copy()
		{
			return new SessionParameters(this.mCipherSuite, this.mCompressionAlgorithm, this.mMasterSecret, this.mPeerCertificate, this.mPskIdentity, this.mSrpIdentity, this.mEncodedServerExtensions, this.mExtendedMasterSecret);
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00115BD0 File Offset: 0x00113DD0
		public int CipherSuite
		{
			get
			{
				return this.mCipherSuite;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x00115BD8 File Offset: 0x00113DD8
		public byte CompressionAlgorithm
		{
			get
			{
				return this.mCompressionAlgorithm;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x00115BE0 File Offset: 0x00113DE0
		public bool IsExtendedMasterSecret
		{
			get
			{
				return this.mExtendedMasterSecret;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x00115BE8 File Offset: 0x00113DE8
		public byte[] MasterSecret
		{
			get
			{
				return this.mMasterSecret;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00115BF0 File Offset: 0x00113DF0
		public Certificate PeerCertificate
		{
			get
			{
				return this.mPeerCertificate;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00115BF8 File Offset: 0x00113DF8
		public byte[] PskIdentity
		{
			get
			{
				return this.mPskIdentity;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x00115C00 File Offset: 0x00113E00
		public byte[] SrpIdentity
		{
			get
			{
				return this.mSrpIdentity;
			}
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00115C08 File Offset: 0x00113E08
		public IDictionary ReadServerExtensions()
		{
			if (this.mEncodedServerExtensions == null)
			{
				return null;
			}
			return TlsProtocol.ReadExtensions(new MemoryStream(this.mEncodedServerExtensions, false));
		}

		// Token: 0x04001CA1 RID: 7329
		private int mCipherSuite;

		// Token: 0x04001CA2 RID: 7330
		private byte mCompressionAlgorithm;

		// Token: 0x04001CA3 RID: 7331
		private byte[] mMasterSecret;

		// Token: 0x04001CA4 RID: 7332
		private Certificate mPeerCertificate;

		// Token: 0x04001CA5 RID: 7333
		private byte[] mPskIdentity;

		// Token: 0x04001CA6 RID: 7334
		private byte[] mSrpIdentity;

		// Token: 0x04001CA7 RID: 7335
		private byte[] mEncodedServerExtensions;

		// Token: 0x04001CA8 RID: 7336
		private bool mExtendedMasterSecret;

		// Token: 0x02000921 RID: 2337
		public sealed class Builder
		{
			// Token: 0x06004E1F RID: 19999 RVA: 0x001B3154 File Offset: 0x001B1354
			public SessionParameters Build()
			{
				this.Validate(this.mCipherSuite >= 0, "cipherSuite");
				this.Validate(this.mCompressionAlgorithm >= 0, "compressionAlgorithm");
				this.Validate(this.mMasterSecret != null, "masterSecret");
				return new SessionParameters(this.mCipherSuite, (byte)this.mCompressionAlgorithm, this.mMasterSecret, this.mPeerCertificate, this.mPskIdentity, this.mSrpIdentity, this.mEncodedServerExtensions, this.mExtendedMasterSecret);
			}

			// Token: 0x06004E20 RID: 20000 RVA: 0x001B31D9 File Offset: 0x001B13D9
			public SessionParameters.Builder SetCipherSuite(int cipherSuite)
			{
				this.mCipherSuite = cipherSuite;
				return this;
			}

			// Token: 0x06004E21 RID: 20001 RVA: 0x001B31E3 File Offset: 0x001B13E3
			public SessionParameters.Builder SetCompressionAlgorithm(byte compressionAlgorithm)
			{
				this.mCompressionAlgorithm = (short)compressionAlgorithm;
				return this;
			}

			// Token: 0x06004E22 RID: 20002 RVA: 0x001B31ED File Offset: 0x001B13ED
			public SessionParameters.Builder SetExtendedMasterSecret(bool extendedMasterSecret)
			{
				this.mExtendedMasterSecret = extendedMasterSecret;
				return this;
			}

			// Token: 0x06004E23 RID: 20003 RVA: 0x001B31F7 File Offset: 0x001B13F7
			public SessionParameters.Builder SetMasterSecret(byte[] masterSecret)
			{
				this.mMasterSecret = masterSecret;
				return this;
			}

			// Token: 0x06004E24 RID: 20004 RVA: 0x001B3201 File Offset: 0x001B1401
			public SessionParameters.Builder SetPeerCertificate(Certificate peerCertificate)
			{
				this.mPeerCertificate = peerCertificate;
				return this;
			}

			// Token: 0x06004E25 RID: 20005 RVA: 0x001B320B File Offset: 0x001B140B
			public SessionParameters.Builder SetPskIdentity(byte[] pskIdentity)
			{
				this.mPskIdentity = pskIdentity;
				return this;
			}

			// Token: 0x06004E26 RID: 20006 RVA: 0x001B3215 File Offset: 0x001B1415
			public SessionParameters.Builder SetSrpIdentity(byte[] srpIdentity)
			{
				this.mSrpIdentity = srpIdentity;
				return this;
			}

			// Token: 0x06004E27 RID: 20007 RVA: 0x001B3220 File Offset: 0x001B1420
			public SessionParameters.Builder SetServerExtensions(IDictionary serverExtensions)
			{
				if (serverExtensions == null)
				{
					this.mEncodedServerExtensions = null;
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					TlsProtocol.WriteExtensions(memoryStream, serverExtensions);
					this.mEncodedServerExtensions = memoryStream.ToArray();
				}
				return this;
			}

			// Token: 0x06004E28 RID: 20008 RVA: 0x001B3253 File Offset: 0x001B1453
			private void Validate(bool condition, string parameter)
			{
				if (!condition)
				{
					throw new InvalidOperationException("Required session parameter '" + parameter + "' not configured");
				}
			}

			// Token: 0x040034E8 RID: 13544
			private int mCipherSuite = -1;

			// Token: 0x040034E9 RID: 13545
			private short mCompressionAlgorithm = -1;

			// Token: 0x040034EA RID: 13546
			private byte[] mMasterSecret;

			// Token: 0x040034EB RID: 13547
			private Certificate mPeerCertificate;

			// Token: 0x040034EC RID: 13548
			private byte[] mPskIdentity;

			// Token: 0x040034ED RID: 13549
			private byte[] mSrpIdentity;

			// Token: 0x040034EE RID: 13550
			private byte[] mEncodedServerExtensions;

			// Token: 0x040034EF RID: 13551
			private bool mExtendedMasterSecret;
		}
	}
}
