using System;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace HxDVisualizer
{
	class ByteArrayExtractor
	{
		public static byte[] GetByteArrayFrom(IVisualizerObjectProvider objectProvider)
		{
			var obj = objectProvider.GetObject();

			return
				GetByteArrayFromWeakReferenceByteArray(obj) ??
				GetByteArrayFromWeakReference(obj) ??
				GetByteArrayFromByteArray(obj);
		}

		private static byte[] GetByteArrayFromWeakReferenceByteArray(Object obj)
		{
			byte[] byteArray = null;
			WeakReference<byte[]> byteArrayWeakReference = obj as WeakReference<byte[]>;
			if (byteArrayWeakReference != null)
			{
				byteArrayWeakReference.TryGetTarget(out byteArray);
			}
			return byteArray;
		}

		private static byte[] GetByteArrayFromWeakReference(Object obj)
		{
			byte[] byteArray = null;
			WeakReference byteArrayWeakReference = obj as WeakReference;
			if (byteArrayWeakReference != null)
			{ 
				byteArray = byteArrayWeakReference.Target as byte[];
			}
			return byteArray;
		}

		private static byte[] GetByteArrayFromByteArray(Object obj)
		{
			return obj as byte[];
		}
	}
}
