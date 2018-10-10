using Pogoda.Models;
using Pogoda.QuartzJobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pogoda
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail update = JobBuilder.Create<UpdateFromApi>().Build();
      

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("apiUpdateTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10) //Seconds Minutes
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(update, trigger);

        }
    }
}
