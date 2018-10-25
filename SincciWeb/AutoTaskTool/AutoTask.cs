using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SincciKC.AutoTaskTool
{
    public class AutoTask:Registry
    {
        public AutoTask()
        {
            Schedule<AutoTaskJob>().ToRunEvery(1).Days().At(3,0);
        }
    }
}