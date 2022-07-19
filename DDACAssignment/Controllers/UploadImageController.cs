using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

namespace DDACAssignment.Controllers
{
    public class UploadImageController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;

        public UploadImageController(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }

        const string bucketname = "musicports3bucket";

        public List<string> getAWSCredential()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configure = builder.Build();

            List<string> accesskeylist = new List<string>();
            accesskeylist.Add(configure["AWSCredential:accesskey"]);
            accesskeylist.Add(configure["AWSCredential:secretkey"]);
            accesskeylist.Add(configure["AWSCredential:sessiontoken"]);

            return accesskeylist;
        }

        public IActionResult Index(string msg = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Identity/Account/Login");
            }

            ViewBag.msg = msg;
            return View();
        }

        [HttpPost("FileUpload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = ""; string fileContents = null;
            string message = "";
            int i = 1;

            foreach (var singleimage in files)
            {
                if (singleimage.ContentType.ToLower() != "text/plain") //not text file..
                {
                    return BadRequest("The " + singleimage.FileName +
                        " unable to upload because uploaded file must be a text file");
                }
                else if (singleimage.Length == 0)
                {
                    return BadRequest("The " + singleimage.FileName + " image file is empty!");
                }
                else if (singleimage.Length > 1048576)
                {
                    return BadRequest("The " + singleimage.FileName + " image file is exceed 1 MB !");
                }
                else //after sem break, come back we learnt how to continue to send the file to server / s3
                {
                    //example 1: learn how to copy the file to the server
                    filePath = Path.Combine(_hostEnvironment.WebRootPath, "image",
                        singleimage.FileName);

                    using (var uploadStream = new FileStream(filePath, FileMode.Create))
                    {
                        await singleimage.CopyToAsync(uploadStream);
                    }

                    //example 2: learn how to read a textfile content so that you can use for future storing
                    using (var reader = new StreamReader(singleimage.OpenReadStream(),
                        new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true),
                        detectEncodingFromByteOrderMarks: true))
                    {
                        fileContents = fileContents + "\\n" + await reader.ReadToEndAsync();
                    }
                }
            }
            message = "Upload Successfully to Server!";

            return RedirectToAction("Index", "UploadFile", new { msg = message + "\\n" + fileContents });
        }


        [HttpPost("UploadToS3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendImage(List<IFormFile> images)
        {
            string message = "";
            List<string> accesskeylist = getAWSCredential();

            foreach (var image in images)
            {
                using (var S3client = new AmazonS3Client(accesskeylist[0], accesskeylist[1],
                    accesskeylist[2], Amazon.RegionEndpoint.USEast1))
                {
                    using (var uploadStream = new MemoryStream())
                    {
                        image.CopyTo(uploadStream);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = uploadStream,
                            Key = image.FileName,
                            BucketName = bucketname + "/Images",
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var transferringtoS3 = new TransferUtility(S3client);
                        await transferringtoS3.UploadAsync(uploadRequest);
                    }
                }
            }
            message = "All images uploaded to the S3";
            return RedirectToAction("Index", "UploadImage", new { msg = message });
        }

        public async Task<IActionResult> ViewImages(string msg = "")
        {
            ViewBag.msg = msg;
            List<string> accesskeylist = getAWSCredential();
            var result = new List<S3Object>();
            List<string> presignedURLS = new List<string>();

            try
            {
                AmazonS3Client s3Client = new AmazonS3Client(accesskeylist[0], accesskeylist[1],
                    accesskeylist[2], Amazon.RegionEndpoint.USEast1);

                //grab the objects and its information
                string token = null;
                do
                {
                    ListObjectsRequest request = new ListObjectsRequest()
                    {
                        BucketName = bucketname,
                        Prefix = "Images/"
                    };
                    ListObjectsResponse response = await s3Client.ListObjectsAsync(request).ConfigureAwait(false);
                    result.AddRange(response.S3Objects);
                    token = response.NextMarker;
                } while (token != null);

                //create each presign URL to the all objects
                foreach (var image in result)
                {
                    //create presigned url for temp access from public
                    GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketname,
                        Key = image.Key,
                        Expires = DateTime.Now.AddDays(1)
                    };

                    //get the generated URL path
                    presignedURLS.Add(s3Client.GetPreSignedURL(request));
                }

                ViewBag.URLs = presignedURLS;
            }
            catch (Exception ex)
            {

            }
            return View(result);
        }

        public async Task<IActionResult> DeleteImage(string FileName)
        {
            string message = "";
            List<string> credentialInfo = getAWSCredential();
            var S3client = new AmazonS3Client(credentialInfo[0], credentialInfo[1], credentialInfo[2],
                Amazon.RegionEndpoint.USEast1);

            //start deleting the related items
            try
            {
                if (string.IsNullOrEmpty(FileName))
                    return BadRequest("The " + FileName + " parameter is empty!");

                var deleteobjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketname,
                    Key = FileName
                };
                await S3client.DeleteObjectAsync(deleteobjectRequest);
                message = FileName + " is deleted from the S3 bucket! Please check the S3 bucket whether is it deleted!";
            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
            }
            return RedirectToAction("ViewImages", "UploadImage", new { msg = message });
        }

        //not used, as presigned URL is applied 
        public async Task<IActionResult> DownloadImage(string FileName)
        {
            string foldername = Path.GetDirectoryName(FileName);
            FileName = Path.GetFileName(FileName);
            string message = "";
            List<string> credentialInfo = getAWSCredential();
            var S3client = new AmazonS3Client(credentialInfo[0], credentialInfo[1], credentialInfo[2], Amazon.RegionEndpoint.USEast1);
            string downloadlocationInPC = "";
            try
            {
                if (string.IsNullOrEmpty(FileName))
                    return BadRequest("The " + FileName + " parameter is empty! Please give the correct filename before proceed");

                foldername = (!string.IsNullOrEmpty(foldername)) ? bucketname + "/" + foldername : bucketname;

                downloadlocationInPC = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\" + FileName;
                TransferUtility transferUtility = new TransferUtility(S3client);
                await transferUtility.DownloadAsync(downloadlocationInPC, foldername, FileName);
                message = FileName + " is downloaded to your PC location of " +
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\\\downloads";
            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
            }
            return RedirectToAction("ViewImages", "UploadImage", new { msg = message });
        }
    }
}
