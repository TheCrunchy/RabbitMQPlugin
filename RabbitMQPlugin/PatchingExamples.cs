using System;
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
        //public static class MQPluginPatch
        //{
        //    internal static readonly MethodInfo HandleMessagePatch = typeof(MQPluginPatch).GetMethod(nameof(HandleMessage), BindingFlags.Static | BindingFlags.Public) ??
        //                                                             throw new Exception("Failed to find patch method");

        //    private static Dictionary<string, Action<string>> Handlers = new Dictionary<string, Action<string>>();
        //    public static void Patch(PatchContext ctx)
        //    {
        //        var HandleMessageMethod = MQ.GetType().GetMethod("MessageHandler", BindingFlags.Instance | BindingFlags.Public);
        //        if (HandleMessageMethod == null) return;

        //        ctx.GetPattern(HandleMessageMethod).Suffixes.Add(HandleMessagePatch);
        //        Handlers.Add("Test", HandleExample1);
        //    }

        //    public static void HandleExample1(string MessageBody)
        //    {
        //        Do something here, maybe log to check it works
        //    }

        //    public static void HandleMessage(string MessageType, string MessageBody)
        //    {
        //        if (Handlers.TryGetValue(MessageType, out var action))
        //        {
        //            action.Invoke(MessageBody);
        //        }
        //    }
        //}


        //When you send a message, the server sending also receives it
        //so either do the logic only when receiving, or store a list of sent IDs in the object, if the incoming has that id, ignore it
        //public static ITorchPlugin MQ;
        //public static MethodInfo SendMessage;
        //public bool MQPluginInstalled = false;

        //how to invoke
        //    if (MQPluginInstalled)
        //    {
        //        var input = JsonConvert.SerializeObject("Test Message");
        //        var methodInput = new object[] { "Test", input };
        //        SendMessage?.Invoke(MQ, methodInput);
        //    }

        //public static void InitMQ(PluginManager Plugins, PatchManager Patches)
        //{

        //    if (Plugins.Plugins.TryGetValue(Guid.Parse("319afed6-6cf7-4865-81c3-cc207b70811d"), out var MQPlugin))
        //    {
        //        SendMessage = MQPlugin.GetType().GetMethod("SendMessage", BindingFlags.Public | BindingFlags.Instance, null, new Type[2] { typeof(string), typeof(string) }, null);
        //        MQ = MQPlugin;

        //        RabbitTest.MQPluginPatch.Patch(Patches.AcquireContext());
        //        Patches.Commit();

        //        MQPluginInstalled = true;
        //    }
        //}

        //private void SessionChanged(ITorchSession session, TorchSessionState newState)
        //{
        //    switch (newState)
        //    {
        //        case TorchSessionState.Loaded:
        //        {
        //            InitMQ(Torch.Managers.GetManager<PluginManager>(), Torch.Managers.GetManager<PatchManager>());
        //        }
        //        break;
        //    }
        //}
    }
}
