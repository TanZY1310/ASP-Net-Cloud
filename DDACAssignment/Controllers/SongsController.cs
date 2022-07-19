using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDACAssignment.Data;
using DDACAssignment.Models;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace DDACAssignment.Controllers
{
    public class SongsController : Controller
    {
        private readonly DDACAssignment_NewContext _context;

        public SongsController(DDACAssignment_NewContext context)
        {
            _context = context;
        }

        const string bucketname = "musicports3bucket";
        public List<string> getAWSCredential()  //Link the code with the JSON file to get aws credentials
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configure = builder.Build();
            List<string> accesskeylist = new List<string>();
            accesskeylist.Add(configure["AWSCredential:accesskey"]);  //Subkey using colon to link
            accesskeylist.Add(configure["AWSCredential:secretkey"]);
            accesskeylist.Add(configure["AWSCredential:sessiontoken"]);

            return accesskeylist;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string searchString, string msg="")
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Identity/Account/Login");
            }

            var song = from m in _context.Songs  //Select all the data one time, then directly filter here. No need to access database multiple times
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                song = song.Where(s => s.SongName.Contains(searchString));  //To extract the flower in which the name matches
            }

            ViewBag.msg = msg;

            return View(await song.ToListAsync());
        }

        // GET: Songs/Details/5
        //using presigned URL to download songs
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (songs == null)
            {
                return NotFound();
            }

            List<string> accesskeylist = getAWSCredential();
            string presignedURLS;

            try
            {
                AmazonS3Client s3Client = new AmazonS3Client(accesskeylist[0], accesskeylist[1],
                    accesskeylist[2], Amazon.RegionEndpoint.USEast1);

                //create presigned url for temp access from public
                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                {
                    BucketName = bucketname,
                    Key = "Songs/" + songs.FileName,
                    Expires = DateTime.Now.AddDays(1)
                };

                //get the generated URL path
                presignedURLS = s3Client.GetPreSignedURL(request);

                ViewBag.URLs = presignedURLS;
            }
            catch (Exception e) { }

            return View(songs);
        }

        //not used, as presigned URL is applied 
        public async Task<IActionResult> DownloadSongs(int id)
        {
            var songs = await _context.Songs
                .FirstOrDefaultAsync(m => m.SongID == id);

            //Add the file name to be exact with the one in S3
            songs.FileName = "Songs/" + songs.FileName;

            ////Download
            string foldername = Path.GetDirectoryName(songs.FileName);  //get the directory name
            songs.FileName = Path.GetFileName(songs.FileName);  //Get File Name
            string message = "";
            List<String> credentialInfo = getAWSCredential();
            var s3client = new AmazonS3Client(credentialInfo[0], credentialInfo[1], credentialInfo[2], Amazon.RegionEndpoint.USEast1);
            string downloadlocationinPC = "";
            try
            {
                if (string.IsNullOrEmpty(songs.FileName))
                    return BadRequest("The " + songs.FileName + " parameter is empty! Please give the correct filename before proceeding");

                foldername = (!string.IsNullOrEmpty(foldername)) ? bucketname + "/" + foldername : bucketname;

                //Get users path and link to download on their pc
                downloadlocationinPC = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\" + songs.FileName;
                TransferUtility transferUtility = new TransferUtility(s3client);
                await transferUtility.DownloadAsync(downloadlocationinPC, foldername, songs.FileName);
                message = songs.FileName + " is downloaded to your PC location of " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\\\downloads'";

            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
            }
            return RedirectToAction(nameof(Index), new { msg = message });
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongID,SongName,SongGenre,ProducerName")] Songs songs, List<IFormFile> songFiles)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                songs.SongUploadDate = DateTime.Now;
                string songFilename = "";
                List<string> accesskeylist = getAWSCredential();
                foreach (var songFile in songFiles)
                {
                    songFilename = songFile.FileName;
                    //Get AWS Credentials
                    using (var S3client = new AmazonS3Client(accesskeylist[0], accesskeylist[1], accesskeylist[2],
                        Amazon.RegionEndpoint.USEast1))   //client to communicate with S3
                    {
                        //Add into S3
                        using (var uploadStream = new MemoryStream())
                        {
                            songFile.CopyTo(uploadStream);

                            var uploadRequest = new TransferUtilityUploadRequest()
                            {
                                InputStream = uploadStream,
                                Key = songFile.FileName,
                                BucketName = bucketname + "/Songs",  //Prefix for folder
                                CannedACL = S3CannedACL.PublicRead  //Give permission to public

                            };
                            var transferringtoS3 = new TransferUtility(S3client);
                            await transferringtoS3.UploadAsync(uploadRequest);
                        }
                    }
                }
                //return RedirectToAction("Index", "UploadSong", new { msg = message });
                message = "Songs created!";
                songs.FileName = songFilename;
                _context.Add(songs);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { msg = message });
            }
            return View(songs);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }


        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongID,SongName,SongGenre,ProducerName, FileName")] Songs songs, List<IFormFile> songFiles)
        {
            string message = "";
            List<string> accesskeylist = getAWSCredential();
            if (id != songs.SongID)
            {
                return NotFound();
            }

            //Delete Existing Music in the S3
            if (songFiles.Count != 0)
            {
                songs.FileName = "Songs/" + songs.FileName;
                var s3client = new AmazonS3Client(accesskeylist[0], accesskeylist[1], accesskeylist[2], Amazon.RegionEndpoint.USEast1);
                //Start deleting 
                try
                {
                    if (string.IsNullOrEmpty(songs.FileName))
                        return BadRequest("The" + songs.FileName + " parameter is empty");

                    var deleteobjectRequest = new DeleteObjectRequest
                    {
                        BucketName = bucketname,
                        Key = songs.FileName
                    };

                    await s3client.DeleteObjectAsync(deleteobjectRequest);

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    //message = "Error: " + ex.Message;
                }
                message = "Songs Successfully Deleted !";
            }

            if (ModelState.IsValid)
            { 
                try
                {
                    songs.SongUploadDate = DateTime.Now;

                    //Add new songs into S3 if there is new file inserted
                    foreach (var songFile in songFiles)
                    {

                        songs.FileName= songFile.FileName;

                        using (var S3client = new AmazonS3Client(accesskeylist[0], accesskeylist[1], accesskeylist[2],
                            Amazon.RegionEndpoint.USEast1))   //client to communicate with S3
                        {
                            using (var uploadStream = new MemoryStream())
                            {
                                songFile.CopyTo(uploadStream);

                                var uploadRequest = new TransferUtilityUploadRequest()
                                {
                                    InputStream = uploadStream,
                                    Key = songFile.FileName,
                                    BucketName = bucketname + "/Songs",  //Prefix for folder
                                    CannedACL = S3CannedACL.PublicRead  //Give permission to public

                                };
                                var transferringtoS3 = new TransferUtility(S3client);
                                await transferringtoS3.UploadAsync(uploadRequest);
                            }
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongsExists(songs.SongID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //songs.FileName = filename;
                message = "Songs successfully edited!";
                _context.Update(songs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { msg = message });
            }
            return View(songs);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string message = "";
            var songs = await _context.Songs.FindAsync(id);
            songs.FileName = "Songs/" + songs.FileName;

            List<string> credentialInfo = getAWSCredential();
            var s3client = new AmazonS3Client(credentialInfo[0], credentialInfo[1], credentialInfo[2], Amazon.RegionEndpoint.USEast1);
            //Start deleting 
            try
            {
                if (string.IsNullOrEmpty(songs.FileName))
                    return BadRequest("The" + songs.FileName + " parameter is empty");

                var deleteobjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketname,
                    Key = songs.FileName
                };

                await s3client.DeleteObjectAsync(deleteobjectRequest);

            }
            catch (Exception ex)
            {
                message= ex.Message;
                //message = "Error: " + ex.Message;
            }
            message = "Songs Successfully Deleted !";
            _context.Songs.Remove(songs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { msg = message });
        }

        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.SongID == id);
        }


    }
}
