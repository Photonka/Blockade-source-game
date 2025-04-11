using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000291 RID: 657
	public class TimeStampResponseGenerator
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x000BBEAC File Offset: 0x000BA0AC
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms) : this(tokenGenerator, acceptedAlgorithms, null, null)
		{
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000BBEB8 File Offset: 0x000BA0B8
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms, IList acceptedPolicy) : this(tokenGenerator, acceptedAlgorithms, acceptedPolicy, null)
		{
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000BBEC4 File Offset: 0x000BA0C4
		public TimeStampResponseGenerator(TimeStampTokenGenerator tokenGenerator, IList acceptedAlgorithms, IList acceptedPolicies, IList acceptedExtensions)
		{
			this.tokenGenerator = tokenGenerator;
			this.acceptedAlgorithms = acceptedAlgorithms;
			this.acceptedPolicies = acceptedPolicies;
			this.acceptedExtensions = acceptedExtensions;
			this.statusStrings = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000BBEF9 File Offset: 0x000BA0F9
		private void AddStatusString(string statusString)
		{
			this.statusStrings.Add(new Asn1Encodable[]
			{
				new DerUtf8String(statusString)
			});
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000BBF15 File Offset: 0x000BA115
		private void SetFailInfoField(int field)
		{
			this.failInfo |= field;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000BBF28 File Offset: 0x000BA128
		private PkiStatusInfo GetPkiStatusInfo()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger((int)this.status)
			});
			if (this.statusStrings.Count > 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new PkiFreeText(new DerSequence(this.statusStrings))
				});
			}
			if (this.failInfo != 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new TimeStampResponseGenerator.FailInfo(this.failInfo)
				});
			}
			return new PkiStatusInfo(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000BBFA9 File Offset: 0x000BA1A9
		public TimeStampResponse Generate(TimeStampRequest request, BigInteger serialNumber, DateTime genTime)
		{
			return this.Generate(request, serialNumber, new DateTimeObject(genTime));
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000BBFBC File Offset: 0x000BA1BC
		public TimeStampResponse Generate(TimeStampRequest request, BigInteger serialNumber, DateTimeObject genTime)
		{
			TimeStampResp resp;
			try
			{
				if (genTime == null)
				{
					throw new TspValidationException("The time source is not available.", 512);
				}
				request.Validate(this.acceptedAlgorithms, this.acceptedPolicies, this.acceptedExtensions);
				this.status = PkiStatus.Granted;
				this.AddStatusString("Operation Okay");
				PkiStatusInfo pkiStatusInfo = this.GetPkiStatusInfo();
				ContentInfo instance;
				try
				{
					instance = ContentInfo.GetInstance(Asn1Object.FromByteArray(this.tokenGenerator.Generate(request, serialNumber, genTime.Value).ToCmsSignedData().GetEncoded()));
				}
				catch (IOException e)
				{
					throw new TspException("Timestamp token received cannot be converted to ContentInfo", e);
				}
				resp = new TimeStampResp(pkiStatusInfo, instance);
			}
			catch (TspValidationException ex)
			{
				this.status = PkiStatus.Rejection;
				this.SetFailInfoField(ex.FailureCode);
				this.AddStatusString(ex.Message);
				resp = new TimeStampResp(this.GetPkiStatusInfo(), null);
			}
			TimeStampResponse result;
			try
			{
				result = new TimeStampResponse(resp);
			}
			catch (IOException e2)
			{
				throw new TspException("created badly formatted response!", e2);
			}
			return result;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000BC0C4 File Offset: 0x000BA2C4
		public TimeStampResponse GenerateFailResponse(PkiStatus status, int failInfoField, string statusString)
		{
			this.status = status;
			this.SetFailInfoField(failInfoField);
			if (statusString != null)
			{
				this.AddStatusString(statusString);
			}
			TimeStampResp resp = new TimeStampResp(this.GetPkiStatusInfo(), null);
			TimeStampResponse result;
			try
			{
				result = new TimeStampResponse(resp);
			}
			catch (IOException e)
			{
				throw new TspException("created badly formatted response!", e);
			}
			return result;
		}

		// Token: 0x04001702 RID: 5890
		private PkiStatus status;

		// Token: 0x04001703 RID: 5891
		private Asn1EncodableVector statusStrings;

		// Token: 0x04001704 RID: 5892
		private int failInfo;

		// Token: 0x04001705 RID: 5893
		private TimeStampTokenGenerator tokenGenerator;

		// Token: 0x04001706 RID: 5894
		private IList acceptedAlgorithms;

		// Token: 0x04001707 RID: 5895
		private IList acceptedPolicies;

		// Token: 0x04001708 RID: 5896
		private IList acceptedExtensions;

		// Token: 0x020008D7 RID: 2263
		private class FailInfo : DerBitString
		{
			// Token: 0x06004D5C RID: 19804 RVA: 0x0017105D File Offset: 0x0016F25D
			internal FailInfo(int failInfoValue) : base(failInfoValue)
			{
			}
		}
	}
}
