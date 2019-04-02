# StreamingAssetStream

This is a cross-platform FileStream class for reading contents in Unity's StreamingAssets folder.

**On non-Android platforms**  
- StreamingAssetStream reads files in StreamingAssets directly.

**On Android platform**
- Because StreamingAssets folder is compressed inside apk, StreamingAssetStream reads files via System.IO.Compression.ZipArchive.

## Usage
```
using (var stream = new StreamingAssetStream("Test.txt"))
{
    using (var reader = new StreamReader(stream))
    {
        Debug.Log(reader.ReadToEnd());
    }
}
```

--------------

これはUnityのStreamingAssetsフォルダの内容を読むためのクロスプラットフォームのFileStreamクラスです。

**アンドロイド以外**
- StreamingAssetStreamはStreamingAssets内のファイルを直接読み取ります。

**アンドロイド**
- StreamingAssetsフォルダはapk内で圧縮されているため、StreamingAssetStreamは System.IO.Compression.ZipArchiveを介してファイルを読み取ります。

