using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BestHTTP.PlatformSupport.TcpClient.General
{
	// Token: 0x020007BF RID: 1983
	public class TcpClient : IDisposable
	{
		// Token: 0x060046C6 RID: 18118 RVA: 0x00196F58 File Offset: 0x00195158
		private void Init(AddressFamily family)
		{
			this.active = false;
			if (this.client != null)
			{
				this.client.Close();
				this.client = null;
			}
			this.client = new Socket(family, SocketType.Stream, ProtocolType.Tcp);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x00196F89 File Offset: 0x00195189
		public TcpClient()
		{
			this.Init(AddressFamily.InterNetwork);
			this.ConnectTimeout = TimeSpan.FromSeconds(2.0);
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x00196FAC File Offset: 0x001951AC
		public TcpClient(AddressFamily family)
		{
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException("Family must be InterNetwork or InterNetworkV6", "family");
			}
			this.Init(family);
			this.ConnectTimeout = TimeSpan.FromSeconds(2.0);
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x00196FE8 File Offset: 0x001951E8
		public TcpClient(IPEndPoint localEP)
		{
			this.Init(localEP.AddressFamily);
			this.ConnectTimeout = TimeSpan.FromSeconds(2.0);
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x00197010 File Offset: 0x00195210
		public TcpClient(string hostname, int port)
		{
			this.ConnectTimeout = TimeSpan.FromSeconds(2.0);
			this.Connect(hostname, port);
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060046CB RID: 18123 RVA: 0x00197034 File Offset: 0x00195234
		// (set) Token: 0x060046CC RID: 18124 RVA: 0x0019703C File Offset: 0x0019523C
		protected bool Active
		{
			get
			{
				return this.active;
			}
			set
			{
				this.active = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060046CD RID: 18125 RVA: 0x00197045 File Offset: 0x00195245
		// (set) Token: 0x060046CE RID: 18126 RVA: 0x0019704D File Offset: 0x0019524D
		public Socket Client
		{
			get
			{
				return this.client;
			}
			set
			{
				this.client = value;
				this.stream = null;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x0019705D File Offset: 0x0019525D
		public int Available
		{
			get
			{
				return this.client.Available;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x0019706A File Offset: 0x0019526A
		public bool Connected
		{
			get
			{
				return this.client.Connected;
			}
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00197078 File Offset: 0x00195278
		public bool IsConnected()
		{
			bool result;
			try
			{
				result = (!this.Client.Poll(1, SelectMode.SelectRead) || this.Client.Available != 0);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x001970C0 File Offset: 0x001952C0
		// (set) Token: 0x060046D3 RID: 18131 RVA: 0x001970CD File Offset: 0x001952CD
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.client.ExclusiveAddressUse;
			}
			set
			{
				this.client.ExclusiveAddressUse = value;
			}
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x001970DB File Offset: 0x001952DB
		internal void SetTcpClient(Socket s)
		{
			this.Client = s;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x001970E4 File Offset: 0x001952E4
		// (set) Token: 0x060046D6 RID: 18134 RVA: 0x00197111 File Offset: 0x00195311
		public LingerOption LingerState
		{
			get
			{
				if ((this.values & TcpClient.Properties.LingerState) != (TcpClient.Properties)0U)
				{
					return this.linger_state;
				}
				return (LingerOption)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				if (!this.client.Connected)
				{
					this.linger_state = value;
					this.values |= TcpClient.Properties.LingerState;
					return;
				}
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x0019714C File Offset: 0x0019534C
		// (set) Token: 0x060046D8 RID: 18136 RVA: 0x00197171 File Offset: 0x00195371
		public bool NoDelay
		{
			get
			{
				if ((this.values & TcpClient.Properties.NoDelay) != (TcpClient.Properties)0U)
				{
					return this.no_delay;
				}
				return (bool)this.client.GetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug);
			}
			set
			{
				if (!this.client.Connected)
				{
					this.no_delay = value;
					this.values |= TcpClient.Properties.NoDelay;
					return;
				}
				this.client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, value ? 1 : 0);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x001971AA File Offset: 0x001953AA
		// (set) Token: 0x060046DA RID: 18138 RVA: 0x001971D7 File Offset: 0x001953D7
		public int ReceiveBufferSize
		{
			get
			{
				if ((this.values & TcpClient.Properties.ReceiveBufferSize) != (TcpClient.Properties)0U)
				{
					return this.recv_buffer_size;
				}
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				if (!this.client.Connected)
				{
					this.recv_buffer_size = value;
					this.values |= TcpClient.Properties.ReceiveBufferSize;
					return;
				}
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x00197212 File Offset: 0x00195412
		// (set) Token: 0x060046DC RID: 18140 RVA: 0x0019723F File Offset: 0x0019543F
		public int ReceiveTimeout
		{
			get
			{
				if ((this.values & TcpClient.Properties.ReceiveTimeout) != (TcpClient.Properties)0U)
				{
					return this.recv_timeout;
				}
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				if (!this.client.Connected)
				{
					this.recv_timeout = value;
					this.values |= TcpClient.Properties.ReceiveTimeout;
					return;
				}
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x0019727A File Offset: 0x0019547A
		// (set) Token: 0x060046DE RID: 18142 RVA: 0x001972A8 File Offset: 0x001954A8
		public int SendBufferSize
		{
			get
			{
				if ((this.values & TcpClient.Properties.SendBufferSize) != (TcpClient.Properties)0U)
				{
					return this.send_buffer_size;
				}
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				if (!this.client.Connected)
				{
					this.send_buffer_size = value;
					this.values |= TcpClient.Properties.SendBufferSize;
					return;
				}
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x001972E4 File Offset: 0x001954E4
		// (set) Token: 0x060046E0 RID: 18144 RVA: 0x00197312 File Offset: 0x00195512
		public int SendTimeout
		{
			get
			{
				if ((this.values & TcpClient.Properties.SendTimeout) != (TcpClient.Properties)0U)
				{
					return this.send_timeout;
				}
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				if (!this.client.Connected)
				{
					this.send_timeout = value;
					this.values |= TcpClient.Properties.SendTimeout;
					return;
				}
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x0019734E File Offset: 0x0019554E
		// (set) Token: 0x060046E2 RID: 18146 RVA: 0x00197356 File Offset: 0x00195556
		public TimeSpan ConnectTimeout { get; set; }

		// Token: 0x060046E3 RID: 18147 RVA: 0x0019735F File Offset: 0x0019555F
		public void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x00197368 File Offset: 0x00195568
		public void Connect(IPEndPoint remoteEP)
		{
			try
			{
				if (this.ConnectTimeout > TimeSpan.Zero)
				{
					ManualResetEvent mre = new ManualResetEvent(false);
					IAsyncResult asyncResult = this.client.BeginConnect(remoteEP, delegate(IAsyncResult res)
					{
						mre.Set();
					}, null);
					this.active = mre.WaitOne(this.ConnectTimeout);
					if (!this.active)
					{
						try
						{
							this.client.Disconnect(true);
						}
						catch
						{
						}
						throw new TimeoutException("Connection timed out!");
					}
					this.client.EndConnect(asyncResult);
				}
				else
				{
					this.client.Connect(remoteEP);
					this.active = true;
				}
			}
			finally
			{
				this.CheckDisposed();
			}
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x00197438 File Offset: 0x00195638
		public void Connect(IPAddress address, int port)
		{
			this.Connect(new IPEndPoint(address, port));
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x00197448 File Offset: 0x00195648
		private void SetOptions()
		{
			TcpClient.Properties properties = this.values;
			this.values = (TcpClient.Properties)0U;
			if ((properties & TcpClient.Properties.LingerState) != (TcpClient.Properties)0U)
			{
				this.LingerState = this.linger_state;
			}
			if ((properties & TcpClient.Properties.NoDelay) != (TcpClient.Properties)0U)
			{
				this.NoDelay = this.no_delay;
			}
			if ((properties & TcpClient.Properties.ReceiveBufferSize) != (TcpClient.Properties)0U)
			{
				this.ReceiveBufferSize = this.recv_buffer_size;
			}
			if ((properties & TcpClient.Properties.ReceiveTimeout) != (TcpClient.Properties)0U)
			{
				this.ReceiveTimeout = this.recv_timeout;
			}
			if ((properties & TcpClient.Properties.SendBufferSize) != (TcpClient.Properties)0U)
			{
				this.SendBufferSize = this.send_buffer_size;
			}
			if ((properties & TcpClient.Properties.SendTimeout) != (TcpClient.Properties)0U)
			{
				this.SendTimeout = this.send_timeout;
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x001974CC File Offset: 0x001956CC
		public void Connect(string hostname, int port)
		{
			if (!(this.ConnectTimeout > TimeSpan.Zero))
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
				this.Connect(hostAddresses, port);
				return;
			}
			ManualResetEvent mre = new ManualResetEvent(false);
			IAsyncResult asyncResult = Dns.BeginGetHostAddresses(hostname, delegate(IAsyncResult res)
			{
				mre.Set();
			}, null);
			if (mre.WaitOne(this.ConnectTimeout))
			{
				IPAddress[] ipAddresses = Dns.EndGetHostAddresses(asyncResult);
				this.Connect(ipAddresses, port);
				return;
			}
			throw new TimeoutException("DNS resolve timed out!");
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x00197550 File Offset: 0x00195750
		public void Connect(IPAddress[] ipAddresses, int port)
		{
			this.CheckDisposed();
			if (ipAddresses == null)
			{
				throw new ArgumentNullException("ipAddresses");
			}
			for (int i = 0; i < ipAddresses.Length; i++)
			{
				try
				{
					IPAddress ipaddress = ipAddresses[i];
					if (ipaddress.Equals(IPAddress.Any) || ipaddress.Equals(IPAddress.IPv6Any))
					{
						throw new SocketException(10049);
					}
					this.Init(ipaddress.AddressFamily);
					if (ipaddress.AddressFamily != AddressFamily.InterNetwork && ipaddress.AddressFamily != AddressFamily.InterNetworkV6)
					{
						throw new NotSupportedException("This method is only valid for sockets in the InterNetwork and InterNetworkV6 families");
					}
					this.Connect(new IPEndPoint(ipaddress, port));
					if (this.values != (TcpClient.Properties)0U)
					{
						this.SetOptions();
					}
					this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
					HTTPManager.Logger.Information("TcpClient", string.Format("Connected to {0}:{1}", ipaddress.ToString(), port.ToString()));
					break;
				}
				catch (Exception ex)
				{
					this.Init(AddressFamily.InterNetwork);
					if (i == ipAddresses.Length - 1)
					{
						throw ex;
					}
				}
			}
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x00197654 File Offset: 0x00195854
		public void EndConnect(IAsyncResult asyncResult)
		{
			this.client.EndConnect(asyncResult);
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00197662 File Offset: 0x00195862
		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
		{
			return this.client.BeginConnect(address, port, requestCallback, state);
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x00197674 File Offset: 0x00195874
		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state)
		{
			return this.client.BeginConnect(addresses, port, requestCallback, state);
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00197686 File Offset: 0x00195886
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
		{
			return this.client.BeginConnect(host, port, requestCallback, state);
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00197698 File Offset: 0x00195898
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x001976A8 File Offset: 0x001958A8
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (disposing)
			{
				NetworkStream networkStream = this.stream;
				this.stream = null;
				if (networkStream != null)
				{
					networkStream.Close();
					this.active = false;
					return;
				}
				if (this.client != null)
				{
					this.client.Close();
					this.client = null;
				}
			}
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00197704 File Offset: 0x00195904
		~TcpClient()
		{
			this.Dispose(false);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00197734 File Offset: 0x00195934
		public Stream GetStream()
		{
			Stream result;
			try
			{
				if (this.stream == null)
				{
					this.stream = new NetworkStream(this.client, true);
				}
				result = this.stream;
			}
			finally
			{
				this.CheckDisposed();
			}
			return result;
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x0019777C File Offset: 0x0019597C
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04002D67 RID: 11623
		private NetworkStream stream;

		// Token: 0x04002D68 RID: 11624
		private bool active;

		// Token: 0x04002D69 RID: 11625
		private Socket client;

		// Token: 0x04002D6A RID: 11626
		private bool disposed;

		// Token: 0x04002D6B RID: 11627
		private TcpClient.Properties values;

		// Token: 0x04002D6C RID: 11628
		private int recv_timeout;

		// Token: 0x04002D6D RID: 11629
		private int send_timeout;

		// Token: 0x04002D6E RID: 11630
		private int recv_buffer_size;

		// Token: 0x04002D6F RID: 11631
		private int send_buffer_size;

		// Token: 0x04002D70 RID: 11632
		private LingerOption linger_state;

		// Token: 0x04002D71 RID: 11633
		private bool no_delay;

		// Token: 0x020009B4 RID: 2484
		private enum Properties : uint
		{
			// Token: 0x0400366D RID: 13933
			LingerState = 1U,
			// Token: 0x0400366E RID: 13934
			NoDelay,
			// Token: 0x0400366F RID: 13935
			ReceiveBufferSize = 4U,
			// Token: 0x04003670 RID: 13936
			ReceiveTimeout = 8U,
			// Token: 0x04003671 RID: 13937
			SendBufferSize = 16U,
			// Token: 0x04003672 RID: 13938
			SendTimeout = 32U
		}
	}
}
