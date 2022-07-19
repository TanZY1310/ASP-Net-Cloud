using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDACAssignment.Data;
using DDACAssignment.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace DDACAssignment.Controllers
{
    public class RecordingSessionsController : Controller
    {
        private readonly DDACAssignment_NewContext _context;

        public RecordingSessionsController(DDACAssignment_NewContext context)
        {
            _context = context;
        }

        private const string QueueName = "recordingSessionQueue";

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

        // GET: RecordingSessions
        public async Task<IActionResult> Index(string searchString, string ComposerName, string ProducerName, string msg = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Identity/Account/Login");
            }

            var recordSession = from m in _context.RecordingSession
                                select m;

            //Generate the listing for Composer
            IQueryable<string> queryComposer = from m in _context.RecordingSession
                                                   orderby m.ComposerName
                                                   select m.ComposerName;
            IEnumerable<SelectListItem> itemsComposer = new SelectList(await queryComposer.Distinct().ToListAsync());
            ViewBag.ComposerName = itemsComposer;

            //Generate the listing for Producer
            IQueryable<string> queryProducer = from m in _context.RecordingSession
                                                   orderby m.ProducerName
                                                   select m.ProducerName;
            IEnumerable<SelectListItem> itemsProducer = new SelectList(await queryProducer.Distinct().ToListAsync());
            ViewBag.ProducerName = itemsProducer;

            if (!String.IsNullOrEmpty(searchString))
            {
                recordSession = recordSession.Where(s => s.SongName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(ComposerName))
            {
                recordSession = recordSession.Where(s => s.ComposerName.Equals(ComposerName));
            }

            if (!String.IsNullOrEmpty(ProducerName))
            {
                recordSession = recordSession.Where(s => s.ProducerName.Contains(ProducerName));
            }

            ViewBag.msg = msg;

            return View(await recordSession.ToListAsync());

            //original
            //return View(await _context.RecordingSession.ToListAsync());
        }

        // GET: RecordingSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordingSession = await _context.RecordingSession
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recordingSession == null)
            {
                return NotFound();
            }

            return View(recordingSession);
        }

        // GET: RecordingSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecordingSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SessionID,SongName,StartDateTime,EndDateTime,ProducerName,ComposerName")] RecordingSession recordingSession)
        {
            List<string> credentialInfo = getAWSCredential();
            var sqsclient = new AmazonSQSClient(credentialInfo[0], credentialInfo[1], credentialInfo[2], Amazon.RegionEndpoint.USEast1);
            var queueURL = await sqsclient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });

            if (ModelState.IsValid)
            {
                _context.Add(recordingSession);
                await _context.SaveChangesAsync();

                //push the message content to queue
                try
                {
                    //create send message request
                    SendMessageRequest message = new SendMessageRequest();
                    message.MessageBody = JsonConvert.SerializeObject(recordingSession);
                    message.QueueUrl = queueURL.QueueUrl;

                    //send message now
                    await sqsclient.SendMessageAsync(message);
                    return RedirectToAction("Index", "RecordingSessions", new { msg = "Recording session scheduled!" });
                }
                catch (AmazonSQSException ex)
                {
                    return RedirectToAction("Index", "RecordingSessions", new { msg = "Error: " + ex.Message });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "RecordingSessions", new { msg = "Error: " + ex.Message });
                }
            }
            return View(recordingSession);
        }

        // GET: RecordingSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordingSession = await _context.RecordingSession.FindAsync(id);
            if (recordingSession == null)
            {
                return NotFound();
            }
            return View(recordingSession);
        }

        // POST: RecordingSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SessionID,SongName,StartDateTime,EndDateTime,ProducerName,ComposerName")] RecordingSession recordingSession)
        {
            if (id != recordingSession.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recordingSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordingSessionExists(recordingSession.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recordingSession);
        }

        // GET: RecordingSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordingSession = await _context.RecordingSession
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recordingSession == null)
            {
                return NotFound();
            }

            return View(recordingSession);
        }

        // POST: RecordingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recordingSession = await _context.RecordingSession.FindAsync(id);
            _context.RecordingSession.Remove(recordingSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordingSessionExists(int id)
        {
            return _context.RecordingSession.Any(e => e.ID == id);
        }

        //public IActionResult SearchPage() //load the first page
        public async Task<IActionResult> SearchPage(string message = "")
        {
            //attach the distinct values to the drop down box
            IQueryable<string> queryComposer = from m in _context.RecordingSession
                                         orderby m.ComposerName
                                         select m.ComposerName;
            IEnumerable<SelectListItem> itemsComposer = new SelectList(await queryComposer.Distinct().ToListAsync());
            ViewBag.ComposerName = itemsComposer;

            IQueryable<string> queryProducer = from m in _context.RecordingSession
                                               orderby m.ProducerName
                                               select m.ProducerName;
            IEnumerable<SelectListItem> itemsProducer = new SelectList(await queryProducer.Distinct().ToListAsync());
            ViewBag.ProducerName = itemsProducer;

            ViewBag.msg = message; //use for showing an alert box using simple javascript
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //prevent cross-site attack
        public async Task<IActionResult> displayRecordingSessionResult(string searchString, string ComposerName, string ProducerName)
        {
            string message = "";

            if (string.IsNullOrEmpty(searchString))
            {
                message = "Please enter any keyword inside the Song Name column before doing a search action!";
                return RedirectToAction("Search", new { message = message });
            }

            var recordSession = from m in _context.RecordingSession
                                select m;
            recordSession = recordSession.Where(s => s.SongName.Contains(searchString));

            if (!string.IsNullOrEmpty(ComposerName))
            {
                recordSession = recordSession.Where(s => s.SongName.Equals(ComposerName));
            }

            if (!string.IsNullOrEmpty(ProducerName))
            {
                recordSession = recordSession.Where(s => s.SongName.Equals(ProducerName));
            }

            ViewBag.msg = "Search Done! Please refer the result in this page!";

            return View(await recordSession.ToListAsync());
        }



    }
}
