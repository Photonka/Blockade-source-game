using System;
using System.IO;

namespace BestHTTP.PlatformSupport.FileSystem
{
	// Token: 0x020007C2 RID: 1986
	public interface IIOService
	{
		// Token: 0x060046F9 RID: 18169
		void DirectoryCreate(string path);

		// Token: 0x060046FA RID: 18170
		bool DirectoryExists(string path);

		// Token: 0x060046FB RID: 18171
		string[] GetFiles(string path);

		// Token: 0x060046FC RID: 18172
		void FileDelete(string path);

		// Token: 0x060046FD RID: 18173
		bool FileExists(string path);

		// Token: 0x060046FE RID: 18174
		Stream CreateFileStream(string path, FileStreamModes mode);
	}
}
