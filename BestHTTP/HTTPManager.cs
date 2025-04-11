using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using BestHTTP.Caching;
using BestHTTP.Cookies;
using BestHTTP.Extensions;
using BestHTTP.Logger;
using BestHTTP.PlatformSupport.FileSystem;
using BestHTTP.Statistics;
using Org.BouncyCastle.Crypto.Tls;
using UnityEngine;

namespace BestHTTP
{
	// Token: 0x02000171 RID: 369
	public static class HTTPManager
	{
		// Token: 0x06000D1F RID: 3359 RVA: 0x00094148 File Offset: 0x00092348
		static HTTPManager()
		{
			HTTPManager.MaxConnectionIdleTime = TimeSpan.FromSeconds(20.0);
			HTTPManager.IsCookiesEnabled = true;
			HTTPManager.CookieJarSize = 10485760U;
			HTTPManager.EnablePrivateBrowsing = false;
			HTTPManager.ConnectTimeout = TimeSpan.FromSeconds(20.0);
			HTTPManager.RequestTimeout = TimeSpan.FromSeconds(60.0);
			HTTPManager.logger = new DefaultLogger();
			HTTPManager.DefaultCertificateVerifyer = null;
			HTTPManager.UseAlternateSSLDefaultValue = true;
			HTTPManager.IOService = new DefaultIOService();
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00094230 File Offset: 0x00092430
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00094237 File Offset: 0x00092437
		public static byte MaxConnectionPerServer
		{
			get
			{
				return HTTPManager.maxConnectionPerServer;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MaxConnectionPerServer must be greater than 0!");
				}
				HTTPManager.maxConnectionPerServer = value;
			}
		} = 4;

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0009424E File Offset: 0x0009244E
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00094255 File Offset: 0x00092455
		public static bool KeepAliveDefaultValue { get; set; } = true;

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0009425D File Offset: 0x0009245D
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00094264 File Offset: 0x00092464
		public static bool IsCachingDisabled { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0009426C File Offset: 0x0009246C
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00094273 File Offset: 0x00092473
		public static TimeSpan MaxConnectionIdleTime { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0009427B File Offset: 0x0009247B
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00094282 File Offset: 0x00092482
		public static bool IsCookiesEnabled { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0009428A File Offset: 0x0009248A
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00094291 File Offset: 0x00092491
		public static uint CookieJarSize { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00094299 File Offset: 0x00092499
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x000942A0 File Offset: 0x000924A0
		public static bool EnablePrivateBrowsing { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000942A8 File Offset: 0x000924A8
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x000942AF File Offset: 0x000924AF
		public static TimeSpan ConnectTimeout { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x000942B7 File Offset: 0x000924B7
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x000942BE File Offset: 0x000924BE
		public static TimeSpan RequestTimeout { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x000942C6 File Offset: 0x000924C6
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x000942CD File Offset: 0x000924CD
		public static Func<string> RootCacheFolderProvider { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000942D5 File Offset: 0x000924D5
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000942DC File Offset: 0x000924DC
		public static Proxy Proxy { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000942E4 File Offset: 0x000924E4
		public static HeartbeatManager Heartbeats
		{
			get
			{
				if (HTTPManager.heartbeats == null)
				{
					HTTPManager.heartbeats = new HeartbeatManager();
				}
				return HTTPManager.heartbeats;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000942FC File Offset: 0x000924FC
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0009431F File Offset: 0x0009251F
		public static ILogger Logger
		{
			get
			{
				if (HTTPManager.logger == null)
				{
					HTTPManager.logger = new DefaultLogger();
					HTTPManager.logger.Level = Loglevels.None;
				}
				return HTTPManager.logger;
			}
			set
			{
				HTTPManager.logger = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00094327 File Offset: 0x00092527
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0009432E File Offset: 0x0009252E
		public static ICertificateVerifyer DefaultCertificateVerifyer { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00094336 File Offset: 0x00092536
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0009433D File Offset: 0x0009253D
		public static IClientCredentialsProvider DefaultClientCredentialsProvider { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00094345 File Offset: 0x00092545
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0009434C File Offset: 0x0009254C
		public static bool UseAlternateSSLDefaultValue { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00094354 File Offset: 0x00092554
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0009435B File Offset: 0x0009255B
		public static Func<HTTPRequest, X509Certificate, X509Chain, bool> DefaultCertificationValidator { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00094363 File Offset: 0x00092563
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x0009436A File Offset: 0x0009256A
		internal static int MaxPathLength { get; set; } = 255;

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00094372 File Offset: 0x00092572
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00094379 File Offset: 0x00092579
		internal static bool IsQuitting { get; private set; }

		// Token: 0x06000D45 RID: 3397 RVA: 0x00094381 File Offset: 0x00092581
		public static void Setup()
		{
			HTTPUpdateDelegator.CheckInstance();
			HTTPCacheService.CheckSetup();
			CookieJar.SetupFolder();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00094392 File Offset: 0x00092592
		public static HTTPRequest SendRequest(string url, OnRequestFinishedDelegate callback)
		{
			return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), HTTPMethods.Get, callback));
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000943A6 File Offset: 0x000925A6
		public static HTTPRequest SendRequest(string url, HTTPMethods methodType, OnRequestFinishedDelegate callback)
		{
			return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), methodType, callback));
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000943BA File Offset: 0x000925BA
		public static HTTPRequest SendRequest(string url, HTTPMethods methodType, bool isKeepAlive, OnRequestFinishedDelegate callback)
		{
			return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), methodType, isKeepAlive, callback));
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000943CF File Offset: 0x000925CF
		public static HTTPRequest SendRequest(string url, HTTPMethods methodType, bool isKeepAlive, bool disableCache, OnRequestFinishedDelegate callback)
		{
			return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), methodType, isKeepAlive, disableCache, callback));
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000943E8 File Offset: 0x000925E8
		public static HTTPRequest SendRequest(HTTPRequest request)
		{
			object locker = HTTPManager.Locker;
			lock (locker)
			{
				HTTPManager.Setup();
				if (HTTPManager.IsCallingCallbacks)
				{
					request.State = HTTPRequestStates.Queued;
					HTTPManager.RequestQueue.Add(request);
				}
				else
				{
					HTTPManager.SendRequestImpl(request);
				}
			}
			return request;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0009444C File Offset: 0x0009264C
		public static GeneralStatistics GetGeneralStatistics(StatisticsQueryFlags queryFlags)
		{
			GeneralStatistics result = default(GeneralStatistics);
			result.QueryFlags = queryFlags;
			if ((queryFlags & StatisticsQueryFlags.Connections) != (StatisticsQueryFlags)0)
			{
				int num = 0;
				foreach (KeyValuePair<string, List<ConnectionBase>> keyValuePair in HTTPManager.Connections)
				{
					if (keyValuePair.Value != null)
					{
						num += keyValuePair.Value.Count;
					}
				}
				result.Connections = num;
				result.ActiveConnections = HTTPManager.ActiveConnections.Count;
				result.FreeConnections = HTTPManager.FreeConnections.Count;
				result.RecycledConnections = HTTPManager.RecycledConnections.Count;
				result.RequestsInQueue = HTTPManager.RequestQueue.Count;
			}
			if ((queryFlags & StatisticsQueryFlags.Cache) != (StatisticsQueryFlags)0)
			{
				result.CacheEntityCount = HTTPCacheService.GetCacheEntityCount();
				result.CacheSize = HTTPCacheService.GetCacheSize();
			}
			if ((queryFlags & StatisticsQueryFlags.Cookies) != (StatisticsQueryFlags)0)
			{
				List<Cookie> all = CookieJar.GetAll();
				result.CookieCount = all.Count;
				uint num2 = 0U;
				for (int i = 0; i < all.Count; i++)
				{
					num2 += all[i].GuessSize();
				}
				result.CookieJarSize = num2;
			}
			return result;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00094584 File Offset: 0x00092784
		private static void SendRequestImpl(HTTPRequest request)
		{
			ConnectionBase conn = HTTPManager.FindOrCreateFreeConnection(request);
			if (conn != null)
			{
				if (HTTPManager.ActiveConnections.Find((ConnectionBase c) => c == conn) == null)
				{
					HTTPManager.ActiveConnections.Add(conn);
				}
				HTTPManager.FreeConnections.Remove(conn);
				request.State = HTTPRequestStates.Processing;
				request.Prepare();
				conn.Process(request);
				return;
			}
			request.State = HTTPRequestStates.Queued;
			HTTPManager.RequestQueue.Add(request);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00094610 File Offset: 0x00092810
		private static string GetKeyForRequest(HTTPRequest request)
		{
			if (request.CurrentUri.IsFile)
			{
				return request.CurrentUri.ToString();
			}
			return ((request.Proxy != null) ? new UriBuilder(request.Proxy.Address.Scheme, request.Proxy.Address.Host, request.Proxy.Address.Port).Uri.ToString() : string.Empty) + new UriBuilder(request.CurrentUri.Scheme, request.CurrentUri.Host, request.CurrentUri.Port).Uri.ToString();
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000946B9 File Offset: 0x000928B9
		private static ConnectionBase CreateConnection(HTTPRequest request, string serverUrl)
		{
			if (request.CurrentUri.IsFile && Application.platform != 17)
			{
				return new FileConnection(serverUrl);
			}
			return new HTTPConnection(serverUrl);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000946E0 File Offset: 0x000928E0
		private static ConnectionBase FindOrCreateFreeConnection(HTTPRequest request)
		{
			ConnectionBase connectionBase = null;
			string keyForRequest = HTTPManager.GetKeyForRequest(request);
			List<ConnectionBase> list;
			if (HTTPManager.Connections.TryGetValue(keyForRequest, out list))
			{
				int num = 0;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].IsActive)
					{
						num++;
					}
				}
				if (num <= (int)HTTPManager.MaxConnectionPerServer)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (connectionBase != null)
						{
							break;
						}
						ConnectionBase connectionBase2 = list[j];
						if (connectionBase2 != null && connectionBase2.IsFree && (!connectionBase2.HasProxy || connectionBase2.LastProcessedUri == null || connectionBase2.LastProcessedUri.Host.Equals(request.CurrentUri.Host, StringComparison.OrdinalIgnoreCase)))
						{
							connectionBase = connectionBase2;
						}
					}
				}
			}
			else
			{
				HTTPManager.Connections.Add(keyForRequest, list = new List<ConnectionBase>((int)HTTPManager.MaxConnectionPerServer));
			}
			if (connectionBase == null)
			{
				if (list.Count >= (int)HTTPManager.MaxConnectionPerServer)
				{
					return null;
				}
				list.Add(connectionBase = HTTPManager.CreateConnection(request, keyForRequest));
			}
			return connectionBase;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000947E4 File Offset: 0x000929E4
		private static bool CanProcessFromQueue()
		{
			for (int i = 0; i < HTTPManager.RequestQueue.Count; i++)
			{
				if (HTTPManager.FindOrCreateFreeConnection(HTTPManager.RequestQueue[i]) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0009481B File Offset: 0x00092A1B
		private static void RecycleConnection(ConnectionBase conn)
		{
			conn.Recycle(new HTTPConnectionRecycledDelegate(HTTPManager.OnConnectionRecylced));
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00094830 File Offset: 0x00092A30
		private static void OnConnectionRecylced(ConnectionBase conn)
		{
			List<ConnectionBase> recycledConnections = HTTPManager.RecycledConnections;
			lock (recycledConnections)
			{
				HTTPManager.RecycledConnections.Add(conn);
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00094874 File Offset: 0x00092A74
		internal static ConnectionBase GetConnectionWith(HTTPRequest request)
		{
			object locker = HTTPManager.Locker;
			ConnectionBase result;
			lock (locker)
			{
				for (int i = 0; i < HTTPManager.ActiveConnections.Count; i++)
				{
					ConnectionBase connectionBase = HTTPManager.ActiveConnections[i];
					if (connectionBase.CurrentRequest == request)
					{
						return connectionBase;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000948E4 File Offset: 0x00092AE4
		internal static bool RemoveFromQueue(HTTPRequest request)
		{
			return HTTPManager.RequestQueue.Remove(request);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000948F4 File Offset: 0x00092AF4
		internal static string GetRootCacheFolder()
		{
			try
			{
				if (HTTPManager.RootCacheFolderProvider != null)
				{
					return HTTPManager.RootCacheFolderProvider();
				}
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("HTTPManager", "GetRootCacheFolder", ex);
			}
			return Application.persistentDataPath;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00094948 File Offset: 0x00092B48
		public static void OnUpdate()
		{
			if (Monitor.TryEnter(HTTPManager.Locker))
			{
				try
				{
					HTTPManager.IsCallingCallbacks = true;
					try
					{
						int i = 0;
						while (i < HTTPManager.ActiveConnections.Count)
						{
							ConnectionBase connectionBase = HTTPManager.ActiveConnections[i];
							switch (connectionBase.State)
							{
							case HTTPConnectionStates.Processing:
								connectionBase.HandleProgressCallback();
								if (connectionBase.CurrentRequest.UseStreaming && connectionBase.CurrentRequest.Response != null && connectionBase.CurrentRequest.Response.HasStreamedFragments())
								{
									connectionBase.HandleCallback();
								}
								try
								{
									if (((!connectionBase.CurrentRequest.UseStreaming && connectionBase.CurrentRequest.UploadStream == null) || connectionBase.CurrentRequest.EnableTimoutForStreaming) && DateTime.UtcNow - connectionBase.StartTime > connectionBase.CurrentRequest.Timeout)
									{
										connectionBase.Abort(HTTPConnectionStates.TimedOut);
									}
									break;
								}
								catch (OverflowException)
								{
									HTTPManager.Logger.Warning("HTTPManager", "TimeSpan overflow");
									break;
								}
								goto IL_108;
							case HTTPConnectionStates.Redirected:
								goto IL_185;
							case HTTPConnectionStates.Upgraded:
								connectionBase.HandleCallback();
								break;
							case HTTPConnectionStates.WaitForProtocolShutdown:
							{
								IProtocol protocol = connectionBase.CurrentRequest.Response as IProtocol;
								if (protocol != null)
								{
									protocol.HandleEvents();
								}
								if (protocol == null || protocol.IsClosed)
								{
									connectionBase.HandleCallback();
									connectionBase.Dispose();
									HTTPManager.RecycleConnection(connectionBase);
								}
								break;
							}
							case HTTPConnectionStates.WaitForRecycle:
								connectionBase.CurrentRequest.FinishStreaming();
								connectionBase.HandleCallback();
								HTTPManager.RecycleConnection(connectionBase);
								break;
							case HTTPConnectionStates.Free:
								HTTPManager.RecycleConnection(connectionBase);
								break;
							case HTTPConnectionStates.AbortRequested:
							{
								IProtocol protocol = connectionBase.CurrentRequest.Response as IProtocol;
								if (protocol != null)
								{
									protocol.HandleEvents();
									if (protocol.IsClosed)
									{
										connectionBase.HandleCallback();
										connectionBase.Dispose();
										HTTPManager.RecycleConnection(connectionBase);
									}
								}
								break;
							}
							case HTTPConnectionStates.TimedOut:
								goto IL_108;
							case HTTPConnectionStates.Closed:
								connectionBase.CurrentRequest.FinishStreaming();
								connectionBase.HandleCallback();
								HTTPManager.RecycleConnection(connectionBase);
								break;
							}
							IL_251:
							i++;
							continue;
							IL_108:
							try
							{
								if (DateTime.UtcNow - connectionBase.TimedOutStart > TimeSpan.FromMilliseconds(500.0))
								{
									HTTPManager.Logger.Information("HTTPManager", "Hard aborting connection because of a long waiting TimedOut state");
									connectionBase.CurrentRequest.Response = null;
									connectionBase.CurrentRequest.State = HTTPRequestStates.TimedOut;
									connectionBase.HandleCallback();
									HTTPManager.RecycleConnection(connectionBase);
								}
								goto IL_251;
							}
							catch (OverflowException)
							{
								HTTPManager.Logger.Warning("HTTPManager", "TimeSpan overflow");
								goto IL_251;
							}
							IL_185:
							HTTPManager.SendRequest(connectionBase.CurrentRequest);
							HTTPManager.RecycleConnection(connectionBase);
							goto IL_251;
						}
					}
					finally
					{
						HTTPManager.IsCallingCallbacks = false;
					}
					if (Monitor.TryEnter(HTTPManager.RecycledConnections))
					{
						try
						{
							if (HTTPManager.RecycledConnections.Count > 0)
							{
								for (int j = 0; j < HTTPManager.RecycledConnections.Count; j++)
								{
									ConnectionBase connectionBase2 = HTTPManager.RecycledConnections[j];
									if (connectionBase2.IsFree)
									{
										HTTPManager.ActiveConnections.Remove(connectionBase2);
										HTTPManager.FreeConnections.Add(connectionBase2);
									}
								}
								HTTPManager.RecycledConnections.Clear();
							}
						}
						finally
						{
							Monitor.Exit(HTTPManager.RecycledConnections);
						}
					}
					if (HTTPManager.FreeConnections.Count > 0)
					{
						for (int k = 0; k < HTTPManager.FreeConnections.Count; k++)
						{
							ConnectionBase connectionBase3 = HTTPManager.FreeConnections[k];
							if (connectionBase3.IsRemovable)
							{
								List<ConnectionBase> list = null;
								if (HTTPManager.Connections.TryGetValue(connectionBase3.ServerAddress, out list))
								{
									list.Remove(connectionBase3);
								}
								connectionBase3.Dispose();
								HTTPManager.FreeConnections.RemoveAt(k);
								k--;
							}
						}
					}
					if (HTTPManager.CanProcessFromQueue())
					{
						if (HTTPManager.RequestQueue.Find((HTTPRequest req) => req.Priority != 0) != null)
						{
							HTTPManager.RequestQueue.Sort((HTTPRequest req1, HTTPRequest req2) => req1.Priority - req2.Priority);
						}
						HTTPRequest[] array = HTTPManager.RequestQueue.ToArray();
						HTTPManager.RequestQueue.Clear();
						for (int l = 0; l < array.Length; l++)
						{
							HTTPManager.SendRequest(array[l]);
						}
					}
				}
				finally
				{
					Monitor.Exit(HTTPManager.Locker);
				}
			}
			if (HTTPManager.heartbeats != null)
			{
				HTTPManager.heartbeats.Update();
			}
			VariableSizedBufferPool.Maintain();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00094DE8 File Offset: 0x00092FE8
		public static void OnQuit()
		{
			object locker = HTTPManager.Locker;
			lock (locker)
			{
				HTTPManager.IsQuitting = true;
				HTTPCacheService.SaveLibrary();
				CookieJar.Persist();
				HTTPManager.AbortAll(true);
				HTTPManager.OnUpdate();
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00094E3C File Offset: 0x0009303C
		public static void AbortAll(bool allowCallbacks = false)
		{
			object locker = HTTPManager.Locker;
			lock (locker)
			{
				HTTPRequest[] array = HTTPManager.RequestQueue.ToArray();
				HTTPManager.RequestQueue.Clear();
				foreach (HTTPRequest httprequest in array)
				{
					try
					{
						if (!allowCallbacks)
						{
							httprequest.Callback = null;
						}
						httprequest.Abort();
					}
					catch
					{
					}
				}
				foreach (KeyValuePair<string, List<ConnectionBase>> keyValuePair in HTTPManager.Connections)
				{
					foreach (ConnectionBase connectionBase in keyValuePair.Value)
					{
						try
						{
							if (connectionBase.CurrentRequest != null)
							{
								if (!allowCallbacks)
								{
									connectionBase.CurrentRequest.Callback = null;
								}
								connectionBase.CurrentRequest.State = HTTPRequestStates.Aborted;
							}
							connectionBase.Abort(HTTPConnectionStates.Closed);
							connectionBase.Dispose();
						}
						catch
						{
						}
					}
					keyValuePair.Value.Clear();
				}
				HTTPManager.Connections.Clear();
			}
		}

		// Token: 0x04001199 RID: 4505
		private static byte maxConnectionPerServer;

		// Token: 0x040011A4 RID: 4516
		private static HeartbeatManager heartbeats;

		// Token: 0x040011A5 RID: 4517
		private static ILogger logger;

		// Token: 0x040011AA RID: 4522
		public static bool TryToMinimizeTCPLatency = false;

		// Token: 0x040011AB RID: 4523
		public static int SendBufferSize = 66560;

		// Token: 0x040011AC RID: 4524
		public static int ReceiveBufferSize = 66560;

		// Token: 0x040011AD RID: 4525
		public static IIOService IOService;

		// Token: 0x040011AF RID: 4527
		private static Dictionary<string, List<ConnectionBase>> Connections = new Dictionary<string, List<ConnectionBase>>();

		// Token: 0x040011B0 RID: 4528
		private static List<ConnectionBase> ActiveConnections = new List<ConnectionBase>();

		// Token: 0x040011B1 RID: 4529
		private static List<ConnectionBase> FreeConnections = new List<ConnectionBase>();

		// Token: 0x040011B2 RID: 4530
		private static List<ConnectionBase> RecycledConnections = new List<ConnectionBase>();

		// Token: 0x040011B3 RID: 4531
		private static List<HTTPRequest> RequestQueue = new List<HTTPRequest>();

		// Token: 0x040011B4 RID: 4532
		private static bool IsCallingCallbacks;

		// Token: 0x040011B5 RID: 4533
		internal static object Locker = new object();
	}
}
