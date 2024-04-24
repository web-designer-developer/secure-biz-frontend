using FluentEmail.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestSharp;
using RestSharp.Authenticators;
using SecurityServices.Common;
using System.Net;
using System.Net.Mail;

namespace SecurityServices.Utlis
{
    public static class EmailSender
    {
        public static async Task<bool> SendEmail(string email, string otp)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(EnvironmentHelper.COMMON_EMAIL_ID),
                    Subject = "OTP validation",
                    IsBodyHtml = true,

                };


                message.To.Add(new MailAddress(email));
                var client = new SmtpClient("smtp.mailgun.org")
                {
                    //UseDefaultCredentials = true,
                    Port = 587,
                    Credentials = new NetworkCredential(EnvironmentHelper.COMMON_EMAIL_ID, EnvironmentHelper.COMMON_EMAIL_APIKEY),
                    EnableSsl = false,
                };

                await client.SendMailAsync(message);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static async Task<bool> SendEmail(IFluentEmail fluentEmail, string email, string otp, int otpType)
        {
            string body = "";
            string subject = "";
            try
            {
                if(otpType == 1) 
                {
                    body = $"<html><body>Thank you for signing up at securebiz.live - Here is your email verification code <br><br> <h2>{otp}</h2><br><sub>(This code will expire 10 minutes after it was sent) <br> If you think this email was sent to you by mistake, please write us at support@securebiz.live . <br> Thanks, <br> SecureBiz Team.<sub> </body></html>";
                    subject = "Verify your Email Address - SecureBiz";
                }
                else if(otpType == 2)
                {
                    body = $"<html><body>Please use below otp to login to SecureBiz <br><br> <h2>{otp}</h2><br><sub>(This code will expire 10 minutes after it was sent) <br> If you think this email was sent to you by mistake, please write us at support@securebiz.live . <br> Thanks, <br> SecureBiz Team.<sub> </body></html>";
                    subject = "Your Login OTP - SecureBiz";
                }
                else
                {
                    body = $"<html><body>Please use below otp to reset your password <br><br> <h2>{otp}</h2><br> <sub>(This code will expire 10 minutes after it was sent) <br> If you think this email was sent to you by mistake, please write us at support@securebiz.live . <br> Thanks, <br> SecureBiz Team.<sub> </body></html>";
                    subject = "Reset Password - SecureBiz";
                }

                var mail = await fluentEmail
                .To(email)
                .Subject(subject)
                .Body(body, true)
                .SendAsync();

                if (!mail.Successful)
                {
                    throw new Exception($"Email not Sent - {mail.ErrorMessages[0]}");
                }
                return true;

            }

            catch (Exception ex)
            {
                return false;

            }

        }

        


    }
}
