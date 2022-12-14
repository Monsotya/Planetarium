// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/planetarium.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace PlanetariumServiceGRPC {
  public static partial class Planetarium
  {
    static readonly string __ServiceName = "planetarium.Planetarium";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::PlanetariumServiceGRPC.HelloRequest> __Marshaller_planetarium_HelloRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PlanetariumServiceGRPC.HelloRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::PlanetariumServiceGRPC.HelloReply> __Marshaller_planetarium_HelloReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PlanetariumServiceGRPC.HelloReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::PlanetariumServiceGRPC.EmailInfo> __Marshaller_planetarium_EmailInfo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PlanetariumServiceGRPC.EmailInfo.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::PlanetariumServiceGRPC.EmailMessage> __Marshaller_planetarium_EmailMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PlanetariumServiceGRPC.EmailMessage.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::PlanetariumServiceGRPC.TicketsInfo> __Marshaller_planetarium_TicketsInfo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PlanetariumServiceGRPC.TicketsInfo.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::PlanetariumServiceGRPC.TicketsMessage> __Marshaller_planetarium_TicketsMessage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::PlanetariumServiceGRPC.TicketsMessage.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::PlanetariumServiceGRPC.HelloRequest, global::PlanetariumServiceGRPC.HelloReply> __Method_SayHello = new grpc::Method<global::PlanetariumServiceGRPC.HelloRequest, global::PlanetariumServiceGRPC.HelloReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SayHello",
        __Marshaller_planetarium_HelloRequest,
        __Marshaller_planetarium_HelloReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::PlanetariumServiceGRPC.EmailInfo, global::PlanetariumServiceGRPC.EmailMessage> __Method_SendEmail = new grpc::Method<global::PlanetariumServiceGRPC.EmailInfo, global::PlanetariumServiceGRPC.EmailMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SendEmail",
        __Marshaller_planetarium_EmailInfo,
        __Marshaller_planetarium_EmailMessage);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::PlanetariumServiceGRPC.TicketsInfo, global::PlanetariumServiceGRPC.TicketsMessage> __Method_BuyTickets = new grpc::Method<global::PlanetariumServiceGRPC.TicketsInfo, global::PlanetariumServiceGRPC.TicketsMessage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "BuyTickets",
        __Marshaller_planetarium_TicketsInfo,
        __Marshaller_planetarium_TicketsMessage);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::PlanetariumServiceGRPC.PlanetariumReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Planetarium</summary>
    [grpc::BindServiceMethod(typeof(Planetarium), "BindService")]
    public abstract partial class PlanetariumBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::PlanetariumServiceGRPC.HelloReply> SayHello(global::PlanetariumServiceGRPC.HelloRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::PlanetariumServiceGRPC.EmailMessage> SendEmail(global::PlanetariumServiceGRPC.EmailInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::PlanetariumServiceGRPC.TicketsMessage> BuyTickets(global::PlanetariumServiceGRPC.TicketsInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(PlanetariumBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SayHello, serviceImpl.SayHello)
          .AddMethod(__Method_SendEmail, serviceImpl.SendEmail)
          .AddMethod(__Method_BuyTickets, serviceImpl.BuyTickets).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, PlanetariumBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SayHello, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PlanetariumServiceGRPC.HelloRequest, global::PlanetariumServiceGRPC.HelloReply>(serviceImpl.SayHello));
      serviceBinder.AddMethod(__Method_SendEmail, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PlanetariumServiceGRPC.EmailInfo, global::PlanetariumServiceGRPC.EmailMessage>(serviceImpl.SendEmail));
      serviceBinder.AddMethod(__Method_BuyTickets, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::PlanetariumServiceGRPC.TicketsInfo, global::PlanetariumServiceGRPC.TicketsMessage>(serviceImpl.BuyTickets));
    }

  }
}
#endregion
