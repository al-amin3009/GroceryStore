using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models
{
    public class UserLogin
    {
        static bool _islogin;
        static int _UserId;

        public static bool IsLogin
        {
            get
            {
                return _islogin;
            }
            set
            {
                _islogin = value;
            }
        }

        public static int UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                _UserId = value;
            }
        }
    }
}
