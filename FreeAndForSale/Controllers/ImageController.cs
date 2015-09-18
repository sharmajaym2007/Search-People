using People_Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace People_Search.Controllers
{
    public class ImageController : ApiController
    {
        [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
        
        [Route("api/addimage/{name?}/{address?}")]
        public Task<IEnumerable<string>> Post(string name, string address)
        {

            try
            {

                if (Request.Content.IsMimeMultipartContent())
                {
                    string fullPath = HttpContext.Current.Server.MapPath("~/uploads");
                    MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(fullPath);
                    var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            var info = new FileInfo(i.LocalFileName);
                            UserDetail ud = new UserDetail();
                            byte[] a = File.ReadAllBytes(info.FullName);

                            //img.productID = productid;
                            ud.fname = name;
                            ud.address = address;
                            ud.pic = a;


                            ImageRepository.updateImage(ud);
                            return "File uploaded successfully!";
                        });
                        return fileInfo;
                    });
                    return task;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));
                }
            
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }


        }


        [Route("api/getimage/{id?}")]
        public HttpResponseMessage Get(int id)
        {

            PeopleEntities1 db = new PeopleEntities1();
            var data = from i in db.UserDetails
                       where i.userid == id
                       select i;
            UserDetail img = (UserDetail)data.SingleOrDefault();
            byte[] imgData = null;
            
                imgData = img.pic;
            
            //AddPeople.byteArrayToImage(imgData);
            HttpResponseMessage response = new HttpResponseMessage();

            //2
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bmp = (Bitmap)typeConverter.ConvertFrom(imgData);
            //3'
            var name = id;
            var Fs = new FileStream(HostingEnvironment.MapPath("~/uploads") + @"\I" + name.ToString() + ".png", FileMode.Create);
            bmp.Save(Fs, ImageFormat.Png);
            bmp.Dispose();
            //4
            Image img1 = Image.FromStream(Fs);
            Fs.Close();
            Fs.Dispose();
            //5
            MemoryStream ms = new MemoryStream();
            img1.Save(ms, ImageFormat.Png);
            //6
            response.Content = new ByteArrayContent(ms.ToArray());
            ms.Close();
            ms.Dispose();
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;
            db.Dispose();
            return response;

        }

    }


}

