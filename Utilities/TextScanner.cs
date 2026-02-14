using System.Drawing;
using System.IO;
using Tesseract;

namespace Piexe.Utilities;

public static class TextScanner
{
    public static string[]? Scan(string imagePath)
    {
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image not found at: {imagePath}");

        var textBlocks = new List<string>();

        using var engine = new TesseractEngine(Path.Combine(AppContext.BaseDirectory, "tessdata"), "eng", EngineMode.Default);
        using var img = Pix.LoadFromFile(imagePath);
        using var page = engine.Process(img);
        using var iter = page.GetIterator();
        iter.Begin();

        do
        {
            string blockText = iter.GetText(PageIteratorLevel.Block);

            if (!string.IsNullOrWhiteSpace(blockText))
            {
                textBlocks.Add(blockText.Trim());
            }
        }
        while (iter.Next(PageIteratorLevel.Block));

        if (textBlocks.Count != 0)
        {
            return [.. textBlocks];
        }

        return null;
    }

    public static string[]? Scan(Bitmap imageBitmap)
    {
        var fileName = Path.Combine(Path.GetTempPath(), "tempImage.png");
        imageBitmap.Save(fileName);
        var result = Scan(fileName);
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        return result;
    }
}
