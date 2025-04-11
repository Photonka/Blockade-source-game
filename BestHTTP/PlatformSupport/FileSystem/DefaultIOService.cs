using System;
using System.IO;
using BestHTTP.Logger;

namespace BestHTTP.PlatformSupport.FileSystem
{
	// Token: 0x020007C0 RID: 1984
	public sealed class DefaultIOService : IIOService
	{
		// Token: 0x060046F2 RID: 18162 RVA: 0x00197798 File Offset: 0x00195998
		public Stream CreateFileStream(string path, FileStreamModes mode)
		{
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("DefaultIOService", string.Format("CreateFileStream path: '{0}' mode: {1}", path, mode));
			}
			switch (mode)
			{
			case FileStreamModes.Create:
				return new FileStream(path, FileMode.Create);
			case FileStreamModes.Open:
				return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			case FileStreamModes.Append:
				return new FileStream(path, FileMode.Append);
			default:
				throw new NotImplementedException("DefaultIOService.CreateFileStream - mode not implemented: " + mode.ToString());
			}
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x0019781B File Offset: 0x00195A1B
		public void DirectoryCreate(string path)
		{
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("DefaultIOService", string.Format("DirectoryCreate path: '{0}'", path));
			}
			Directory.CreateDirectory(path);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x0019784C File Offset: 0x00195A4C
		public bool DirectoryExists(string path)
		{
			bool flag = Directory.Exists(path);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("DefaultIOService", string.Format("DirectoryExists path: '{0}' exists: {1}", path, flag));
			}
			return flag;
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x00197890 File Offset: 0x00195A90
		public string[] GetFiles(string path)
		{
			string[] files = Directory.GetFiles(path);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("DefaultIOService", string.Format("GetFiles path: '{0}' files count: {1}", path, files.Length));
			}
			return files;
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x001978D3 File Offset: 0x00195AD3
		public void FileDelete(string path)
		{
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("DefaultIOService", string.Format("FileDelete path: '{0}'", path));
			}
			File.Delete(path);
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x00197904 File Offset: 0x00195B04
		public bool FileExists(string path)
		{
			bool flag = File.Exists(path);
			if (HTTPManager.Logger.Level == Loglevels.All)
			{
				HTTPManager.Logger.Verbose("DefaultIOService", string.Format("FileExists path: '{0}' exists: {1}", path, flag));
			}
			return flag;
		}
	}
}
