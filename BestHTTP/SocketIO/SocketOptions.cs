using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.SocketIO.Transports;
using PlatformSupport.Collections.ObjectModel;
using PlatformSupport.Collections.Specialized;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001BF RID: 447
	public sealed class SocketOptions
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000A2D7C File Offset: 0x000A0F7C
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x000A2D84 File Offset: 0x000A0F84
		public TransportTypes ConnectWith { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000A2D8D File Offset: 0x000A0F8D
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x000A2D95 File Offset: 0x000A0F95
		public bool Reconnection { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000A2D9E File Offset: 0x000A0F9E
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x000A2DA6 File Offset: 0x000A0FA6
		public int ReconnectionAttempts { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x000A2DAF File Offset: 0x000A0FAF
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x000A2DB7 File Offset: 0x000A0FB7
		public TimeSpan ReconnectionDelay { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x000A2DC0 File Offset: 0x000A0FC0
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x000A2DC8 File Offset: 0x000A0FC8
		public TimeSpan ReconnectionDelayMax { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x000A2DD1 File Offset: 0x000A0FD1
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x000A2DD9 File Offset: 0x000A0FD9
		public float RandomizationFactor
		{
			get
			{
				return this.randomizationFactor;
			}
			set
			{
				this.randomizationFactor = Math.Min(1f, Math.Max(0f, value));
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000A2DF6 File Offset: 0x000A0FF6
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x000A2DFE File Offset: 0x000A0FFE
		public TimeSpan Timeout { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000A2E07 File Offset: 0x000A1007
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x000A2E0F File Offset: 0x000A100F
		public bool AutoConnect { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x000A2E18 File Offset: 0x000A1018
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x000A2E20 File Offset: 0x000A1020
		public ObservableDictionary<string, string> AdditionalQueryParams
		{
			get
			{
				return this.additionalQueryParams;
			}
			set
			{
				if (this.additionalQueryParams != null)
				{
					this.additionalQueryParams.CollectionChanged -= this.AdditionalQueryParams_CollectionChanged;
				}
				this.additionalQueryParams = value;
				this.BuiltQueryParams = null;
				if (value != null)
				{
					value.CollectionChanged += this.AdditionalQueryParams_CollectionChanged;
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x000A2E6F File Offset: 0x000A106F
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x000A2E77 File Offset: 0x000A1077
		public bool QueryParamsOnlyForHandshake { get; set; }

		// Token: 0x0600110B RID: 4363 RVA: 0x000A2E80 File Offset: 0x000A1080
		public SocketOptions()
		{
			this.ConnectWith = TransportTypes.Polling;
			this.Reconnection = true;
			this.ReconnectionAttempts = int.MaxValue;
			this.ReconnectionDelay = TimeSpan.FromMilliseconds(1000.0);
			this.ReconnectionDelayMax = TimeSpan.FromMilliseconds(5000.0);
			this.RandomizationFactor = 0.5f;
			this.Timeout = TimeSpan.FromMilliseconds(20000.0);
			this.AutoConnect = true;
			this.QueryParamsOnlyForHandshake = true;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000A2F04 File Offset: 0x000A1104
		internal string BuildQueryParams()
		{
			if (this.AdditionalQueryParams == null || this.AdditionalQueryParams.Count == 0)
			{
				return string.Empty;
			}
			if (!string.IsNullOrEmpty(this.BuiltQueryParams))
			{
				return this.BuiltQueryParams;
			}
			StringBuilder stringBuilder = new StringBuilder(this.AdditionalQueryParams.Count * 4);
			foreach (KeyValuePair<string, string> keyValuePair in this.AdditionalQueryParams)
			{
				stringBuilder.Append("&");
				stringBuilder.Append(keyValuePair.Key);
				if (!string.IsNullOrEmpty(keyValuePair.Value))
				{
					stringBuilder.Append("=");
					stringBuilder.Append(keyValuePair.Value);
				}
			}
			return this.BuiltQueryParams = stringBuilder.ToString();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000A2FE0 File Offset: 0x000A11E0
		private void AdditionalQueryParams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.BuiltQueryParams = null;
		}

		// Token: 0x04001393 RID: 5011
		private float randomizationFactor;

		// Token: 0x04001396 RID: 5014
		private ObservableDictionary<string, string> additionalQueryParams;

		// Token: 0x04001398 RID: 5016
		private string BuiltQueryParams;
	}
}
