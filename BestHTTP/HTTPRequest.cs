using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using BestHTTP.Authentication;
using BestHTTP.Cookies;
using BestHTTP.Extensions;
using BestHTTP.Forms;
using BestHTTP.Logger;
using Org.BouncyCastle.Crypto.Tls;

namespace BestHTTP
{
	// Token: 0x0200017F RID: 383
	public sealed class HTTPRequest : IEnumerator, IEnumerator<HTTPRequest>, IDisposable
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000956B1 File Offset: 0x000938B1
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x000956B9 File Offset: 0x000938B9
		public Uri Uri { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000956C2 File Offset: 0x000938C2
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x000956CA File Offset: 0x000938CA
		public HTTPMethods MethodType { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000956D3 File Offset: 0x000938D3
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x000956DB File Offset: 0x000938DB
		public byte[] RawData { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x000956E4 File Offset: 0x000938E4
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x000956EC File Offset: 0x000938EC
		public Stream UploadStream { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x000956F5 File Offset: 0x000938F5
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x000956FD File Offset: 0x000938FD
		public bool DisposeUploadStream { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00095706 File Offset: 0x00093906
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0009570E File Offset: 0x0009390E
		public bool UseUploadStreamLength { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00095717 File Offset: 0x00093917
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0009571F File Offset: 0x0009391F
		public bool IsKeepAlive
		{
			get
			{
				return this.isKeepAlive;
			}
			set
			{
				if (this.State == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the IsKeepAlive property while processing the request is not supported.");
				}
				this.isKeepAlive = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0009573C File Offset: 0x0009393C
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00095744 File Offset: 0x00093944
		public bool DisableCache
		{
			get
			{
				return this.disableCache;
			}
			set
			{
				if (this.State == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the DisableCache property while processing the request is not supported.");
				}
				this.disableCache = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00095761 File Offset: 0x00093961
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00095769 File Offset: 0x00093969
		public bool CacheOnly
		{
			get
			{
				return this.cacheOnly;
			}
			set
			{
				if (this.State == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the CacheOnly property while processing the request is not supported.");
				}
				this.cacheOnly = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00095786 File Offset: 0x00093986
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0009578E File Offset: 0x0009398E
		public bool UseStreaming
		{
			get
			{
				return this.useStreaming;
			}
			set
			{
				if (this.State == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the UseStreaming property while processing the request is not supported.");
				}
				this.useStreaming = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000957AB File Offset: 0x000939AB
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x000957B3 File Offset: 0x000939B3
		public int StreamFragmentSize
		{
			get
			{
				return this.streamFragmentSize;
			}
			set
			{
				if (this.State == HTTPRequestStates.Processing)
				{
					throw new NotSupportedException("Changing the StreamFragmentSize property while processing the request is not supported.");
				}
				if (value < 1)
				{
					throw new ArgumentException("StreamFragmentSize must be at least 1.");
				}
				this.streamFragmentSize = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000957DF File Offset: 0x000939DF
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x000957E7 File Offset: 0x000939E7
		public int MaxFragmentQueueLength { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000957F0 File Offset: 0x000939F0
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000957F8 File Offset: 0x000939F8
		public OnRequestFinishedDelegate Callback { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00095801 File Offset: 0x00093A01
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00095809 File Offset: 0x00093A09
		public bool DisableRetry { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00095812 File Offset: 0x00093A12
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0009581A File Offset: 0x00093A1A
		public bool IsRedirected { get; internal set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00095823 File Offset: 0x00093A23
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0009582B File Offset: 0x00093A2B
		public Uri RedirectUri { get; internal set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00095834 File Offset: 0x00093A34
		public Uri CurrentUri
		{
			get
			{
				if (!this.IsRedirected)
				{
					return this.Uri;
				}
				return this.RedirectUri;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0009584B File Offset: 0x00093A4B
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00095853 File Offset: 0x00093A53
		public HTTPResponse Response { get; internal set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0009585C File Offset: 0x00093A5C
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x00095864 File Offset: 0x00093A64
		public HTTPResponse ProxyResponse { get; internal set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0009586D File Offset: 0x00093A6D
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x00095875 File Offset: 0x00093A75
		public Exception Exception { get; internal set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0009587E File Offset: 0x00093A7E
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x00095886 File Offset: 0x00093A86
		public object Tag { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0009588F File Offset: 0x00093A8F
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x00095897 File Offset: 0x00093A97
		public Credentials Credentials { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000958A0 File Offset: 0x00093AA0
		public bool HasProxy
		{
			get
			{
				return this.Proxy != null;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000958AB File Offset: 0x00093AAB
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x000958B3 File Offset: 0x00093AB3
		public Proxy Proxy { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000958BC File Offset: 0x00093ABC
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x000958C4 File Offset: 0x00093AC4
		public int MaxRedirects { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000958CD File Offset: 0x00093ACD
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x000958D5 File Offset: 0x00093AD5
		public bool UseAlternateSSL { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000958DE File Offset: 0x00093ADE
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x000958E6 File Offset: 0x00093AE6
		public bool IsCookiesEnabled { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000958EF File Offset: 0x00093AEF
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0009590A File Offset: 0x00093B0A
		public List<Cookie> Cookies
		{
			get
			{
				if (this.customCookies == null)
				{
					this.customCookies = new List<Cookie>();
				}
				return this.customCookies;
			}
			set
			{
				this.customCookies = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00095913 File Offset: 0x00093B13
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0009591B File Offset: 0x00093B1B
		public HTTPFormUsage FormUsage { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00095924 File Offset: 0x00093B24
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0009592C File Offset: 0x00093B2C
		public HTTPRequestStates State { get; internal set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00095935 File Offset: 0x00093B35
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0009593D File Offset: 0x00093B3D
		public int RedirectCount { get; internal set; }

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000DD0 RID: 3536 RVA: 0x00095948 File Offset: 0x00093B48
		// (remove) Token: 0x06000DD1 RID: 3537 RVA: 0x00095980 File Offset: 0x00093B80
		public event Func<HTTPRequest, X509Certificate, X509Chain, bool> CustomCertificationValidator;

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x000959B5 File Offset: 0x00093BB5
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x000959BD File Offset: 0x00093BBD
		public TimeSpan ConnectTimeout { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x000959C6 File Offset: 0x00093BC6
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x000959CE File Offset: 0x00093BCE
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x000959D7 File Offset: 0x00093BD7
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x000959DF File Offset: 0x00093BDF
		public bool EnableTimoutForStreaming { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000959E8 File Offset: 0x00093BE8
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x000959F0 File Offset: 0x00093BF0
		public bool EnableSafeReadOnUnknownContentLength { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000959F9 File Offset: 0x00093BF9
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00095A01 File Offset: 0x00093C01
		public int Priority { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00095A0A File Offset: 0x00093C0A
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x00095A12 File Offset: 0x00093C12
		public ICertificateVerifyer CustomCertificateVerifyer { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00095A1B File Offset: 0x00093C1B
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00095A23 File Offset: 0x00093C23
		public IClientCredentialsProvider CustomClientCredentialsProvider { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00095A2C File Offset: 0x00093C2C
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x00095A34 File Offset: 0x00093C34
		public List<string> CustomTLSServerNameList { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00095A3D File Offset: 0x00093C3D
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00095A45 File Offset: 0x00093C45
		public SupportedProtocols ProtocolHandler { get; set; }

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000DE4 RID: 3556 RVA: 0x00095A4E File Offset: 0x00093C4E
		// (remove) Token: 0x06000DE5 RID: 3557 RVA: 0x00095A67 File Offset: 0x00093C67
		public event OnBeforeRedirectionDelegate OnBeforeRedirection
		{
			add
			{
				this.onBeforeRedirection = (OnBeforeRedirectionDelegate)Delegate.Combine(this.onBeforeRedirection, value);
			}
			remove
			{
				this.onBeforeRedirection = (OnBeforeRedirectionDelegate)Delegate.Remove(this.onBeforeRedirection, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000DE6 RID: 3558 RVA: 0x00095A80 File Offset: 0x00093C80
		// (remove) Token: 0x06000DE7 RID: 3559 RVA: 0x00095A99 File Offset: 0x00093C99
		public event OnBeforeHeaderSendDelegate OnBeforeHeaderSend
		{
			add
			{
				this._onBeforeHeaderSend = (OnBeforeHeaderSendDelegate)Delegate.Combine(this._onBeforeHeaderSend, value);
			}
			remove
			{
				this._onBeforeHeaderSend = (OnBeforeHeaderSendDelegate)Delegate.Remove(this._onBeforeHeaderSend, value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00095AB2 File Offset: 0x00093CB2
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00095ABA File Offset: 0x00093CBA
		public bool TryToMinimizeTCPLatency { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00095AC3 File Offset: 0x00093CC3
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x00095ACB File Offset: 0x00093CCB
		internal long Downloaded { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00095AD4 File Offset: 0x00093CD4
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x00095ADC File Offset: 0x00093CDC
		internal long DownloadLength { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00095AE5 File Offset: 0x00093CE5
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00095AED File Offset: 0x00093CED
		internal bool DownloadProgressChanged { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00095AF8 File Offset: 0x00093CF8
		internal long UploadStreamLength
		{
			get
			{
				if (this.UploadStream == null || !this.UseUploadStreamLength)
				{
					return -1L;
				}
				long result;
				try
				{
					result = this.UploadStream.Length;
				}
				catch
				{
					result = -1L;
				}
				return result;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00095B40 File Offset: 0x00093D40
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00095B48 File Offset: 0x00093D48
		internal long Uploaded { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00095B51 File Offset: 0x00093D51
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00095B59 File Offset: 0x00093D59
		internal long UploadLength { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00095B62 File Offset: 0x00093D62
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00095B6A File Offset: 0x00093D6A
		internal bool UploadProgressChanged { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00095B73 File Offset: 0x00093D73
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00095B7B File Offset: 0x00093D7B
		private Dictionary<string, List<string>> Headers { get; set; }

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00095B84 File Offset: 0x00093D84
		public HTTPRequest(Uri uri) : this(uri, HTTPMethods.Get, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, null)
		{
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00095B99 File Offset: 0x00093D99
		public HTTPRequest(Uri uri, OnRequestFinishedDelegate callback) : this(uri, HTTPMethods.Get, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, callback)
		{
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00095BAE File Offset: 0x00093DAE
		public HTTPRequest(Uri uri, bool isKeepAlive, OnRequestFinishedDelegate callback) : this(uri, HTTPMethods.Get, isKeepAlive, HTTPManager.IsCachingDisabled, callback)
		{
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00095BBF File Offset: 0x00093DBF
		public HTTPRequest(Uri uri, bool isKeepAlive, bool disableCache, OnRequestFinishedDelegate callback) : this(uri, HTTPMethods.Get, isKeepAlive, disableCache, callback)
		{
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00095BCD File Offset: 0x00093DCD
		public HTTPRequest(Uri uri, HTTPMethods methodType) : this(uri, methodType, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled || methodType > HTTPMethods.Get, null)
		{
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00095BEB File Offset: 0x00093DEB
		public HTTPRequest(Uri uri, HTTPMethods methodType, OnRequestFinishedDelegate callback) : this(uri, methodType, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled || methodType > HTTPMethods.Get, callback)
		{
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00095C09 File Offset: 0x00093E09
		public HTTPRequest(Uri uri, HTTPMethods methodType, bool isKeepAlive, OnRequestFinishedDelegate callback) : this(uri, methodType, isKeepAlive, HTTPManager.IsCachingDisabled || methodType > HTTPMethods.Get, callback)
		{
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00095C24 File Offset: 0x00093E24
		public HTTPRequest(Uri uri, HTTPMethods methodType, bool isKeepAlive, bool disableCache, OnRequestFinishedDelegate callback)
		{
			this.Uri = uri;
			this.MethodType = methodType;
			this.IsKeepAlive = isKeepAlive;
			this.DisableCache = disableCache;
			this.Callback = callback;
			this.StreamFragmentSize = 4096;
			this.MaxFragmentQueueLength = 10;
			this.DisableRetry = (methodType > HTTPMethods.Get);
			this.MaxRedirects = int.MaxValue;
			this.RedirectCount = 0;
			this.IsCookiesEnabled = HTTPManager.IsCookiesEnabled;
			this.Downloaded = (this.DownloadLength = 0L);
			this.DownloadProgressChanged = false;
			this.State = HTTPRequestStates.Initial;
			this.ConnectTimeout = HTTPManager.ConnectTimeout;
			this.Timeout = HTTPManager.RequestTimeout;
			this.EnableTimoutForStreaming = false;
			this.EnableSafeReadOnUnknownContentLength = true;
			this.Proxy = HTTPManager.Proxy;
			this.UseUploadStreamLength = true;
			this.DisposeUploadStream = true;
			this.CustomCertificateVerifyer = HTTPManager.DefaultCertificateVerifyer;
			this.CustomClientCredentialsProvider = HTTPManager.DefaultClientCredentialsProvider;
			this.UseAlternateSSL = HTTPManager.UseAlternateSSLDefaultValue;
			this.CustomCertificationValidator += HTTPManager.DefaultCertificationValidator;
			this.TryToMinimizeTCPLatency = HTTPManager.TryToMinimizeTCPLatency;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00095D29 File Offset: 0x00093F29
		public void AddField(string fieldName, string value)
		{
			this.AddField(fieldName, value, Encoding.UTF8);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00095D38 File Offset: 0x00093F38
		public void AddField(string fieldName, string value, Encoding e)
		{
			if (this.FieldCollector == null)
			{
				this.FieldCollector = new HTTPFormBase();
			}
			this.FieldCollector.AddField(fieldName, value, e);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00095D5B File Offset: 0x00093F5B
		public void AddBinaryData(string fieldName, byte[] content)
		{
			this.AddBinaryData(fieldName, content, null, null);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00095D67 File Offset: 0x00093F67
		public void AddBinaryData(string fieldName, byte[] content, string fileName)
		{
			this.AddBinaryData(fieldName, content, fileName, null);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00095D73 File Offset: 0x00093F73
		public void AddBinaryData(string fieldName, byte[] content, string fileName, string mimeType)
		{
			if (this.FieldCollector == null)
			{
				this.FieldCollector = new HTTPFormBase();
			}
			this.FieldCollector.AddBinaryData(fieldName, content, fileName, mimeType);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00095D98 File Offset: 0x00093F98
		public void SetForm(HTTPFormBase form)
		{
			this.FormImpl = form;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00095DA1 File Offset: 0x00093FA1
		public List<HTTPFieldData> GetFormFields()
		{
			if (this.FieldCollector == null || this.FieldCollector.IsEmpty)
			{
				return null;
			}
			return new List<HTTPFieldData>(this.FieldCollector.Fields);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00095DCA File Offset: 0x00093FCA
		public void ClearForm()
		{
			this.FormImpl = null;
			this.FieldCollector = null;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00095DDC File Offset: 0x00093FDC
		private HTTPFormBase SelectFormImplementation()
		{
			if (this.FormImpl != null)
			{
				return this.FormImpl;
			}
			if (this.FieldCollector == null)
			{
				return null;
			}
			switch (this.FormUsage)
			{
			case HTTPFormUsage.Automatic:
				if (this.FieldCollector.HasBinary || this.FieldCollector.HasLongValue)
				{
					goto IL_5F;
				}
				break;
			case HTTPFormUsage.UrlEncoded:
				break;
			case HTTPFormUsage.Multipart:
				goto IL_5F;
			case HTTPFormUsage.RawJSon:
				this.FormImpl = new RawJsonForm();
				goto IL_77;
			default:
				goto IL_77;
			}
			this.FormImpl = new HTTPUrlEncodedForm();
			goto IL_77;
			IL_5F:
			this.FormImpl = new HTTPMultiPartForm();
			IL_77:
			this.FormImpl.CopyFrom(this.FieldCollector);
			return this.FormImpl;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00095E78 File Offset: 0x00094078
		public void AddHeader(string name, string value)
		{
			if (this.Headers == null)
			{
				this.Headers = new Dictionary<string, List<string>>();
			}
			List<string> list;
			if (!this.Headers.TryGetValue(name, out list))
			{
				this.Headers.Add(name, list = new List<string>(1));
			}
			list.Add(value);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00095EC4 File Offset: 0x000940C4
		public void SetHeader(string name, string value)
		{
			if (this.Headers == null)
			{
				this.Headers = new Dictionary<string, List<string>>();
			}
			List<string> list;
			if (!this.Headers.TryGetValue(name, out list))
			{
				this.Headers.Add(name, list = new List<string>(1));
			}
			list.Clear();
			list.Add(value);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00095F15 File Offset: 0x00094115
		public bool RemoveHeader(string name)
		{
			return this.Headers != null && this.Headers.Remove(name);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00095F2D File Offset: 0x0009412D
		public bool HasHeader(string name)
		{
			return this.Headers != null && this.Headers.ContainsKey(name);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00095F48 File Offset: 0x00094148
		public string GetFirstHeaderValue(string name)
		{
			if (this.Headers == null)
			{
				return null;
			}
			List<string> list = null;
			if (this.Headers.TryGetValue(name, out list) && list.Count > 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00095F84 File Offset: 0x00094184
		public List<string> GetHeaderValues(string name)
		{
			if (this.Headers == null)
			{
				return null;
			}
			List<string> list = null;
			if (this.Headers.TryGetValue(name, out list) && list.Count > 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00095FB9 File Offset: 0x000941B9
		public void RemoveHeaders()
		{
			if (this.Headers == null)
			{
				return;
			}
			this.Headers.Clear();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00095FCF File Offset: 0x000941CF
		public void SetRangeHeader(int firstBytePos)
		{
			this.SetHeader("Range", string.Format("bytes={0}-", firstBytePos));
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00095FEC File Offset: 0x000941EC
		public void SetRangeHeader(int firstBytePos, int lastBytePos)
		{
			this.SetHeader("Range", string.Format("bytes={0}-{1}", firstBytePos, lastBytePos));
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0009600F File Offset: 0x0009420F
		public void EnumerateHeaders(OnHeaderEnumerationDelegate callback)
		{
			this.EnumerateHeaders(callback, false);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0009601C File Offset: 0x0009421C
		public void EnumerateHeaders(OnHeaderEnumerationDelegate callback, bool callBeforeSendCallback)
		{
			if (!this.HasHeader("Host"))
			{
				if (this.CurrentUri.Port == 80 || this.CurrentUri.Port == 443)
				{
					this.SetHeader("Host", this.CurrentUri.Host);
				}
				else
				{
					this.SetHeader("Host", this.CurrentUri.Authority);
				}
			}
			if (this.IsRedirected && !this.HasHeader("Referer"))
			{
				this.AddHeader("Referer", this.Uri.ToString());
			}
			if (!this.HasHeader("Accept-Encoding"))
			{
				this.AddHeader("Accept-Encoding", "gzip, identity");
			}
			if (this.HasProxy && !this.HasHeader("Proxy-Connection"))
			{
				this.AddHeader("Proxy-Connection", this.IsKeepAlive ? "Keep-Alive" : "Close");
			}
			if (!this.HasHeader("Connection"))
			{
				this.AddHeader("Connection", this.IsKeepAlive ? "Keep-Alive, TE" : "Close, TE");
			}
			if (!this.HasHeader("TE"))
			{
				this.AddHeader("TE", "identity");
			}
			if (!this.HasHeader("User-Agent"))
			{
				this.AddHeader("User-Agent", "BestHTTP");
			}
			long num;
			if (this.UploadStream == null)
			{
				byte[] entityBody = this.GetEntityBody();
				num = (long)((entityBody != null) ? entityBody.Length : 0);
				if (this.RawData == null && (this.FormImpl != null || (this.FieldCollector != null && !this.FieldCollector.IsEmpty)))
				{
					this.SelectFormImplementation();
					if (this.FormImpl != null)
					{
						this.FormImpl.PrepareRequest(this);
					}
				}
			}
			else
			{
				num = this.UploadStreamLength;
				if (num == -1L)
				{
					this.SetHeader("Transfer-Encoding", "Chunked");
				}
				if (!this.HasHeader("Content-Type"))
				{
					this.SetHeader("Content-Type", "application/octet-stream");
				}
			}
			if (num >= 0L && !this.HasHeader("Content-Length"))
			{
				this.SetHeader("Content-Length", num.ToString());
			}
			if (this.HasProxy && this.Proxy.Credentials != null)
			{
				switch (this.Proxy.Credentials.Type)
				{
				case AuthenticationTypes.Unknown:
				case AuthenticationTypes.Digest:
				{
					Digest digest = DigestStore.Get(this.Proxy.Address);
					if (digest != null)
					{
						string value = digest.GenerateResponseHeader(this, this.Proxy.Credentials, false);
						if (!string.IsNullOrEmpty(value))
						{
							this.SetHeader("Proxy-Authorization", value);
						}
					}
					break;
				}
				case AuthenticationTypes.Basic:
					this.SetHeader("Proxy-Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Proxy.Credentials.UserName + ":" + this.Proxy.Credentials.Password)));
					break;
				}
			}
			if (this.Credentials != null)
			{
				switch (this.Credentials.Type)
				{
				case AuthenticationTypes.Unknown:
				case AuthenticationTypes.Digest:
				{
					Digest digest2 = DigestStore.Get(this.CurrentUri);
					if (digest2 != null)
					{
						string value2 = digest2.GenerateResponseHeader(this, this.Credentials, false);
						if (!string.IsNullOrEmpty(value2))
						{
							this.SetHeader("Authorization", value2);
						}
					}
					break;
				}
				case AuthenticationTypes.Basic:
					this.SetHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Credentials.UserName + ":" + this.Credentials.Password)));
					break;
				}
			}
			List<Cookie> list = this.IsCookiesEnabled ? CookieJar.Get(this.CurrentUri) : null;
			if (list == null || list.Count == 0)
			{
				list = this.customCookies;
			}
			else if (this.customCookies != null)
			{
				for (int i = 0; i < this.customCookies.Count; i++)
				{
					Cookie customCookie = this.customCookies[i];
					int num2 = list.FindIndex((Cookie c) => c.Name.Equals(customCookie.Name));
					if (num2 >= 0)
					{
						list[num2] = customCookie;
					}
					else
					{
						list.Add(customCookie);
					}
				}
			}
			if (list != null && list.Count > 0)
			{
				bool flag = true;
				string text = string.Empty;
				bool flag2 = HTTPProtocolFactory.IsSecureProtocol(this.CurrentUri);
				foreach (Cookie cookie in list)
				{
					if (!cookie.IsSecure || (cookie.IsSecure && flag2))
					{
						if (!flag)
						{
							text += "; ";
						}
						else
						{
							flag = false;
						}
						text += cookie.ToString();
						cookie.LastAccess = DateTime.UtcNow;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.SetHeader("Cookie", text);
				}
			}
			if (callBeforeSendCallback && this._onBeforeHeaderSend != null)
			{
				try
				{
					this._onBeforeHeaderSend(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("HTTPRequest", "OnBeforeHeaderSend", ex);
				}
			}
			if (callback != null && this.Headers != null)
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in this.Headers)
				{
					callback(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x000965A0 File Offset: 0x000947A0
		private void SendHeaders(Stream stream)
		{
			this.EnumerateHeaders(delegate(string header, List<string> values)
			{
				if (string.IsNullOrEmpty(header) || values == null)
				{
					return;
				}
				byte[] asciibytes = (header + ": ").GetASCIIBytes();
				for (int i = 0; i < values.Count; i++)
				{
					if (string.IsNullOrEmpty(values[i]))
					{
						HTTPManager.Logger.Warning("HTTPRequest", string.Format("Null/empty value for header: {0}", header));
					}
					else
					{
						if (HTTPManager.Logger.Level <= Loglevels.Information)
						{
							this.VerboseLogging(string.Concat(new string[]
							{
								"Header - '",
								header,
								"': '",
								values[i],
								"'"
							}));
						}
						byte[] asciibytes2 = values[i].GetASCIIBytes();
						stream.WriteArray(asciibytes);
						stream.WriteArray(asciibytes2);
						stream.WriteArray(HTTPRequest.EOL);
						VariableSizedBufferPool.Release(asciibytes2);
					}
				}
				VariableSizedBufferPool.Release(asciibytes);
			}, true);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000965D4 File Offset: 0x000947D4
		public string DumpHeaders()
		{
			string result;
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream(5120))
			{
				this.SendHeaders(bufferPoolMemoryStream);
				result = bufferPoolMemoryStream.ToArray().AsciiToString();
			}
			return result;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0009661C File Offset: 0x0009481C
		public byte[] GetEntityBody()
		{
			if (this.RawData != null)
			{
				return this.RawData;
			}
			if (this.FormImpl != null || (this.FieldCollector != null && !this.FieldCollector.IsEmpty))
			{
				this.SelectFormImplementation();
				if (this.FormImpl != null)
				{
					return this.FormImpl.GetData();
				}
			}
			return null;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00096674 File Offset: 0x00094874
		internal void SendOutTo(Stream stream)
		{
			try
			{
				string arg = this.HasProxy ? this.Proxy.GetRequestPath(this.CurrentUri) : this.CurrentUri.GetRequestPathAndQueryURL();
				string text = string.Format("{0} {1} HTTP/1.1", HTTPRequest.MethodNames[(int)this.MethodType], arg);
				if (HTTPManager.Logger.Level <= Loglevels.Information)
				{
					HTTPManager.Logger.Information("HTTPRequest", string.Format("Sending request: '{0}'", text));
				}
				using (WriteOnlyBufferedStream writeOnlyBufferedStream = new WriteOnlyBufferedStream(stream, (int)((float)HTTPRequest.UploadChunkSize * 1.5f)))
				{
					byte[] asciibytes = text.GetASCIIBytes();
					writeOnlyBufferedStream.WriteArray(asciibytes);
					writeOnlyBufferedStream.WriteArray(HTTPRequest.EOL);
					VariableSizedBufferPool.Release(asciibytes);
					this.SendHeaders(writeOnlyBufferedStream);
					writeOnlyBufferedStream.WriteArray(HTTPRequest.EOL);
					writeOnlyBufferedStream.Flush();
					byte[] array = this.RawData;
					if (array == null && this.FormImpl != null)
					{
						array = this.FormImpl.GetData();
					}
					if (array != null || this.UploadStream != null)
					{
						Stream stream2 = this.UploadStream;
						if (stream2 == null)
						{
							stream2 = new MemoryStream(array, 0, array.Length);
							this.UploadLength = (long)array.Length;
						}
						else
						{
							this.UploadLength = (this.UseUploadStreamLength ? this.UploadStreamLength : -1L);
						}
						this.Uploaded = 0L;
						byte[] array2 = VariableSizedBufferPool.Get((long)HTTPRequest.UploadChunkSize, true);
						int num;
						while ((num = stream2.Read(array2, 0, array2.Length)) > 0)
						{
							if (!this.UseUploadStreamLength)
							{
								byte[] asciibytes2 = num.ToString("X").GetASCIIBytes();
								writeOnlyBufferedStream.WriteArray(asciibytes2);
								writeOnlyBufferedStream.WriteArray(HTTPRequest.EOL);
								VariableSizedBufferPool.Release(asciibytes2);
							}
							writeOnlyBufferedStream.Write(array2, 0, num);
							if (!this.UseUploadStreamLength)
							{
								writeOnlyBufferedStream.WriteArray(HTTPRequest.EOL);
							}
							this.Uploaded += (long)num;
							writeOnlyBufferedStream.Flush();
							this.UploadProgressChanged = true;
						}
						VariableSizedBufferPool.Release(array2);
						if (!this.UseUploadStreamLength)
						{
							byte[] array3 = VariableSizedBufferPool.Get(1L, true);
							array3[0] = 48;
							writeOnlyBufferedStream.Write(array3, 0, 1);
							writeOnlyBufferedStream.WriteArray(HTTPRequest.EOL);
							writeOnlyBufferedStream.WriteArray(HTTPRequest.EOL);
							VariableSizedBufferPool.Release(array3);
						}
						writeOnlyBufferedStream.Flush();
						if (this.UploadStream == null && stream2 != null)
						{
							stream2.Dispose();
						}
					}
					else
					{
						writeOnlyBufferedStream.Flush();
					}
				}
				HTTPManager.Logger.Information("HTTPRequest", "'" + text + "' sent out");
			}
			finally
			{
				if (this.UploadStream != null && this.DisposeUploadStream)
				{
					this.UploadStream.Dispose();
				}
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00096928 File Offset: 0x00094B28
		internal void UpgradeCallback()
		{
			if (this.Response == null || !this.Response.IsUpgraded)
			{
				return;
			}
			try
			{
				if (this.OnUpgraded != null)
				{
					this.OnUpgraded(this, this.Response);
				}
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("HTTPRequest", "UpgradeCallback", ex);
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00096990 File Offset: 0x00094B90
		internal void CallCallback()
		{
			try
			{
				if (this.Callback != null)
				{
					this.Callback(this, this.Response);
				}
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("HTTPRequest", "CallCallback", ex);
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000969E4 File Offset: 0x00094BE4
		internal bool CallOnBeforeRedirection(Uri redirectUri)
		{
			return this.onBeforeRedirection == null || this.onBeforeRedirection(this, this.Response, redirectUri);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00096A03 File Offset: 0x00094C03
		internal void FinishStreaming()
		{
			if (this.Response != null && this.UseStreaming)
			{
				this.Response.FinishStreaming();
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00002B75 File Offset: 0x00000D75
		internal void Prepare()
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00096A20 File Offset: 0x00094C20
		internal bool CallCustomCertificationValidator(X509Certificate cert, X509Chain chain)
		{
			return this.CustomCertificationValidator == null || this.CustomCertificationValidator(this, cert, chain);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00096A3A File Offset: 0x00094C3A
		public HTTPRequest Send()
		{
			return HTTPManager.SendRequest(this);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00096A44 File Offset: 0x00094C44
		public void Abort()
		{
			if (Monitor.TryEnter(HTTPManager.Locker, TimeSpan.FromMilliseconds(100.0)))
			{
				try
				{
					if (this.State >= HTTPRequestStates.Finished)
					{
						HTTPManager.Logger.Warning("HTTPRequest", string.Format("Abort - Already in a state({0}) where no Abort required!", this.State.ToString()));
						return;
					}
					ConnectionBase connectionWith = HTTPManager.GetConnectionWith(this);
					if (connectionWith == null)
					{
						if (!HTTPManager.RemoveFromQueue(this))
						{
							HTTPManager.Logger.Warning("HTTPRequest", "Abort - No active connection found with this request! (The request may already finished?)");
						}
						this.State = HTTPRequestStates.Aborted;
						this.CallCallback();
						return;
					}
					if (this.Response != null && this.Response.IsStreamed)
					{
						this.Response.Dispose();
					}
					connectionWith.Abort(HTTPConnectionStates.AbortRequested);
					return;
				}
				finally
				{
					Monitor.Exit(HTTPManager.Locker);
				}
			}
			throw new Exception("Wasn't able to acquire a thread lock. Abort failed!");
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00096B2C File Offset: 0x00094D2C
		public void Clear()
		{
			this.ClearForm();
			this.RemoveHeaders();
			this.IsRedirected = false;
			this.RedirectCount = 0;
			this.Downloaded = (this.DownloadLength = 0L);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00096B64 File Offset: 0x00094D64
		private void VerboseLogging(string str)
		{
			HTTPManager.Logger.Verbose("HTTPRequest", "'" + this.CurrentUri.ToString() + "' - " + str);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00096B90 File Offset: 0x00094D90
		public bool MoveNext()
		{
			return this.State < HTTPRequestStates.Finished;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00096B9B File Offset: 0x00094D9B
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00096BA2 File Offset: 0x00094DA2
		HTTPRequest IEnumerator<HTTPRequest>.Current
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00096BA5 File Offset: 0x00094DA5
		public void Dispose()
		{
			if (this.Response != null)
			{
				this.Response.Dispose();
			}
		}

		// Token: 0x040011D7 RID: 4567
		public static readonly byte[] EOL = new byte[]
		{
			13,
			10
		};

		// Token: 0x040011D8 RID: 4568
		public static readonly string[] MethodNames = new string[]
		{
			HTTPMethods.Get.ToString().ToUpper(),
			HTTPMethods.Head.ToString().ToUpper(),
			HTTPMethods.Post.ToString().ToUpper(),
			HTTPMethods.Put.ToString().ToUpper(),
			HTTPMethods.Delete.ToString().ToUpper(),
			HTTPMethods.Patch.ToString().ToUpper(),
			HTTPMethods.Merge.ToString().ToUpper(),
			HTTPMethods.Options.ToString().ToUpper()
		};

		// Token: 0x040011D9 RID: 4569
		public static int UploadChunkSize = 2048;

		// Token: 0x040011E0 RID: 4576
		public OnUploadProgressDelegate OnUploadProgress;

		// Token: 0x040011E3 RID: 4579
		public OnDownloadProgressDelegate OnProgress;

		// Token: 0x040011E4 RID: 4580
		public OnRequestFinishedDelegate OnUpgraded;

		// Token: 0x040011F1 RID: 4593
		private List<Cookie> customCookies;

		// Token: 0x040011FF RID: 4607
		private OnBeforeRedirectionDelegate onBeforeRedirection;

		// Token: 0x04001200 RID: 4608
		private OnBeforeHeaderSendDelegate _onBeforeHeaderSend;

		// Token: 0x04001208 RID: 4616
		private bool isKeepAlive;

		// Token: 0x04001209 RID: 4617
		private bool disableCache;

		// Token: 0x0400120A RID: 4618
		private bool cacheOnly;

		// Token: 0x0400120B RID: 4619
		private int streamFragmentSize;

		// Token: 0x0400120C RID: 4620
		private bool useStreaming;

		// Token: 0x0400120E RID: 4622
		private HTTPFormBase FieldCollector;

		// Token: 0x0400120F RID: 4623
		private HTTPFormBase FormImpl;
	}
}
