using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProccessing
{
    class Proccess
    {
        public Proccess()
        {

        }

        public static bool ConvertToGray(Bitmap b, string[] files)
        {
            List<Bitmap> list = new List<Bitmap>();

            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color c1 = b.GetPixel(i, j);
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;

                    int grey = (byte)(0.299 * r1 + 0.587 * g1 + 0.144 * b1);

                    Bitmap bitmap = GetDominantColor(c1, files);
                    list.Add(bitmap);
                    b.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
                }
            }

            CombineImages(list);

            return true;
        }

        public static void CombineImages(List<Bitmap> list)
        {
            //change the location to store the final image.
            string finalImage = @"C:\\Users\\NurulloAlijonov\\Desktop\\Result.jpg";
            List<int> imageHeights = new List<int>();
            int nIndex = 0;
            int width = 0;
            foreach (var img in list)
            {
                imageHeights.Add(img.Height);
                width += img.Width;
            }
            imageHeights.Sort();
            int height = imageHeights[imageHeights.Count - 1];
            Bitmap img3 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img3);
            g.Clear(SystemColors.AppWorkspace);
            for (int i = 0; i < list.Count; i++)
            {
                if (nIndex == 0)
                {
                    g.DrawImage(list[i], new Point(0, 0));
                    nIndex++;
                    width = list[i].Width;
                }
                else
                {
                    g.DrawImage(list[i], new Point(i * 5, i*5));
                    width += list[i].Width;
                }
            }
            g.Dispose();
            img3.Save(finalImage, System.Drawing.Imaging.ImageFormat.Jpeg);
            img3.Dispose();
        }

        public static Bitmap GetDominantColor(Color c, string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                int r = 0;
                int g = 0;
                int b = 0;

                int total = 0;

                Bitmap temp = new Bitmap(Image.FromFile(files[i]));
                for (int x = 0; x < temp.Width; x++)
                {
                    for (int y = 0; y < temp.Height; y++)
                    {
                        Color clr = temp.GetPixel(x, y);

                        r += clr.R;
                        g += clr.G;
                        b += clr.B;

                        total++;
                    }
                }

                //Calculate average
                r /= total;
                g /= total;
                b /= total;

                Color c2 = Color.FromArgb(r, g, b);

                if (ColorsAreClose(c2, c))
                {
                    return temp;
                }
            }

            return new Bitmap(Image.FromFile(@"C:\Users\NurulloAlijonov\Desktop\Color\grey.jpg"));
        }

        public static bool ColorsAreClose(Color color1, Color color2)
        {

            var rDist = Math.Abs(color1.R - color2.R);
            var gDist = Math.Abs(color1.G - color2.G);
            var bDist = Math.Abs(color1.B - color2.B);

            if (rDist + gDist + bDist > 100)
                return false;

            return true;

        }
    }


}
