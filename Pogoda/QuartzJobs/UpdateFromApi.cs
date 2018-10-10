using Pogoda.Controllers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pogoda.QuartzJobs
{
    public class UpdateFromApi : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            WeathersController instance = new WeathersController();
            instance.UpdateAllWeathersForAllUsers();
        }

    }
}