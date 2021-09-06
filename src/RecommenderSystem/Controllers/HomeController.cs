using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using HtmlAgilityPack;
using InventoryManagement.Models.Common;
using InventoryManagement.Models.DBModels;
using InventoryManagement.Models.Repositories;
using InventoryManagement.ViewModels;
using Microsoft.Win32;
using PuppeteerSharp;
using Product = InventoryManagement.Models.DBModels.Product;

namespace InventoryManagement.Controllers
{
    class FileDownloader
    {
        private readonly string _url;
        private readonly string _fullPathWhereToSave;
        private bool _result = false;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);

        public FileDownloader(string url, string fullPathWhereToSave)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (string.IsNullOrEmpty(fullPathWhereToSave)) throw new ArgumentNullException("fullPathWhereToSave");

            this._url = url;
            this._fullPathWhereToSave = fullPathWhereToSave;
        }

        public bool StartDownload(int timeout)
        {
            try
            {
                var directoryName = Path.GetDirectoryName(_fullPathWhereToSave);
                System.IO.Directory.CreateDirectory(directoryName);

                if (File.Exists(_fullPathWhereToSave))
                {
                    File.Delete(_fullPathWhereToSave);
                }
                using (WebClient client = new WebClient())
                {
                    var ur = new Uri(_url);
                    // client.Credentials = new NetworkCredential("username", "password");
                    client.DownloadProgressChanged += WebClientDownloadProgressChanged;
                    client.DownloadFileCompleted += WebClientDownloadCompleted;
                    Console.WriteLine(@"Downloading file:");
                    client.DownloadFileAsync(ur, _fullPathWhereToSave);
                    _semaphore.Wait(timeout);
                    return _result && File.Exists(_fullPathWhereToSave);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Was not able to download file!");
                Console.Write(e);
                return false;
            }
            finally
            {
                this._semaphore.Dispose();
            }
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\r     -->    {0}%.", e.ProgressPercentage);
        }

        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            _result = !args.Cancelled;
            if (!_result)
            {
                Console.Write(args.Error.ToString());
            }
            Console.WriteLine(Environment.NewLine + "Download finished!");
            _semaphore.Release();
        }

        public static bool DownloadFile(string url, string fullPathWhereToSave, int timeoutInMilliSec)
        {
            return new FileDownloader(url, fullPathWhereToSave).StartDownload(timeoutInMilliSec);
        }
    }


    public class ProductFilter
    {
        public string Name { get; set; }
        public int CategoryId { get; set; } = 0;
        public int PriceFrom { get; set; } = 0;
        public int PriceTo { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public bool? Featured { get; set; }
        public string Vendor { get; set; }
        public string Memory { get; set; }
        public string ScreenSize { get; set; }
        public int UserId { get; set; } = 0;
    }

    public class ProductDetailPage
    {
        public Product_LV Product { get; set; }
        public List<Product_LV> FeaturedProducts { get; set; }
        public List<Product_LV> VendorProducts { get; set; }
        public List<Product_LV> WishListProducts { get; set; }
        public List<Product_LV> RelatedProducts { get; set; }
        public List<Product_LV> DiscountedProducts { get; set; }

        public ProductReviewForm ProductReviewForm { get; set; }
        public List<ProductReviewForm> Reviews { get; internal set; }
    }

    public class ScreenSize
    {
        public string value { get; set; }
    }
    public class MemorySize
    {
        public string value { get; set; }
    }

    public class Vendor
    {
        public string value { get; set; }
    }

    public class ProductSearchVM
    {
        public List<MemorySize> MemorySize { get; set; }
        public List<ScreenSize> ScreenSize { get; set; }
        public List<Product_LV> ProductList { get; set; }
        public List<Vendor> Vendors { get; set; }
    }
    [Table("ProductReview")]
    public class ProductReviewForm
    {
        public string CustomerName { get; set; }
        public int Rating { get; set; }
        public string Email { get; set; }
        public string Review { get; set; }
        public int ProductId { get; set; }
        [DontInsert][DontUpdate]
        public DateTime TimeStamp { get; set; }
    }

    public class HomeController : BaseController
    {
        public ProductRepository ProductRepo { get; set; }

        public HomeController()
        {
            this.ProductRepo = new ProductRepository();
        }

        public async Task<ActionResult> Index()
        {
            //await ImportProducts();
            return View();
        }


        [HttpGet]
        public ActionResult ProductSearch(ProductFilter filter)
        {
           var lst =  ProductRepo.GetList(filter);
           var screensizes = DBHelper.GetList<ScreenSize>("SELECT DISTINCT ScreenSize as value FROM Products.Product where Screensize IS NOT NULL");
           var memorySize = DBHelper.GetList<MemorySize>("SELECT DISTINCT Memory as value FROM Products.Product where Memory IS NOT NULL");
           var vendors =  DBHelper.GetList<Vendor>("SELECT DISTINCT Vendor as value FROM Products.Product where Vendor IS NOT NULL");

            var vm = new ProductSearchVM
           {
               MemorySize = memorySize,
               ScreenSize = screensizes,
               Vendors = vendors,
               ProductList = lst
           };
            ViewBag.Filter = filter;
            return View(vm);
        }

        [HttpGet]
        public ActionResult ProductDetail(int id)
        {
            var product = ProductRepo.GetList(new ProductFilter
            {
                 ProductId = id
            }).FirstOrDefault();

            var vendorProducts = ProductRepo.GetList(new ProductFilter
            {
                 Vendor =  product.Vendor
            });

            var featuredProducts = ProductRepo.GetList(new ProductFilter
            {
                Featured = true
            });

            var relatedProducts = ProductRepo.GetList(new ProductFilter
            {
                CategoryId = product.CategoryID
            });


            var reviews = DBHelper.GetList<ProductReviewForm>("", $"where ProductId = {product.ID}");

            var discountedProducts = ProductRepo.GetProductsWithLowerPrices(product.CategoryID);

            var wishListProducts = new List<Product_LV>();
            if (Session["User"] != null)
            {
               var user = Session["User"] as SessionUser;
                wishListProducts = ProductRepo.GetList(new ProductFilter
               {
                   UserId = user.Id
               });
            }

            var vm = new ProductDetailPage
            {
                  Product =  product,
                  VendorProducts = vendorProducts,
                  FeaturedProducts = featuredProducts,
                  RelatedProducts = relatedProducts,
                  WishListProducts = wishListProducts,
                  DiscountedProducts = discountedProducts,
                  Reviews = reviews
            };
            return View(vm);
        }


        [HttpPost]
        public ActionResult ProductDetail(ProductReviewForm form)
        {
            DBHelper.Insert(form);
            return RedirectToAction("ProductDetail", "Home", new {Id = form.ProductId});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task ImportProducts()
        {

            var url = "https://czone.com.pk/laptops-pakistan-ppt.74.aspx?page=4";

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                Timeout = 120000
            });

            var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions { IsLandscape = true, Width = 1366, Height = 768 });
            try
            {
                await page.GoToAsync(url);

                string content = await page.GetContentAsync();

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(content);
                
                var lst = htmlDoc.QuerySelectorAll(".product");

                foreach (var item in lst)
                {
                    var productName = item.QuerySelector("h4").InnerText;
                    var price = item.QuerySelector("[id*=\"spnPrice\"]").InnerText;
                    var val = item.QuerySelector("img").Attributes["src"].Value;
                    var productDescription = item.QuerySelector(".description").InnerHtml;
             

                    
                    string fn = Path.GetFileName(Guid.NewGuid().ToString());
                    string fileExtension = fn.Remove(0, fn.IndexOf('.') + 1);
                    fn += ".jpg";
                    string SaveLocation = "~/Images/Products/";
                    var imagePath = Path.Combine(SaveLocation, fn);
                    string FilePath = Server.MapPath(SaveLocation);
                    var path = Path.Combine(FilePath, fn);

                    //var success = FileDownloader.DownloadFile(val, path, 30000);
                    val = "http://czone.com.pk/" + val;
                    var uri = new Uri(val);

                    //HttpClient _HttpClient = new HttpClient();
                    //using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
                    //{
                    //    request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                    //    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                    //    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                    //    request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
                    //    using (var response = await _HttpClient.SendAsync(request).ConfigureAwait(false))
                    //    {
                    //        response.EnsureSuccessStatusCode();
                    //        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    //        using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    //        using (var streamReader = new StreamReader(decompressedStream))
                    //        {
                    //            //await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    //            using (var fs = new FileStream(path, FileMode.CreateNew))
                    //            {
                    //                await response.Content.CopyToAsync(fs);
                    //            }
                    //        }
                    //    }
                    //}

                   
                    //using (var client = new WebClient())
                    //{
                    //    //path = path.Replace("\"", "");
                    //    client.DownloadFile(val,  imagePath);
                    //}
                    int priceInInt = 0;
                    int.TryParse(price.ToLower().Replace("rs", "").Replace(",", ""), out  priceInInt);
                    if (priceInInt == 0)
                    {
                        priceInInt = 1000;
                    }

                    var prod = new AddProduct_VM
                    {
                        product = new Models.DBModels.Product
                        {
                            ImagePath = val,
                            Name = productName,
                            CategoryID = 13,
                            Description = productName,
                            UnitID = "1"
                        },
                        CategoryID = 26,
                        ProductPacking = new List<ProductPackaging>()
                            {
                                new ProductPackaging
                                {
                                    PackageSize = 1,
                                    Quantity = 100,
                                    PackagingID = 4,
                                    PricePerPiece = priceInInt,
                                    RetailPrice = priceInInt
                                }
                            },
                        packaging = new List<Packaging_LV>(),
                        Units = new List<Unit>(),
                        categoryList = new List<Category_LV>()
                    };
                    AddProduct(prod);
                }


                browser.Dispose();

            }
            catch (Exception ex)
            {
                try
                {
                    string html = await page.GetContentAsync();

                }
                catch (Exception)
                {

                }

            }



        }

        public void AddProduct(AddProduct_VM vm)
        {
            SessionUser user = Session["User"] as SessionUser;
            if (vm.ProductPacking != null)
            {
                vm.ProductPacking = vm.ProductPacking.Where(x => x.PackagingID != 0).ToList();
            }
            if (vm.ImageFile != null)
            {
                string fn = Path.GetFileName(vm.ImageFile.FileName);
                string fileExtension = fn.Remove(0, fn.IndexOf('.') + 1);
                fn = vm.product.Name + "." + fileExtension;
                string SaveLocation = "~/Images/Products/";
                vm.product.ImagePath = Path.Combine(SaveLocation, fn);
                string FilePath = Server.MapPath(SaveLocation);
                vm.ImageFile.SaveAs(Path.Combine(FilePath, fn));
            }
            ProductRepo.Insert(vm, 8);
        }

        public JsonResult AddToWishList(int productId)
        {
            var user = Session["User"] as SessionUser;
            try
            {
                ProductRepo.AddToWishList(productId, user.Id);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
    }


    public class ProductL
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
    }
}