using System;
using System.IO;
using System.Text;
using UnityEngine;
using Ionic.Zlib;
using ICSharpCode.SharpZipLib.GZip;

public class Gzip : MonoBehaviour {
	/*
    public static string Compress(string text) {
        byte[] buffer = Encoding.UTF8.GetBytes(text);
        //byte[] compressed = Ionic.Zlib.GZipStream.CompressBuffer(buffer);
        //byte[] compressed =
        Stream stream = new MemoryStream(gzBuffer);
        ICSharpCode.SharpZipLib.GZip.GZipOutputStream input = new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(stream);
        byte[] compressed = new byte[input.Length];
        input.Read(decompressed, 0, decompressed.Length);
        return Convert.ToBase64String(compressed);
    }
    public static string Decompress(string compressedText) {
        byte[] gzBuffer = Convert.FromBase64String(compressedText);
        Stream stream = new MemoryStream(gzBuffer);
        ICSharpCode.SharpZipLib.GZip.GZipInputStream input = new ICSharpCode.SharpZipLib.GZip.GZipInputStream(stream);
        byte[] decompressed = new byte[input.Length];
        input.Read(decompressed, 0, decompressed.Length);
        return Encoding.UTF8.GetString(decompressed);
    }
    */

	public static string Compress(string text)
	{
		if (text == null)
			return null;

		using (Stream memOutput = new MemoryStream())
		{
			using (GZipOutputStream zipOut = new GZipOutputStream(memOutput))
			{
				using (StreamWriter writer = new StreamWriter(zipOut))
				{
					writer.Write(text);

					writer.Flush();
					zipOut.Finish();

					byte[] bytes = new byte[memOutput.Length];
					memOutput.Seek(0, SeekOrigin.Begin);
					memOutput.Read(bytes, 0, bytes.Length);

					return Convert.ToBase64String(bytes);
				}
			}
		}
	}

	public static string Decompress(string text)
	{
		byte[] gzBuffer = Convert.FromBase64String(text);
		if (gzBuffer == null)
			return null;

		using (Stream memInput = new MemoryStream(gzBuffer))
		using (GZipInputStream zipInput = new GZipInputStream(memInput))
		using (StreamReader reader = new StreamReader(zipInput))
		{
			string decompressed = reader.ReadToEnd();

			return decompressed;
		}
	}
}