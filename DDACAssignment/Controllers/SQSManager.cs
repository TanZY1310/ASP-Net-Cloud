using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DDACAssignment.Models;

namespace DDACAssignment.Controllers
{
    public class SQSManager : Controller
    {

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

        public async Task<IActionResult> Index(string msg = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Identity/Account/Login");
            }
            
            List<string> credentialInfo = getAWSCredential();
            var sqsclient = new AmazonSQSClient(credentialInfo[0], credentialInfo[1], credentialInfo[2], Amazon.RegionEndpoint.USEast1);
            var queueURL = await sqsclient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });

            //customerinfo => object data, string => delete token
            List<KeyValuePair<RecordingSession, string>> recordingSession = new List<KeyValuePair<RecordingSession, string>>();

            try
            {
                //create received request
                ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
                receiveMessageRequest.QueueUrl = queueURL.QueueUrl;
                receiveMessageRequest.MaxNumberOfMessages = 10;
                receiveMessageRequest.WaitTimeSeconds = 20;  //polling - short polling = 0(min) secons, long polling = 1-20(max) seconds
                receiveMessageRequest.VisibilityTimeout = 20;  //to block other customer seeing during the current consumer reading
                ReceiveMessageResponse receivedContent = await sqsclient.ReceiveMessageAsync(receiveMessageRequest);

                //check whether message received or not
                if (receivedContent.Messages.Count > 0)
                {
                    for (int i = 0; i < receivedContent.Messages.Count; i++)
                    {
                        var session = JsonConvert.DeserializeObject<RecordingSession>(receivedContent.Messages[i].Body);
                        var deleteToken = receivedContent.Messages[i].ReceiptHandle;
                        recordingSession.Add(new KeyValuePair<RecordingSession, string>(session, deleteToken));
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Songs", new { msg = "There are no recording sessions!" });
                }
            }
            catch (AmazonSQSException ex)
            {
                return RedirectToAction("Index", "Songs", new { msg = "Error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Songs", new { msg = "Error: " + ex.Message });
            }

            return View(recordingSession);
        }

        public async Task<IActionResult> deleteMessage(string deleteToken)
        {
            List<string> credentialInfo = getAWSCredential();
            var sqsclient = new AmazonSQSClient(credentialInfo[0], credentialInfo[1], credentialInfo[2], Amazon.RegionEndpoint.USEast1);
            var queueURL = await sqsclient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });

            try
            {
                var delRequest = new DeleteMessageRequest
                {
                    QueueUrl = queueURL.QueueUrl,
                    ReceiptHandle = deleteToken
                };
                var delResponse = await sqsclient.DeleteMessageAsync(delRequest);
            }
            catch (AmazonSQSException ex)
            {
                return RedirectToAction("Index", "Songs", new { msg = "Error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Songs", new { msg = "Error: " + ex.Message });
            }

            return RedirectToAction("Index", "Songs", new { msg = "Session Accepted!" });
        }
    }
}
