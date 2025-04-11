using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000425 RID: 1061
	public class NewSessionTicket
	{
		// Token: 0x06002A4C RID: 10828 RVA: 0x00114B84 File Offset: 0x00112D84
		public NewSessionTicket(long ticketLifetimeHint, byte[] ticket)
		{
			this.mTicketLifetimeHint = ticketLifetimeHint;
			this.mTicket = ticket;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00114B9A File Offset: 0x00112D9A
		public virtual long TicketLifetimeHint
		{
			get
			{
				return this.mTicketLifetimeHint;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x00114BA2 File Offset: 0x00112DA2
		public virtual byte[] Ticket
		{
			get
			{
				return this.mTicket;
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00114BAA File Offset: 0x00112DAA
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint32(this.mTicketLifetimeHint, output);
			TlsUtilities.WriteOpaque16(this.mTicket, output);
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00114BC4 File Offset: 0x00112DC4
		public static NewSessionTicket Parse(Stream input)
		{
			long ticketLifetimeHint = TlsUtilities.ReadUint32(input);
			byte[] ticket = TlsUtilities.ReadOpaque16(input);
			return new NewSessionTicket(ticketLifetimeHint, ticket);
		}

		// Token: 0x04001C60 RID: 7264
		protected readonly long mTicketLifetimeHint;

		// Token: 0x04001C61 RID: 7265
		protected readonly byte[] mTicket;
	}
}
