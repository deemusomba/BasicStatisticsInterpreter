using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public class BitmapAscii
{
    private const string AsciiChars = "@%#*+=-:. ";

    private readonly int kernelWidth;
    private readonly int kernelHeight;

    public BitmapAscii(int kernelWidth, int kernelHeight)
    {
        this.kernelWidth = kernelWidth;
        this.kernelHeight = kernelHeight;
    }

    public string Asciitize(Bitmap bitmap)
    {
        var asciiBuilder = new StringBuilder();

        for (int y = 0; y < bitmap.Height; y += kernelHeight)
        {
            for (int x = 0; x < bitmap.Width; x += kernelWidth)
            {
                var kernelColors = GetKernelColors(bitmap, x, y);
                var averageGray = AverageColor(kernelColors);
                var asciiChar = GrayToString(averageGray);
                asciiBuilder.Append(asciiChar);
            }

            asciiBuilder.Append(Environment.NewLine);
        }

        return asciiBuilder.ToString();
    }

    private List<Color> GetKernelColors(Bitmap bitmap, int startX, int startY)
    {
        var colors = new List<Color>();

        for (int y = startY; y < startY + kernelHeight; y++)
        {
            for (int x = startX; x < startX + kernelWidth; x++)
            {
                if (x < bitmap.Width && y < bitmap.Height)
                {
                    var pixelColor = bitmap.GetPixel(x, y);
                    colors.Add(pixelColor);
                }
            }
        }

        return colors;
    }

    private double AverageColor(List<Color> colors)
    {
        int totalR = 0;
        int totalG = 0;
        int totalB = 0;

        foreach (var color in colors)
        {
            totalR += color.R;
            totalG += color.G;
            totalB += color.B;
        }

        int count = colors.Count;
        double averageR = totalR / (double)count;
        double averageG = totalG / (double)count;
        double averageB = totalB / (double)count;

        return AveragePixel(averageR, averageG, averageB);
    }

    private double AveragePixel(double r, double g, double b)
    {
        return (r + g + b) / (3 * 255.0);
    }

    private string GrayToString(double gray)
    {
        int charIndex = (int)Math.Floor(gray * (AsciiChars.Length - 1));
        return AsciiChars[charIndex].ToString();
    }

    public override string ToString()
    {
    public override string ToString()
{
    StringBuilder asciiBuilder = new StringBuilder();

    for (int y = 0; y < asciiImage.Height; y++)
    {
        for (int x = 0; x < asciiImage.Width; x++)
        {
            var pixelColor = asciiImage.GetPixel(x, y);
            var grayValue = AveragePixel(pixelColor.R, pixelColor.G, pixelColor.B);
            var asciiChar = GrayToString(grayValue);
            asciiBuilder.Append(asciiChar);
        }

        asciiBuilder.Append(Environment.NewLine);
    }

    return asciiBuilder.ToString();
}
        throw new NotImplementedException();
    }
}

public class MainForm : Form
{
    private PictureBox inputPictureBox;
    private PictureBox outputPictureBox;
    private NumericUpDown kernelWidthNumericUpDown;
    private NumericUpDown kernelHeightNumericUpDown;
    private BitmapAscii bitmapAscii;

    public MainForm()
    {
        InitializeComponents();
        bitmapAscii = new BitmapAscii((int)kernelWidthNumericUpDown.Value, (int)kernelHeightNumericUpDown.Value);
    }

    private void InitializeComponents()
    {
        // Initialize and configure controls
        inputPictureBox = new PictureBox
        {
            Location = new Point(10, 10),
            Size = new Size(300, 300),
            BorderStyle = BorderStyle.FixedSingle,
            SizeMode = PictureBoxSizeMode.Zoom
        };

        outputPictureBox = new PictureBox
        {
            Location = new Point(320, 10),
            Size = new Size(300, 300),
            BorderStyle = BorderStyle.FixedSingle,
            SizeMode = PictureBoxSizeMode.Zoom
        };

        kernelWidthNumericUpDown = new NumericUpDown
        {
            Location = new Point(10, 320),
            Size = new Size(100, 20),
            Value = 10
        };

        kernelHeightNumericUpDown = new NumericUpDown
        {
            Location = new Point(120, 320),
            Size = new Size(100, 20),
            Value = 10
        };

        kernelWidthNumericUpDown.ValueChanged += (sender, e) => UpdateOutputImage();
        kernelHeightNumericUpDown.ValueChanged += (sender, e) => UpdateOutputImage();

        // Add controls to the form
        Controls.Add(inputPictureBox);
        Controls.Add(outputPictureBox);
        Controls.Add(kernelWidthNumericUpDown);
        Controls.Add(kernelHeightNumericUpDown);
    }

    private void UpdateOutputImage()
    {
        if (inputPictureBox.Image != null)
        {
            var inputImage = new Bitmap(inputPictureBox.Image);
            var asciiText = bitmapAscii.Asciitize(inputImage);
            var asciiImage = GenerateAsciiImage(asciiText);
            outputPictureBox.Image = asciiImage;
        }
    }

    private Bitmap GenerateAsciiImage(string asciiText)
    {
        const int fontSize = 12;
        const int fontAspectRatio = 2;

        var asciiImage = new Bitmap(outputPictureBox.Width, outputPictureBox.Height);
        var graphics = Graphics.FromImage(asciiImage);
        graphics.Clear(Color.White);
        using (var font = new Font("Consolas", fontSize, FontStyle.Regular))
        {
            for (int y = 0; y < asciiText.Length; y++)
            {
                for (int x = 0; x < asciiText.Length; x++)
                {
                    var character = asciiText[y * asciiText.Length + x];
                    var brush = new SolidBrush(Color.Black);
                    var point = new PointF(x * fontSize * fontAspectRatio, y * fontSize);
                    graphics.DrawString(character.ToString(), font, brush, point);
                }
            }
        }

        return asciiImage;
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new MainForm());
    }
}
