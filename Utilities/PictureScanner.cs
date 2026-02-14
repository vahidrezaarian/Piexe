using System.Drawing;

namespace Piexe.Utilities;

public static class PictureScanner
{
    public static List<AnalyzedItem> Scan(string imagePath)
    {
        var result = new List<AnalyzedItem>();

        var barcodes = BarcodeScanner.Scan(imagePath);
        
        if (barcodes != null && barcodes.Count > 0)
        {
            foreach (var barcode in barcodes)
            {
                if (barcode.IsQrCode)
                {
                    result.Add(new AnalyzedItem(barcode.Text, AnalyzedItemType.QrCode));
                }
                else
                {
                    result.Add(new AnalyzedItem(barcode.Text, AnalyzedItemType.Barcode));
                }
            }
        }

        var texts = TextScanner.Scan(imagePath);
        if (texts != null && texts.Length > 0)
        {
            foreach (var text in texts)
            {
                result.Add(new AnalyzedItem(text, AnalyzedItemType.Text));
            }
        }

        return result;
    }

    public static List<AnalyzedItem> Scan(Bitmap imageBitmap)
    {
        var result = new List<AnalyzedItem>();

        var barcodes = BarcodeScanner.Scan(imageBitmap);

        if (barcodes != null && barcodes.Count > 0)
        {
            foreach (var barcode in barcodes)
            {
                if (barcode.IsQrCode)
                {
                    result.Add(new AnalyzedItem(barcode.Text, AnalyzedItemType.QrCode));
                }
                else
                {
                    result.Add(new AnalyzedItem(barcode.Text, AnalyzedItemType.Barcode));
                }
            }
        }

        var texts = TextScanner.Scan(imageBitmap);
        if (texts != null && texts.Length > 0)
        {
            foreach (var text in texts)
            {
                result.Add(new AnalyzedItem(text, AnalyzedItemType.Text));
            }
        }

        return result;
    }
}
