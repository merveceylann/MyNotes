using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity.Messages
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExist=101,
        EmailAlreadyExist=102,
        UserIsNotActive=151,
        UsernameOrPassWrong=152,
        CheckYourEmail=153,
        UserAlreadyActive=154,
        ActiveIdDoesNotExist=155,
        UserNotFound=156,
        ProfileCouldNotUpdate=157,
        UserCouldNotRemove=158,
        UserCouldNotFind=159,
        UserCouldNotInsert=160,
        UserCouldNotUpdated=161,
    }
}
