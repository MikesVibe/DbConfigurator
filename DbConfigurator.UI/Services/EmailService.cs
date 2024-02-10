using DbConfigurator.Core;
using DbConfigurator.Core.Models;
using FluentResults;
using HtmlAgilityPack;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class EmailService
    {
        const string attachmentName = "TicketData.txt";

        private MailItem? _lastValidSelectedMailItem;

        public Result<EmailData> GetEmailData()
        {
            var toReturn = new EmailData();

            var selectedEmail = GetSelectedEmail();
            if (selectedEmail.IsFailed)
                return Result.Fail(selectedEmail.Errors.First().Message);

            var emailData = ProcessMailItem(selectedEmail.Value);
            if (emailData.IsFailed)
                return Result.Fail(emailData.Errors.First().Message);

            //Important that this code is placed after processingMailItem so it is valid email to respond
            _lastValidSelectedMailItem = selectedEmail.Value;

            return Result.Ok(emailData.Value);
        }
        public Result CreateReplayEmail(DistributionList distributionList, NotificationData notificationData)
        {
            if (_lastValidSelectedMailItem is null)
                return Result.Fail("Replying is only possible for properly selected email with valid data inside attachment.");

            var reply = _lastValidSelectedMailItem.Reply();
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

            return Result.Ok();
        }


        private Result<EmailData> ProcessMailItem(MailItem mailItem)
        {
            if(mailItem.Attachments.Count == 0)
                return Result.Fail($"Couldn't find propper attachment in selected email.\nProper name for attachment is \"{attachmentName}\"");

            foreach (Attachment attachment in mailItem.Attachments)
            {
                if(attachment.FileName != attachmentName)
                {
                    continue;
                }

                // Read attachment content
                string attachmentContent = ReadAttachmentContent(attachment);

                var emailData = JsonSerializer.Deserialize<EmailData>(attachmentContent);
                if (emailData is null)
                    return Result.Fail("Couldn't serialize data from attachment.");

                emailData.ReportedDate = mailItem.ReceivedTime;
                return Result.Ok(emailData);
            }

            return Result.Fail($"Couldn't find propper attachment in selected email.\nProper name for attachment is \"{attachmentName}\"");
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
            try
            {
                Selection selection = new Application().ActiveExplorer().Selection;
                List<MailItem> mailItems = new();

                foreach (MailItem email in selection)
                {
                    mailItems.Add(email);
                }

                return Result.Ok(mailItems.Single());
            }
            catch (NullReferenceException ex)
            {
                return Result.Fail("Could not get data from email. Make sure that outlook app is open.");
            }
            catch (InvalidOperationException ex)
            {
                return Result.Fail("Invalid number of emails selected, please select exactly one email.");
            }
            catch (COMException ex)
            {
                return Result.Fail("Could not get data from email. Outlook application has to be opened with the same windows user as this application e.g. Administrator");
            }
            catch (System.Exception ex)
            {
                return Result.Fail($"Some unexpected error occured.\nPlease inform administrator about this error.\nException message:{ex}");
            }
        }

    }
}
