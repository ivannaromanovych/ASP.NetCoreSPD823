using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Komunalka.Entities;
using Komunalka.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Komunalka.Controllers
{
    public class ConsumerController : Controller
    {
        private readonly ApplicationDbContext context;
        public ConsumerController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<ConsumerViewModel> model = context.Consumers.Select(c => new ConsumerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Photo= "/Image/"+c.Image,
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
       [HttpPost]
        public async Task<IActionResult> Create(ConsumerCreateViewModel model)
        {
            string filename = Path.GetRandomFileName() + ".jpg";
            Consumer consumer = new Consumer
            {
                Name = model.Name,
                Address = model.Address,
                Image=filename
            };
            context.Consumers.Add(consumer);
            context.SaveChanges();
            await AddFile(model.Image, filename);
            return RedirectToAction("Index");
        }

        public async Task AddFile(IFormFile uploadedFile, string filename)
        {
            if (uploadedFile != null)
            {
                string save= Path.Combine("Image", filename);
                //using (FileStream fileStream = new FileStream(save, FileMode.Create))
                //{
                //    await uploadedFile.CopyToAsync(fileStream);
                //}
                var bitmap=GetBitmapFromFile(uploadedFile);
                var saveBitmap = CompressImage(bitmap, 75, 75);
                saveBitmap.Save(save, ImageFormat.Jpeg);


            }
        }
        private Bitmap GetBitmapFromFile(IFormFile uploadedFile)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Position = 0;
                    uploadedFile.CopyToAsync(memoryStream);
                    using (var img = Image.FromStream(memoryStream))
                    {
                        memoryStream.Close();
                        return new Bitmap(img);
                    }
                }
            }
            catch { return null; }
        }


        private Bitmap CompressImage(Bitmap originalPic, int maxWidth, int maxHeight)
        {
            try
            {
                int width = originalPic.Width;
                int height = originalPic.Height;
                int widthDiff = width - maxWidth;
                int heightDiff = height - maxHeight;
                bool doWidthResize = (maxWidth > 0 && width > maxWidth && widthDiff > heightDiff);
                bool doHeightResize = (maxHeight > 0 && height > maxHeight && heightDiff > widthDiff);

                if (doWidthResize || doHeightResize || (width.Equals(height) && widthDiff.Equals(heightDiff)))
                {
                    int iStart;
                    Decimal divider;
                    if (doWidthResize)
                    {
                        iStart = width;
                        divider = Math.Abs((Decimal)iStart / maxWidth);
                        width = maxWidth;
                        height = (int)Math.Round((height / divider));
                    }
                    else
                    {
                        iStart = height;
                        divider = Math.Abs((Decimal)iStart / maxHeight);
                        height = maxHeight;
                        width = (int)Math.Round(width / divider);
                    }
                }
                using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics oGraphics = Graphics.FromImage(outBmp))
                    {
                        //oGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        //oGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        oGraphics.DrawImage(originalPic, 0, 0, width, height);
                        //Водяний знак
                        //Font font = new Font("Arial", 20);
                        //Brush brash = new SolidBrush(Color.Blue);
                        //oGraphics.DrawString("Hello Vova", font, brash, new Point(25, 25));
                        return new Bitmap(outBmp);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        public IActionResult Edit( int id)
        {
            ConsumerEditViewModel model= context.Consumers.Select(c => new ConsumerEditViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address

            }).SingleOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ConsumerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Consumer consumer = context.Consumers.SingleOrDefault(x => x.Id == model.Id);
                consumer.Name = model.Name;
                consumer.Address = model.Address;
                context.SaveChanges();

            }            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ConsumerViewModel model = context.Consumers.Select(c => new ConsumerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address

            }).SingleOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(ConsumerViewModel model)
        {
            if (ModelState.IsValid)
            {

                Consumer consumer = context.Consumers.SingleOrDefault(x => x.Id == model.Id);
                context.Consumers.Remove(consumer);
                context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}