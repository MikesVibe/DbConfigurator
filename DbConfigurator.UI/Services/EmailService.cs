using DbConfigurator.Model;
using DbConfigurator.Model.Entities.Core;
using FluentResults;
using HtmlAgilityPack;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class EmailService
    {
        private MailItem? _lastSelectedMailItem;

        public Result<EmailData> GetEmailData()
        {
            var toReturn = new EmailData();

            var selectedEmail = GetSelectedEmail();
            if (selectedEmail.IsFailed)
                return Result.Fail("Failed get data from email.");
            
            var emailData = ProcessMailItem(selectedEmail.Value);
            if (emailData.IsFailed)
                return Result.Fail("Failed get data from email.");
        
            return Result.Ok(emailData.Value);
        }
        public bool CreateReplayEmail(DistributionList distributionList, NotificationData notificationData)
        {
            if(_lastSelectedMailItem is null)
                return false;

            var reply = _lastSelectedMailItem.Reply();
            reply.To = string.Join("; ", distributionList.RecipientsTo.Select(r => r.Email));
            reply.CC = string.Join("; ", distributionList.RecipientsCc.Select(r => r.Email));

            reply.Subject = $"{notificationData.TicketNumber} | {notificationData.Priority} | {notificationData.GBU} | {notificationData.TicketSummary}";

            var body =
                $"Type: {notificationData.TicketType.ToString()}<br/>" +
                $"Ticket Number: {notificationData.TicketNumber}<br/>" +
                $"Summary: {notificationData.TicketSummary}<br/>" +
                $"Priority: {notificationData.Priority}<br/>" +
                $"Reported By: {notificationData.ReportedBy}<br/>" +
                $"Opened By: {notificationData.OpenedBy}<br/>" +
                $"GBUs: {notificationData.GBU}<br/>" +
                $"Reported Date: {notificationData.ReportedDate.ToString()}<br/>" +
                $"Opened Date: {notificationData.OpenedDate.ToString()}<br/>" +
                $"Description: {notificationData.TicketDescription}";


            reply.HTMLBody = reply.HTMLBody.Insert(0, body);

            reply.Display();

            return true;
        }


        private Result<EmailData> ProcessMailItem(MailItem mailItem)
        {
            if (mailItem.Attachments.Count != 1)
                return Result.Fail("Failed to read attachment from email.");

            Attachment attachment = mailItem.Attachments[1];

            // Read attachment content
            string attachmentContent = ReadAttachmentContent(attachment);

            var emailData = JsonSerializer.Deserialize<EmailData>(attachmentContent);
            if(emailData is null)
                return Result.Fail("Failed to read data from attachment.");
            
            emailData.ReportedDate = mailItem.ReceivedTime;
            return Result.Ok(emailData);
        }

        private string ReadAttachmentContent(Attachment attachment)
        {
            // Check if the attachment is a text file
            if (attachment.Type == OlAttachmentType.olByValue)
            {
                try
                {
                    // Read the content of the attachment
                    string attachmentContent = System.Text.Encoding.UTF8.GetString((byte[])attachment.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x37010102"));

                    return attachmentContent;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error reading attachment content: {ex.Message}");
                }
            }

            return string.Empty;
        }

        private Result<MailItem> GetSelectedEmail()
        {
            Selection selection = new Application().ActiveExplorer().Selection;
            List<MailItem> mailItems = new();

            foreach (MailItem email in selection)
            {
                mailItems.Add(email);
            }

            try
            {
                _lastSelectedMailItem = mailItems.Single();
                return Result.Ok(_lastSelectedMailItem);
            }
            catch
            {
                return Result.Fail("Failed get data from email.");
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(selection);
            }
        }

    }
}
