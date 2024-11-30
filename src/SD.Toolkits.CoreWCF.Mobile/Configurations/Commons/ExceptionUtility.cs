using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SD.Toolkits.CoreWCF.Mobile.Configurations.Commons
{
    internal class ExceptionUtility
    {
        internal ArgumentException ThrowHelperArgument(string message)
        {
            return (ArgumentException)this.ThrowHelperError(new ArgumentException(message));
        }

        internal ArgumentException ThrowHelperArgument(string paramName, string message)
        {
            return (ArgumentException)this.ThrowHelperError(new ArgumentException(message, paramName));
        }

        internal ArgumentNullException ThrowHelperArgumentNull(string paramName)
        {
            return (ArgumentNullException)this.ThrowHelperError(new ArgumentNullException(paramName));
        }

        internal Exception ThrowHelperError(Exception exception)
        {
            return this.ThrowHelper(exception, TraceEventType.Error);
        }

        internal Exception ThrowHelperWarning(Exception exception)
        {
            return this.ThrowHelper(exception, TraceEventType.Warning);
        }

        internal Exception ThrowHelper(Exception exception, TraceEventType eventType)
        {
            return this.ThrowHelper(exception, eventType, null);
        }

        internal Exception ThrowHelper(Exception exception, TraceEventType eventType, TraceRecord extendedData)
        {
            //if ((_diagnosticTrace == null ? 0 : (_diagnosticTrace.ShouldTrace(eventType) ? 1 : 0)) != 0)
            //{
            //    using (
            //        ExceptionUtility.useStaticActivityId
            //            ? Activity.CreateActivity(ExceptionUtility.activityId)
            //            : (Activity)null)
            //        _diagnosticTrace.TraceEvent(eventType, 131075,
            //            LegacyDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics",
            //                "ThrowingException"), TraceSR.Format("ThrowingException"), extendedData, exception,
            //            (object)null);
            //    IDictionary data = exception.Data;
            //    if (data != null && !data.IsReadOnly && !data.IsFixedSize)
            //    {
            //        object obj =
            //            data[(object)"System.ServiceModel.Diagnostics.ExceptionUtility.ExceptionStackAsString"];
            //        string str1 = obj == null ? "" : obj as string;
            //        if (str1 != null)
            //        {
            //            string stackTrace = exception.StackTrace;
            //            if (!string.IsNullOrEmpty(stackTrace))
            //            {
            //                string str2 = str1 + (str1.Length == 0 ? "" : Environment.NewLine) + "throw" +
            //                              Environment.NewLine + stackTrace + Environment.NewLine + "catch" +
            //                              Environment.NewLine;
            //                data[(object)"System.ServiceModel.Diagnostics.ExceptionUtility.ExceptionStackAsString"]
            //                    = (object)str2;
            //            }
            //        }
            //    }
            //}
            //this.exceptionTrace.TraceEtwException(exception, eventType);
            return exception;
        }



        internal Exception ThrowHelperCritical(Exception exception)
        {
            return this.ThrowHelper(exception, TraceEventType.Critical);
        }

        internal class TraceRecord
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal void TraceFailFast(string message)
        {
            //Microsoft.Runtime.Diagnostics.EventLogger logger = null;
            //try
            //{
            //    logger = new Microsoft.Runtime.Diagnostics.EventLogger(this.eventSourceName, this.diagnosticTrace);
            //}
            //finally
            //{
            //    TraceFailFast(message, logger);
            //}
        }

        internal Exception ThrowHelperArgumentNull(string paramName, string message)
        {
            return (ArgumentNullException)this.ThrowHelperError(new ArgumentNullException(paramName, message));
        }
    }
}
