﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Tài khoản người sử dụng
    /// </summary>
    public class Account
    {
            /// <summary>
            /// Tên/ID tài khoản
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Tên đầy đủ
            /// </summary>
            public string FullName { get; set; }

            /// <summary>
            /// Email
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Photo { get; set; }
    }
}
