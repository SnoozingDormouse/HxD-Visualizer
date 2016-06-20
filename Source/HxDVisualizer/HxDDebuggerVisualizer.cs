using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(typeof(HxDVisualizer.HxDDebuggerVisualizer), typeof(VisualizerObjectSource), Target = typeof(WeakReference), Description = "HxD Visualizer")]
[assembly: System.Diagnostics.DebuggerVisualizer(typeof(HxDVisualizer.HxDDebuggerVisualizer), typeof(VisualizerObjectSource), Target = typeof(WeakReference<byte[]>), Description = "HxD Visualizer")]
[assembly: System.Diagnostics.DebuggerVisualizer(typeof(HxDVisualizer.HxDDebuggerVisualizer), typeof(VisualizerObjectSource), Target = typeof(byte[]), Description = "HxD Visualizer")]

namespace HxDVisualizer
{
	public class HxDDebuggerVisualizer : DialogDebuggerVisualizer
	{ 
		protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			ShowInHxDOrDisplayError(ByteArrayExtractor.GetByteArrayFrom(objectProvider));
		}

		private static void ShowInHxDOrDisplayError(Byte[] byteArray)
		{
			if (byteArray != null)
				SaveByteArrayAsFileAndDisplayInHxD(byteArray);
			else
				ShowErrorIfNotByteArray();
		}

		private static void SaveByteArrayAsFileAndDisplayInHxD(byte[] byteArray)
		{
			String byteFile = Path.GetTempFileName().Replace(".tmp", ".bin"); 

			File.WriteAllBytes(byteFile, byteArray);
			ShowFileInHxD(byteFile);
			File.Delete(byteFile);
		}

		private static void ShowFileInHxD(String byteFile)
		{
			Process hxDProc = new Process();
			hxDProc.StartInfo.FileName = byteFile;
			hxDProc.EnableRaisingEvents = true;
			hxDProc.Start();
			hxDProc.WaitForExit();
		}
		private static void ShowErrorIfNotByteArray()
		{
			MessageBox.Show("Only byte[] or WeakReference of byte[] allowed");
		}
	}
}
