//
// Copyright(c) 2019 Takanori Shibasaki
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
using UnityEngine;
using System.IO;
using System.IO.Compression;

public class StreamingAssetStream
#if UNITY_ANDROID && !UNITY_EDITOR
    : Stream
#else
    : FileStream
#endif
{
#if UNITY_ANDROID && !UNITY_EDITOR
    static FileStream mAPKStream;
    static ZipArchive mAPK;

    Stream mZipStream;

    static StreamingAssetStream()
    {
        mAPKStream = new FileStream(Application.dataPath, FileMode.Open, FileAccess.Read);
        mAPK = new ZipArchive(mAPKStream, ZipArchiveMode.Read);
    }

    public StreamingAssetStream(string entryName)
    {
        ZipArchiveEntry entry = mAPK.GetEntry("assets/" + entryName);

        if (entry == null)
        {
            throw new FileNotFoundException(entryName);
        }

        mZipStream = entry.Open();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (mZipStream != null)
        {
            mZipStream.Dispose();
            mZipStream = null;
        }
    }

    public override bool CanRead { get { return mZipStream.CanRead; } }

    public override bool CanSeek { get { return mZipStream.CanSeek; } }

    public override bool CanWrite { get { return mZipStream.CanWrite; } }

    public override long Length { get { return mZipStream.Length; } }

    public override long Position { get { return mZipStream.Position; } set { mZipStream.Position = value; } }

    public override void Flush()
    {
        mZipStream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return mZipStream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return mZipStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        mZipStream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        mZipStream.Write(buffer, offset, count);
    }
#else

    public StreamingAssetStream(string entryName): base(Application.streamingAssetsPath + "/" + entryName, FileMode.Open, FileAccess.Read)
    {
    }

#endif
}
