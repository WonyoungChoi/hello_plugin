﻿using System;
using System.Threading.Tasks;
using Tizen.Flutter.Embedding;
using Tizen.System;

namespace Plugins.Tests
{
    public class HelloPlugin : IFlutterPlugin
    {
        private MethodChannel _channel;

        public void OnAttachedToEngine(IFlutterPluginBinding binding)
        {
            _channel = new MethodChannel("hello_plugin/method", StandardMethodCodec.Instance, binding.BinaryMessenger);
            _channel.SetMethodCallHandler(HandleMethodCall);
        }

        public void OnDetachedFromEngine()
        {
            _channel.SetMethodCallHandler(null);
            _channel = null;
        }

        public Task<object> HandleMethodCall(MethodCall call)
        {
            if (call.Method == "getPlatformVersion")
            {
                if (Information.TryGetValue<string>("http://tizen.org/feature/platform.version", out string version))
                {
                    return Task.FromResult<object>(version);
                }
                else
                {
                    return Task.FromResult<object>("Unknown");
                }
            }
            else
            {
                return Task.FromResult<object>(null);
            }
        }

    }
}

