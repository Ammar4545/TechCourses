﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTree.Interface
{
    public interface IPrimaryProperties
    {
        int Id { get; set; }
        string Title { get; set; }
    }
}
