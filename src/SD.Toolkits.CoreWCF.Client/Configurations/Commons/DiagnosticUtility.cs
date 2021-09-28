using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SD.Toolkits.CoreWCF.Client.Configurations.Commons
{
    internal class DiagnosticUtility
    {
        private static ExceptionUtility s_exceptionUtility = (ExceptionUtility)null;
        private static readonly object s_lockObject = new object();

        internal static ExceptionUtility ExceptionUtility
        {
            get
            {
                return DiagnosticUtility.s_exceptionUtility ?? DiagnosticUtility.GetExceptionUtility();
            }
        }

        private static ExceptionUtility GetExceptionUtility()
        {
            lock (DiagnosticUtility.s_lockObject)
            {
                if (DiagnosticUtility.s_exceptionUtility == null)
                {
                    // TODO: Make this generic shared code used by multiple assemblies
                    //exceptionUtility = new ExceptionUtility("System.ServiceModel", "System.ServiceModel 4.0.0.0", (object)DiagnosticUtility.diagnosticTrace, (object)FxTrace.Exception);
                    DiagnosticUtility.s_exceptionUtility = new ExceptionUtility();
                }
            }

            return DiagnosticUtility.s_exceptionUtility;
        }

        internal static void TraceHandledException(Exception exception, TraceEventType traceEventType)
        {
            //FxTrace.Exception.TraceHandledException(exception, traceEventType);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static Exception FailFast(string message)
        {
            try
            {
                try
                {
                    DiagnosticUtility.ExceptionUtility.TraceFailFast(message);
                }
                finally
                {
                    Environment.FailFast(message);
                }
            }
            catch
            {
            }
            Environment.FailFast(message);
            return (Exception)null;
        }
    }
}