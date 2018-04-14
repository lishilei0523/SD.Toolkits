using Quartz;
using SD.Toolkits.TaskScheduler.ITask;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD.Toolkits.TaskScheduler.Tests.StubTasks
{
    /// <summary>
    /// 报时任务
    /// </summary>
    public class StubShowTimeTask : BaseTask<StubShowTimeTask>
    {
        /// <summary>
        /// 参照时间集
        /// </summary>
        public static readonly ICollection<DateTime> ReferenceTimes = new HashSet<DateTime>();

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context">执行上下文</param>
        public override async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => ReferenceTimes.Add(DateTime.Now));
        }
    }
}
