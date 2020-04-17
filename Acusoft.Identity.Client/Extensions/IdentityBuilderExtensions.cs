using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Acusoft.Identity.Client.Models;
using Acusoft.Identity.Protos;

namespace Acusoft.Identity.Client.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public class Url
        {

        }
        public static IdentityBuilder UseGrpcStores(this IdentityBuilder builder)
           => builder
               .AddGrpcUserStore()
               .AddGrpcRoleStore();

        private static IdentityBuilder AddGrpcUserStore(this IdentityBuilder builder)
        {
            var userStoreType = typeof(UserStore<>).MakeGenericType(builder.UserType);

            builder.Services.AddScoped(
                typeof(IUserStore<>).MakeGenericType(builder.UserType),
                userStoreType
            );

            return builder;
        }

        private static IdentityBuilder AddGrpcRoleStore(this IdentityBuilder builder)
        {
            var roleStoreType = typeof(RoleStore<>).MakeGenericType(builder.RoleType);

            builder.Services.AddScoped(
                typeof(IRoleStore<>).MakeGenericType(builder.RoleType),
                roleStoreType
            );

            return builder;
        }

        public static IdentityBuilder AddGrpcErrorDescriber<TErrorDescriber>(
            this IdentityBuilder builder) where TErrorDescriber : GrpcErrorDescriber
        {
            builder.Services.AddScoped<GrpcErrorDescriber, TErrorDescriber>();
            return builder;
        }
    }
}
