using System;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HxDVisualizer.Tests
{
	[TestClass]
	public class DHxDDebuggerVisualizerTest
	{
		[TestMethod]
		public void TestHxDIsCalledAndExitsGracefully()
		{
			byte[] b;
			var r = new Random();

			b = new byte[r.Next(1024 * 512)];

			for (int i = 0; i < b.Length; i++)
			{
				b[i] = (byte)r.Next(0, 256);
			}

			TestShowVisualizer(b);
		}

		[TestMethod]
		public void TestHxDIsNotCalledWhenObjectIsString()
		{
			try
			{
				var b = "String Object";
				TestShowVisualizer(b);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		public static void TestShowVisualizer(object objectToVisualize)
		{
			VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(HxDDebuggerVisualizer));
			visualizerHost.ShowVisualizer();
		}
	}
}
