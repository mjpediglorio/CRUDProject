using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Enums.Common
{
    public enum DbResultTypes
    {
        OK_NONE = -1,
        OK = 0,
        INVALID_EMAIL = 1,
        INVALID_USERNAME = 2,
        INVALID_PASSWORD = 3,
        INVALID_CREDENTIAL = 4,
        INVALID_OTP = 5,
        INVALID_COMMAND = 6,
        INVALID_REQUEST = 7,
        INVALID = 8,
        NOT_FOUND = 9,
        EXISTING_EMAIL = 10,
        EXISTING_USERNAME = 11,
        EXISTING_CONTACTNUMBER = 12,
        SEND_EMAIL_ERROR = 13,

        ERROR = 500,
        DB_EXCEPTION = 501,
        SERVER_ERROR = 502,
    }
}
