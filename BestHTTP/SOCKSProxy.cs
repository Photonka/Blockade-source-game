﻿using System;
using System.IO;
using System.Net;
using System.Text;
using BestHTTP.Authentication;
using BestHTTP.Extensions;
using BestHTTP.Logger;

namespace BestHTTP
{
	// Token: 0x02000187 RID: 391
	public sealed class SOCKSProxy : Proxy
	{
		// Token: 0x06000E7C RID: 3708 RVA: 0x0009887C File Offset: 0x00096A7C
		public SOCKSProxy(Uri address, Credentials credentials) : base(address, credentials)
		{
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00098886 File Offset: 0x00096A86
		internal override string GetRequestPath(Uri uri)
		{
			return uri.GetRequestPathAndQueryURL();
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00098890 File Offset: 0x00096A90
		internal override void Connect(Stream stream, HTTPRequest request)
		{
			byte[] array = new byte[1024];
			int num = 0;
			array[num++] = 5;
			if (base.Credentials != null)
			{
				array[num++] = 2;
				array[num++] = 2;
				array[num++] = 0;
			}
			else
			{
				array[num++] = 1;
				array[num++] = 0;
			}
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Information("SOCKSProxy", string.Format("Sending method negotiation - count: {0} buffer: {1} ", num.ToString(), this.BufferToHexStr(array, num)));
			}
			stream.Write(array, 0, num);
			num = stream.Read(array, 0, array.Length);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Information("SOCKSProxy", string.Format("Negotiation response - count: {0} buffer: {1} ", num.ToString(), this.BufferToHexStr(array, num)));
			}
			SOCKSVersions socksversions = (SOCKSVersions)array[0];
			SOCKSMethods socksmethods = (SOCKSMethods)array[1];
			if (num != 2)
			{
				throw new Exception(string.Format("SOCKS Proxy - Expected read count: 2! count: {0} buffer: {1}" + num.ToString(), this.BufferToHexStr(array, num)));
			}
			if (socksversions != SOCKSVersions.V5)
			{
				throw new Exception("SOCKS Proxy - Expected version: 5, received version: " + array[0].ToString("X2"));
			}
			if (socksmethods == SOCKSMethods.NoAcceptableMethods)
			{
				throw new Exception("SOCKS Proxy - Received 'NO ACCEPTABLE METHODS' (0xFF)");
			}
			HTTPManager.Logger.Information("SOCKSProxy", "Method negotiation over. Method: " + socksmethods.ToString());
			switch (socksmethods)
			{
			case SOCKSMethods.NoAuthenticationRequired:
				break;
			case SOCKSMethods.GSSAPI:
				throw new Exception("SOCKS proxy: GSSAPI not supported!");
			case SOCKSMethods.UsernameAndPassword:
			{
				if (base.Credentials.UserName.Length > 255)
				{
					throw new Exception(string.Format("SOCKS Proxy - Credentials.UserName too long! {0} > 255", base.Credentials.UserName.Length.ToString()));
				}
				if (base.Credentials.Password.Length > 255)
				{
					throw new Exception(string.Format("SOCKS Proxy - Credentials.Password too long! {0} > 255", base.Credentials.Password.Length.ToString()));
				}
				HTTPManager.Logger.Information("SOCKSProxy", "starting sub-negotiation");
				num = 0;
				array[num++] = 1;
				this.WriteString(array, ref num, base.Credentials.UserName);
				this.WriteString(array, ref num, base.Credentials.Password);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					HTTPManager.Logger.Information("SOCKSProxy", string.Format("Sending username and password sub-negotiation - count: {0} buffer: {1} ", num.ToString(), this.BufferToHexStr(array, num)));
				}
				stream.Write(array, 0, num);
				num = stream.Read(array, 0, array.Length);
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					HTTPManager.Logger.Information("SOCKSProxy", string.Format("Username and password sub-negotiation response - count: {0} buffer: {1} ", num.ToString(), this.BufferToHexStr(array, num)));
				}
				bool flag = array[1] == 0;
				if (num != 2)
				{
					throw new Exception(string.Format("SOCKS Proxy - Expected read count: 2! count: {0} buffer: {1}" + num.ToString(), this.BufferToHexStr(array, num)));
				}
				if (!flag)
				{
					throw new Exception("SOCKS proxy: username+password authentication failed!");
				}
				HTTPManager.Logger.Information("SOCKSProxy", "Authenticated!");
				break;
			}
			default:
				if (socksmethods == SOCKSMethods.NoAcceptableMethods)
				{
					throw new Exception("SOCKS proxy: No acceptable method");
				}
				break;
			}
			num = 0;
			array[num++] = 5;
			array[num++] = 1;
			array[num++] = 0;
			if (request.CurrentUri.IsHostIsAnIPAddress())
			{
				bool flag2 = Extensions.IsIpV4AddressValid(request.CurrentUri.Host);
				array[num++] = (flag2 ? 0 : 4);
				byte[] addressBytes = IPAddress.Parse(request.CurrentUri.Host).GetAddressBytes();
				this.WriteBytes(array, ref num, addressBytes);
			}
			else
			{
				array[num++] = 3;
				this.WriteString(array, ref num, request.CurrentUri.Host);
			}
			array[num++] = (byte)(request.CurrentUri.Port >> 8 & 255);
			array[num++] = (byte)(request.CurrentUri.Port & 255);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Information("SOCKSProxy", string.Format("Sending connect request - count: {0} buffer: {1} ", num.ToString(), this.BufferToHexStr(array, num)));
			}
			stream.Write(array, 0, num);
			num = stream.Read(array, 0, array.Length);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Information("SOCKSProxy", string.Format("Connect response - count: {0} buffer: {1} ", num.ToString(), this.BufferToHexStr(array, num)));
			}
			socksversions = (SOCKSVersions)array[0];
			SOCKSReplies socksreplies = (SOCKSReplies)array[1];
			if (num < 10)
			{
				throw new Exception(string.Format("SOCKS proxy: not enough data returned by the server. Expected count is at least 10 bytes, server returned {0} bytes! content: {1}", num.ToString(), this.BufferToHexStr(array, num)));
			}
			if (socksreplies != SOCKSReplies.Succeeded)
			{
				throw new Exception("SOCKS proxy error: " + socksreplies.ToString());
			}
			HTTPManager.Logger.Information("SOCKSProxy", "Connected!");
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00098D5C File Offset: 0x00096F5C
		private void WriteString(byte[] buffer, ref int count, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			if (bytes.Length > 255)
			{
				throw new Exception(string.Format("SOCKS Proxy - String is too large ({0}) to fit in 255 bytes!", bytes.Length.ToString()));
			}
			int num = count;
			count = num + 1;
			buffer[num] = (byte)bytes.Length;
			Array.Copy(bytes, 0, buffer, count, bytes.Length);
			count += bytes.Length;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00098DBE File Offset: 0x00096FBE
		private void WriteBytes(byte[] buffer, ref int count, byte[] bytes)
		{
			Array.Copy(bytes, 0, buffer, count, bytes.Length);
			count += bytes.Length;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00098DD8 File Offset: 0x00096FD8
		private string BufferToHexStr(byte[] buffer, int count)
		{
			StringBuilder stringBuilder = new StringBuilder(count * 2);
			for (int i = 0; i < count; i++)
			{
				stringBuilder.AppendFormat("0x{0} ", buffer[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}
	}
}
