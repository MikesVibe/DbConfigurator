using DbConfigurator.Model;
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
                return Result.Ok(mailItems.Single());
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
