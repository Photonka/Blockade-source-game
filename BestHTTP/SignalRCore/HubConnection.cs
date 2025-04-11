using System;
using System.Collections.Generic;
using System.Threading;
using BestHTTP.Extensions;
using BestHTTP.Futures;
using BestHTTP.SignalRCore.Authentication;
using BestHTTP.SignalRCore.Messages;
using BestHTTP.SignalRCore.Transports;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D7 RID: 471
	public sealed class HubConnection : IHeartbeat
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000A4986 File Offset: 0x000A2B86
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x000A498E File Offset: 0x000A2B8E
		public Uri Uri { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000A4997 File Offset: 0x000A2B97
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x000A499F File Offset: 0x000A2B9F
		public ConnectionStates State { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000A49A8 File Offset: 0x000A2BA8
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x000A49B0 File Offset: 0x000A2BB0
		public ITransport Transport { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000A49B9 File Offset: 0x000A2BB9
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x000A49C1 File Offset: 0x000A2BC1
		public IProtocol Protocol { get; private set; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600118F RID: 4495 RVA: 0x000A49CC File Offset: 0x000A2BCC
		// (remove) Token: 0x06001190 RID: 4496 RVA: 0x000A4A04 File Offset: 0x000A2C04
		public event Action<HubConnection, Uri, Uri> OnRedirected;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001191 RID: 4497 RVA: 0x000A4A3C File Offset: 0x000A2C3C
		// (remove) Token: 0x06001192 RID: 4498 RVA: 0x000A4A74 File Offset: 0x000A2C74
		public event Action<HubConnection> OnConnected;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001193 RID: 4499 RVA: 0x000A4AAC File Offset: 0x000A2CAC
		// (remove) Token: 0x06001194 RID: 4500 RVA: 0x000A4AE4 File Offset: 0x000A2CE4
		public event Action<HubConnection, string> OnError;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001195 RID: 4501 RVA: 0x000A4B1C File Offset: 0x000A2D1C
		// (remove) Token: 0x06001196 RID: 4502 RVA: 0x000A4B54 File Offset: 0x000A2D54
		public event Action<HubConnection> OnClosed;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001197 RID: 4503 RVA: 0x000A4B8C File Offset: 0x000A2D8C
		// (remove) Token: 0x06001198 RID: 4504 RVA: 0x000A4BC4 File Offset: 0x000A2DC4
		public event Func<HubConnection, Message, bool> OnMessage;

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x000A4BF9 File Offset: 0x000A2DF9
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x000A4C01 File Offset: 0x000A2E01
		public IAuthenticationProvider AuthenticationProvider { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x000A4C0A File Offset: 0x000A2E0A
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x000A4C12 File Offset: 0x000A2E12
		public NegotiationResult NegotiationResult { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x000A4C1B File Offset: 0x000A2E1B
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x000A4C23 File Offset: 0x000A2E23
		public HubOptions Options { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x000A4C2C File Offset: 0x000A2E2C
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x000A4C34 File Offset: 0x000A2E34
		public int RedirectCount { get; private set; }

		// Token: 0x060011A1 RID: 4513 RVA: 0x000A4C3D File Offset: 0x000A2E3D
		public HubConnection(Uri hubUri, IProtocol protocol) : this(hubUri, protocol, new HubOptions())
		{
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000A4C4C File Offset: 0x000A2E4C
		public HubConnection(Uri hubUri, IProtocol protocol, HubOptions options)
		{
			this.Uri = hubUri;
			this.State = ConnectionStates.Initial;
			this.Options = options;
			this.Protocol = protocol;
			this.Protocol.Connection = this;
			this.AuthenticationProvider = new DefaultAccessTokenAuthenticator(this);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000A4CB0 File Offset: 0x000A2EB0
		public void StartConnect()
		{
			if (this.State != ConnectionStates.Initial && this.State != ConnectionStates.Redirected)
			{
				HTTPManager.Logger.Warning("HubConnection", "StartConnect - Expected Initial or Redirected state, got " + this.State.ToString());
				return;
			}
			HTTPManager.Logger.Verbose("HubConnection", "StartConnect");
			if (this.AuthenticationProvider != null && this.AuthenticationProvider.IsPreAuthRequired)
			{
				HTTPManager.Logger.Information("HubConnection", "StartConnect - Authenticating");
				this.SetState(ConnectionStates.Authenticating, null);
				this.AuthenticationProvider.OnAuthenticationSucceded += this.OnAuthenticationSucceded;
				this.AuthenticationProvider.OnAuthenticationFailed += this.OnAuthenticationFailed;
				this.AuthenticationProvider.StartAuthentication();
				return;
			}
			this.StartNegotiation();
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000A4D84 File Offset: 0x000A2F84
		private void OnAuthenticationSucceded(IAuthenticationProvider provider)
		{
			HTTPManager.Logger.Verbose("HubConnection", "OnAuthenticationSucceded");
			this.AuthenticationProvider.OnAuthenticationSucceded -= this.OnAuthenticationSucceded;
			this.AuthenticationProvider.OnAuthenticationFailed -= this.OnAuthenticationFailed;
			this.StartNegotiation();
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000A4DDC File Offset: 0x000A2FDC
		private void OnAuthenticationFailed(IAuthenticationProvider provider, string reason)
		{
			HTTPManager.Logger.Error("HubConnection", "OnAuthenticationFailed: " + reason);
			this.AuthenticationProvider.OnAuthenticationSucceded -= this.OnAuthenticationSucceded;
			this.AuthenticationProvider.OnAuthenticationFailed -= this.OnAuthenticationFailed;
			this.SetState(ConnectionStates.Closed, reason);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000A4E3C File Offset: 0x000A303C
		private void StartNegotiation()
		{
			HTTPManager.Logger.Verbose("HubConnection", "StartNegotiation");
			if (this.State == ConnectionStates.CloseInitiated)
			{
				this.SetState(ConnectionStates.Closed, null);
				return;
			}
			if (this.Options.SkipNegotiation)
			{
				HTTPManager.Logger.Verbose("HubConnection", "Skipping negotiation");
				this.ConnectImpl();
				return;
			}
			this.SetState(ConnectionStates.Negotiating, null);
			UriBuilder uriBuilder = new UriBuilder(this.Uri);
			uriBuilder.Path += "/negotiate";
			HTTPRequest httprequest = new HTTPRequest(uriBuilder.Uri, HTTPMethods.Post, new OnRequestFinishedDelegate(this.OnNegotiationRequestFinished));
			if (this.AuthenticationProvider != null)
			{
				this.AuthenticationProvider.PrepareRequest(httprequest);
			}
			httprequest.Send();
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000A4EF4 File Offset: 0x000A30F4
		private void ConnectImpl()
		{
			HTTPManager.Logger.Verbose("HubConnection", "ConnectImpl");
			if (this.Options.PreferedTransport == TransportTypes.WebSocket)
			{
				if (this.NegotiationResult != null && !this.IsTransportSupported("WebSockets"))
				{
					this.SetState(ConnectionStates.Closed, "The 'WebSockets' transport isn't supported by the server!");
					return;
				}
				this.Transport = new WebSocketTransport(this);
				this.Transport.OnStateChanged += this.Transport_OnStateChanged;
			}
			else
			{
				this.SetState(ConnectionStates.Closed, "Unsupportted transport: " + this.Options.PreferedTransport);
			}
			this.Transport.StartConnect();
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000A4F98 File Offset: 0x000A3198
		private bool IsTransportSupported(string transportName)
		{
			if (this.NegotiationResult.SupportedTransports == null)
			{
				return true;
			}
			for (int i = 0; i < this.NegotiationResult.SupportedTransports.Count; i++)
			{
				if (this.NegotiationResult.SupportedTransports[i].Name == transportName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000A4FF0 File Offset: 0x000A31F0
		private void OnNegotiationRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			if (this.State == ConnectionStates.CloseInitiated)
			{
				this.SetState(ConnectionStates.Closed, null);
				return;
			}
			string text = null;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess)
				{
					HTTPManager.Logger.Information("HubConnection", "Negotiation Request Finished Successfully! Response: " + resp.DataAsText);
					this.NegotiationResult = NegotiationResult.Parse(resp.DataAsText, out text, this);
					if (string.IsNullOrEmpty(text))
					{
						if (this.NegotiationResult.Url != null)
						{
							this.SetState(ConnectionStates.Redirected, null);
							int num = this.RedirectCount + 1;
							this.RedirectCount = num;
							if (num >= this.Options.MaxRedirects)
							{
								text = string.Format("MaxRedirects ({0:N0}) reached!", this.Options.MaxRedirects);
							}
							else
							{
								Uri uri = this.Uri;
								this.Uri = this.NegotiationResult.Url;
								if (this.OnRedirected != null)
								{
									try
									{
										this.OnRedirected(this, uri, this.Uri);
									}
									catch (Exception ex)
									{
										HTTPManager.Logger.Exception("HubConnection", "OnNegotiationRequestFinished - OnRedirected", ex);
									}
								}
								this.StartConnect();
							}
						}
						else
						{
							this.ConnectImpl();
						}
					}
				}
				else
				{
					text = string.Format("Negotiation Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Negotiation Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.Aborted:
				text = "Negotiation Request Aborted!";
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Negotiation Request - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Negotiation Request - Processing the request Timed Out!";
				break;
			}
			if (text != null)
			{
				this.SetState(ConnectionStates.Closed, text);
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000A51D0 File Offset: 0x000A33D0
		public void StartClose()
		{
			HTTPManager.Logger.Verbose("HubConnection", "StartClose");
			this.SetState(ConnectionStates.CloseInitiated, null);
			if (this.Transport != null)
			{
				this.Transport.StartClose();
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000A5204 File Offset: 0x000A3404
		public IFuture<StreamItemContainer<TResult>> Stream<TResult>(string target, params object[] args)
		{
			Future<StreamItemContainer<TResult>> future = new Future<StreamItemContainer<TResult>>();
			long id = this.InvokeImp(target, args, delegate(Message message)
			{
				MessageTypes type = message.type;
				if (type != MessageTypes.StreamItem)
				{
					if (type != MessageTypes.Completion)
					{
						return;
					}
					if (string.IsNullOrEmpty(message.error))
					{
						StreamItemContainer<TResult> value = future.value;
						future.Assign(value);
						return;
					}
					future.Fail(new Exception(message.error));
				}
				else
				{
					StreamItemContainer<TResult> value2 = future.value;
					if (!value2.IsCanceled)
					{
						value2.AddItem((TResult)((object)this.Protocol.ConvertTo(typeof(TResult), message.item)));
						future.AssignItem(value2);
						return;
					}
				}
			}, true);
			future.BeginProcess(new StreamItemContainer<TResult>(id));
			return future;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000A5258 File Offset: 0x000A3458
		public void CancelStream<T>(StreamItemContainer<T> container)
		{
			Message message = new Message
			{
				type = MessageTypes.CancelInvocation,
				invocationId = container.id.ToString()
			};
			container.IsCanceled = true;
			this.SendMessage(message);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000A529C File Offset: 0x000A349C
		public IFuture<TResult> Invoke<TResult>(string target, params object[] args)
		{
			Future<TResult> future = new Future<TResult>();
			this.InvokeImp(target, args, delegate(Message message)
			{
				if (string.IsNullOrEmpty(message.error))
				{
					future.Assign((TResult)((object)this.Protocol.ConvertTo(typeof(TResult), message.result)));
					return;
				}
				future.Fail(new Exception(message.error));
			}, false);
			return future;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000A52E0 File Offset: 0x000A34E0
		public IFuture<bool> Send(string target, params object[] args)
		{
			Future<bool> future = new Future<bool>();
			this.InvokeImp(target, args, delegate(Message message)
			{
				if (string.IsNullOrEmpty(message.error))
				{
					future.Assign(true);
					return;
				}
				future.Fail(new Exception(message.error));
			}, false);
			return future;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000A531C File Offset: 0x000A351C
		private long InvokeImp(string target, object[] args, Action<Message> callback, bool isStreamingInvocation = false)
		{
			if (this.State != ConnectionStates.Connected)
			{
				return -1L;
			}
			long num = Interlocked.Increment(ref this.lastInvocationId);
			Message message = new Message
			{
				type = (isStreamingInvocation ? MessageTypes.StreamInvocation : MessageTypes.Invocation),
				invocationId = num.ToString(),
				target = target,
				arguments = args,
				nonblocking = (callback == null)
			};
			this.SendMessage(message);
			if (callback != null)
			{
				this.invocations.Add(num, callback);
			}
			return num;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000A539C File Offset: 0x000A359C
		private void SendMessage(Message message)
		{
			byte[] msg = this.Protocol.EncodeMessage(message);
			this.Transport.Send(msg);
			this.lastMessageSent = DateTime.UtcNow;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000A53D0 File Offset: 0x000A35D0
		public void On(string methodName, Action callback)
		{
			this.On(methodName, null, delegate(object[] args)
			{
				callback();
			});
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000A5400 File Offset: 0x000A3600
		public void On<T1>(string methodName, Action<T1> callback)
		{
			this.On(methodName, new Type[]
			{
				typeof(T1)
			}, delegate(object[] args)
			{
				callback((T1)((object)args[0]));
			});
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000A5440 File Offset: 0x000A3640
		public void On<T1, T2>(string methodName, Action<T1, T2> callback)
		{
			this.On(methodName, new Type[]
			{
				typeof(T1),
				typeof(T2)
			}, delegate(object[] args)
			{
				callback((T1)((object)args[0]), (T2)((object)args[1]));
			});
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000A5490 File Offset: 0x000A3690
		public void On<T1, T2, T3>(string methodName, Action<T1, T2, T3> callback)
		{
			this.On(methodName, new Type[]
			{
				typeof(T1),
				typeof(T2),
				typeof(T3)
			}, delegate(object[] args)
			{
				callback((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			});
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000A54EC File Offset: 0x000A36EC
		public void On<T1, T2, T3, T4>(string methodName, Action<T1, T2, T3, T4> callback)
		{
			this.On(methodName, new Type[]
			{
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4)
			}, delegate(object[] args)
			{
				callback((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			});
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000A5554 File Offset: 0x000A3754
		public void On(string methodName, Type[] paramTypes, Action<object[]> callback)
		{
			Subscription subscription = null;
			if (!this.subscriptions.TryGetValue(methodName, out subscription))
			{
				this.subscriptions.Add(methodName, subscription = new Subscription());
			}
			subscription.Add(paramTypes, callback);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000A5590 File Offset: 0x000A3790
		internal void OnMessages(List<Message> messages)
		{
			int i = 0;
			while (i < messages.Count)
			{
				Message message = messages[i];
				try
				{
					if (this.OnMessage != null && !this.OnMessage(this, message))
					{
						break;
					}
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("HubConnection", "Exception in OnMessage user code!", ex);
				}
				switch (message.type)
				{
				case MessageTypes.Invocation:
				{
					Subscription subscription = null;
					if (this.subscriptions.TryGetValue(message.target, out subscription))
					{
						for (int j = 0; j < subscription.callbacks.Count; j++)
						{
							CallbackDescriptor callbackDescriptor = subscription.callbacks[j];
							object[] obj = null;
							try
							{
								obj = this.Protocol.GetRealArguments(callbackDescriptor.ParamTypes, message.arguments);
							}
							catch (Exception ex2)
							{
								HTTPManager.Logger.Exception("HubConnection", "OnMessages - Invocation - GetRealArguments", ex2);
							}
							try
							{
								callbackDescriptor.Callback(obj);
							}
							catch (Exception ex3)
							{
								HTTPManager.Logger.Exception("HubConnection", "OnMessages - Invocation - Invoke", ex3);
							}
						}
					}
					break;
				}
				case MessageTypes.StreamItem:
				{
					long key;
					Action<Message> action;
					if (long.TryParse(message.invocationId, out key) && this.invocations.TryGetValue(key, out action) && action != null)
					{
						try
						{
							action(message);
							break;
						}
						catch (Exception ex4)
						{
							HTTPManager.Logger.Exception("HubConnection", "OnMessages - StreamItem - callback", ex4);
							break;
						}
						goto IL_178;
					}
					break;
				}
				case MessageTypes.Completion:
					goto IL_178;
				case MessageTypes.Close:
					this.SetState(ConnectionStates.Closed, message.error);
					break;
				}
				IL_1DD:
				i++;
				continue;
				IL_178:
				long key2;
				if (long.TryParse(message.invocationId, out key2))
				{
					Action<Message> action2;
					if (this.invocations.TryGetValue(key2, out action2) && action2 != null)
					{
						try
						{
							action2(message);
						}
						catch (Exception ex5)
						{
							HTTPManager.Logger.Exception("HubConnection", "OnMessages - Completion - callback", ex5);
						}
					}
					this.invocations.Remove(key2);
					goto IL_1DD;
				}
				goto IL_1DD;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000A57CC File Offset: 0x000A39CC
		private void Transport_OnStateChanged(TransportStates oldState, TransportStates newState)
		{
			HTTPManager.Logger.Verbose("HubConnection", string.Format("Transport_OnStateChanged - oldState: {0} newState: {1}", oldState.ToString(), newState.ToString()));
			switch (newState)
			{
			case TransportStates.Connected:
				this.SetState(ConnectionStates.Connected, null);
				return;
			case TransportStates.Closing:
				break;
			case TransportStates.Failed:
				this.SetState(ConnectionStates.Closed, this.Transport.ErrorReason);
				return;
			case TransportStates.Closed:
				this.SetState(ConnectionStates.Closed, null);
				break;
			default:
				return;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000A584C File Offset: 0x000A3A4C
		private void SetState(ConnectionStates state, string errorReason = null)
		{
			if (string.IsNullOrEmpty(errorReason))
			{
				HTTPManager.Logger.Information("HubConnection", string.Concat(new string[]
				{
					"SetState - from State: '",
					this.State.ToString(),
					"' to State: '",
					state.ToString(),
					"'"
				}));
			}
			else
			{
				HTTPManager.Logger.Information("HubConnection", string.Concat(new string[]
				{
					"SetState - from State: '",
					this.State.ToString(),
					"' to State: '",
					state.ToString(),
					"' errorReason: '",
					errorReason,
					"'"
				}));
			}
			if (this.State == state)
			{
				return;
			}
			this.State = state;
			switch (state)
			{
			case ConnectionStates.Initial:
			case ConnectionStates.Authenticating:
			case ConnectionStates.Negotiating:
			case ConnectionStates.Redirected:
			case ConnectionStates.CloseInitiated:
				break;
			case ConnectionStates.Connected:
				try
				{
					if (this.OnConnected != null)
					{
						this.OnConnected(this);
					}
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("HubConnection", "Exception in OnConnected user code!", ex);
				}
				HTTPManager.Heartbeats.Subscribe(this);
				this.lastMessageSent = DateTime.UtcNow;
				return;
			case ConnectionStates.Closed:
				if (string.IsNullOrEmpty(errorReason))
				{
					if (this.OnClosed == null)
					{
						goto IL_1A7;
					}
					try
					{
						this.OnClosed(this);
						goto IL_1A7;
					}
					catch (Exception ex2)
					{
						HTTPManager.Logger.Exception("HubConnection", "Exception in OnClosed user code!", ex2);
						goto IL_1A7;
					}
				}
				if (this.OnError != null)
				{
					try
					{
						this.OnError(this, errorReason);
					}
					catch (Exception ex3)
					{
						HTTPManager.Logger.Exception("HubConnection", "Exception in OnError user code!", ex3);
					}
				}
				IL_1A7:
				HTTPManager.Heartbeats.Unsubscribe(this);
				break;
			default:
				return;
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000A5A34 File Offset: 0x000A3C34
		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			ConnectionStates state = this.State;
			if (state == ConnectionStates.Connected && this.Options.PingInterval != TimeSpan.Zero && DateTime.UtcNow - this.lastMessageSent >= this.Options.PingInterval)
			{
				this.SendMessage(new Message
				{
					type = MessageTypes.Ping
				});
			}
		}

		// Token: 0x040013DC RID: 5084
		public static readonly object[] EmptyArgs = new object[0];

		// Token: 0x040013EA RID: 5098
		private long lastInvocationId;

		// Token: 0x040013EB RID: 5099
		private Dictionary<long, Action<Message>> invocations = new Dictionary<long, Action<Message>>();

		// Token: 0x040013EC RID: 5100
		private Dictionary<string, Subscription> subscriptions = new Dictionary<string, Subscription>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040013ED RID: 5101
		private DateTime lastMessageSent;
	}
}
