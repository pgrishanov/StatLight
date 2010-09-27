﻿using System;
using System.Reflection;
using StatLight.Client.Harness.Events;
using StatLight.Client.Harness.Messaging;

namespace StatLight.Client.Harness.Hosts
{
    public static class ReflectionInfoHelper
    {
        public static string ReadClassName(this Type type)
        {
            return type.FullName.Substring(type.Namespace.Length + 1);
        }

        public static string FullName(this MemberInfo methodInfo)
        {
            string m = "{0}.{1}.{2}".FormatWith(
                        methodInfo.ReflectedType.Namespace,
                        methodInfo.ReflectedType.ReadClassName(),
                        methodInfo.Name);
            return m;
        }


        public static void HandleReflectionTypeLoadException(ReflectionTypeLoadException rfex)
        {
            string loaderExceptionMessages = "";
            //string msg = "********************* " + helperMessage + "*********************";
            foreach (var t in rfex.LoaderExceptions)
            {
                loaderExceptionMessages += "   -  ";
                loaderExceptionMessages += t.Message;
                loaderExceptionMessages += Environment.NewLine;
            }

            string msg = @"
********************* ReflectionTypeLoadException *********************
***** Begin Loader Exception Messages *****
{0}
***** End Loader Exception Messages *****
".FormatWith(loaderExceptionMessages);

            Server.Trace(msg);
        }
    }
}