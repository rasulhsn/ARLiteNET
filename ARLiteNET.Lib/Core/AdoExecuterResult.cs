﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ARLiteNET.Lib
{
    public class AdoExecuterResult
    {
        public int AffectedRows { get; internal set; }
        public IEnumerable<Exception> Errors { get; internal set; }

        public bool IsSuccess
        {
            get
            {
                return Errors != null && Errors.Count() > 0 ? false : true;
            }
        }
    }

    public class AdoExecuterResult<T> : AdoExecuterResult
    {
        public T Object { get; internal set; }
    }
}
