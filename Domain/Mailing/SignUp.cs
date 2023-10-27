using Domain.Db.Entities;
using Domain.Interfaces.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mailing
{
    public class SignUp : IMailContent
    {
        public string EmailSubjectGet()
        {
            return "Account Activation";
        }

        public string GenerateHtml(DbAuthUsers user, string content)
        {
            string html = @"
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Verify Your Account</title>
    <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
    <link
        href=""https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap""
        rel=""stylesheet"">
    <style>
        * {
            padding: 0px;
            margin: 0px;

            font-family: 'Roboto';
        }

        .main-container {
            width: 100%;
            height: 700px;
        }

        .content-container {
            background-color: white;
            margin: 0px auto;
            width: 375px;
            height: 100%;
        }

        .header-container {
            padding: 21px 25px 0px;
            background-color: #05060e;
            width: 100%;
            height: 70px;
            box-sizing: border-box;
        }

        .header-h1 {
            font-size: 22px;
            font-weight: 400;
            color: white;
        }

        .body-container {
            background-color: #a0aab4;
            width: 325px;
            height: 300px;
            margin: 25px auto 0px;
            box-sizing: border-box;
            padding: 15px 15px 0px;
        }

        .body-content {
            letter-spacing: 0.25px;
            color: #05060e;
            font-size: 14px;
        }

        .bold {
            font-weight: bold;
        }
    </style>
</head>

<body>
    <div class=""main-container"">
        <div class=""content-container"">
            <div class=""header-container"">
                <h1 class=""header-h1"">
                    Welcome to DreamLand
                </h1>
            </div>
            <div class=""body-container"">
                <p class=""body-content"">
                    Dear "+user.FirstName +@",
                </p>
                <p class=""body-content"" style=""margin-top:20px"">
                    Exciting news - your Dreamland account awaits! To start blogging, use this One-Time Password (OTP):
                    <span class=""bold"">"+content+@"</span> for
                    verification.
                </p>
                <p class=""body-content"" style=""margin-top:20px"">
                    Dreamland is your creative hub. Dive in, share your thoughts, and connect with fellow bloggers. Need
                    help? We're here
                    24/7.
                </p>

                <p class=""body-content"" style=""margin-top:20px"">
                    Let's make your blogging dreams come true!
                </p>
            </div>
        </div>
    </div>
</body>

</html>";
            return html;
        }
    }
}
