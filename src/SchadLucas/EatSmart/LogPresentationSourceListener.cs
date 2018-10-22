using NLog;
using System;
using System.Diagnostics;

namespace SchadLucas.EatSmart
{
    public sealed class LogPresentationSourceListener : TraceListener
    {
        private Logger Logger { get; }

        public LogPresentationSourceListener(string name)
        {
            Logger = LogManager.GetLogger(name);
        }

        private void Log(TraceEventType eventType, string message)
        {
            Action<string> method;

            switch (eventType)
            {
                case TraceEventType.Verbose:
                    method = Logger.Trace;
                    break;

                case TraceEventType.Information:
                    method = Logger.Info;
                    break;

                case TraceEventType.Warning:
                    method = Logger.Warn;
                    break;

                case TraceEventType.Error:
                    method = Logger.Error;
                    break;

                case TraceEventType.Critical:
                    method = Logger.Fatal;
                    break;

                default:
                    method = s => { };
                    break;
            }

            method.Invoke(message);
        }

        public override void Write(string message)
        {
            //Logger.Log(_logLevel, message);
        }

        public override void WriteLine(string message)
        {
            //Logger.Log(_logLevel, message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            Log(eventType, message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            Log(eventType, string.Format(format, args));
        }
    }
}