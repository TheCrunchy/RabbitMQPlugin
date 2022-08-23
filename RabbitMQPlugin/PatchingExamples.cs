﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Torch.Managers.PatchManager;

namespace RabbitMQPlugin
{
    public class PatchingExamples
    {
        //[PatchShim]
        //public class MQPluginPatch
        //{
        //    internal static readonly MethodInfo HandleMessageMethod = Type.GetType("RabbitMQPlugin.Core").GetMethod("HandleMessage", BindingFlags.Instance | BindingFlags.NonPublic);
        //    internal static readonly MethodInfo HandleMessagePatch = typeof(MQPluginPatch).GetMethod(nameof(HandleMessage), BindingFlags.Static | BindingFlags.Public) ??
        //                                                             throw new Exception("Failed to find patch method");
        //    public static void Patch(PatchContext ctx)
        //    {
        //        if (HandleMessageMethod != null)
        //        {
        //            ctx.GetPattern(HandleMessageMethod).Suffixes.Add(HandleMessagePatch);
        //        }
        //    }

        //    public static void HandleMessage(string MessageType, string MessageBody)
        //    {
        //        switch (MessageType)
        //        {
        //            case "ExampleType1":
        //                return;
        //            case "ExampleType2":
        //                return;
        //            default:
        //                return;
        //        }
        //    }
        //}


        //public static MethodInfo SendMessage;

        //TODO change this guid to be whatever it actually is, do this when torch session is loaded 
        //if (Session.Managers.GetManager<PluginManager>().Plugins.TryGetValue(Guid.Parse("74796707-646f-4ebd-8700-d077a5f47af3"), out var mq))
        //{
        //    var mq = all.GetType().Assembly.GetType("RabbitMQPlugin.Core");
        //    try
        //    {
        //        SendMessage = mq.GetType().GetMethod("SendMessage", BindingFlags.Public | BindingFlags.Instance, null, new Type[2] { typeof(string), typeof(string)}, null);
        //    }
        //    catch (Exception ex)
        //    {
        //     
        //    }
        //}

        //how to invoke
        //var methodInput = new object[] { MessageType, JsonMessageBody };
        //SendMessage.Invoke(null, methodInput);
    }
}