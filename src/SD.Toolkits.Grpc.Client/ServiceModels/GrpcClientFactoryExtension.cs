using Grpc.Core;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Configuration;
using System;
using System.Reflection;

namespace SD.Toolkits.Grpc.Client.ServiceModels
{
    /// <summary>
    /// gRPC客户端工厂扩展
    /// </summary>
    internal static class GrpcClientFactoryExtension
    {
        /// <summary>
        /// 创建gRPC服务
        /// </summary>
        /// <param name="channel">gRPC信道</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="clientFactory">客户端工厂</param>
        /// <returns>gRPC服务实例</returns>
        public static object CreateGrpcService(this ChannelBase channel, Type serviceType, ClientFactory clientFactory = null)
        {
            Type clientFactoryType = typeof(GrpcClientFactory);
            Type[] argumentTypes = { typeof(ChannelBase), typeof(ClientFactory) };
            MethodInfo createGrpcServiceMethod = clientFactoryType.GetMethod("CreateGrpcService", BindingFlags.Public | BindingFlags.Static, null, argumentTypes, null);
            MethodInfo createGrpcServiceGenericMethod = createGrpcServiceMethod.MakeGenericMethod(serviceType);

            object[] arguments = { channel, clientFactory };
            object serviceInstance = createGrpcServiceGenericMethod.Invoke(null, arguments);

            return serviceInstance;
        }
    }
}
