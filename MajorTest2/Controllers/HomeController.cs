using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace MajorTest2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Screenshot()
        {
            Bitmap screen;
            string fileName = "ScreenshotNew.jpg";

            //получение размера экрана 

            // Rectangle screenDimensions = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            // Size s = new Size(screenDimensions.Width, screenDimensions.Height);
            Size s = new Size(3456, 2234);

            // пустое изображение
            screen = new Bitmap(s.Width, s.Height);
            Graphics memoryGraphics = Graphics.FromImage(screen);
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);

            //сохранение в файл
            screen.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            string path = Path.Combine("./", fileName);
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/jpg";
            return File(mas, file_type, fileName);
        }


        [HttpGet]
        public IActionResult Largefile(string name)
        {
            try
            {
                DirectoryInfo folderInfo = new DirectoryInfo(name);
                FileInfo[] files = folderInfo.GetFiles();
                long largestSize = 0;
                string largestFileName = "";
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Length > largestSize)
                    {
                        largestSize = files[i].Length;
                        largestFileName = files[i].Name;
                    }
                }

                return new JsonResult(new
                {
                    DirectoryName = name,
                    FileName = largestFileName,
                    FileSize = largestSize
                });
                
                // Response Example (200):
                // {
                //     "directoryName": "controllers",
                //     "fileName": "AccountController.cs",
                //     "fileSize": 3214
                // }
                
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
        }
    }
}