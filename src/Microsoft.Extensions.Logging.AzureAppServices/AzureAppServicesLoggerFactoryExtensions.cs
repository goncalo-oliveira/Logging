﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Extension methods for <see cref="AzureAppServicesDiagnosticsLoggerProvider"/>.
    /// </summary>
    public static class AzureAppServicesLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds an Azure Web Apps diagnostics logger.
        /// </summary>
        /// <param name="collection">The extension method argument</param>
        public static IServiceCollection AddAzureWebAppDiagnostics(this IServiceCollection collection)
        {
            return AddAzureWebAppDiagnostics(collection, null);
        }

        /// <summary>
        /// Adds an Azure Web Apps diagnostics logger.
        /// </summary>
        /// <param name="collection">The extension method argument</param>
        /// <param name="settings">The setting object to configure loggers.</param>
        public static IServiceCollection AddAzureWebAppDiagnostics(this IServiceCollection collection, AzureAppServicesDiagnosticsSettings settings)
        {
            collection.AddLogging();
            if (WebAppContext.Default.IsRunningInAzureWebApp)
            {
                // Only add the provider if we're in Azure WebApp. That cannot change once the apps started
                collection.AddSingleton(WebAppContext.Default);
                collection.AddSingleton<ILoggerProvider>(new AzureAppServicesDiagnosticsLoggerProvider(WebAppContext.Default, settings ?? new AzureAppServicesDiagnosticsSettings()));
            }
            return collection;
        }

        /// <summary>
        /// <para>
        /// This method is obsolete and will be removed in a future version. The recommended alternative is to call the Microsoft.Extensions.Logging.AddAzureWebAppDiagnostics() extension method on the Microsoft.Extensions.Logging.LoggerFactory instance.
        /// </para>
        /// Adds an Azure Web Apps diagnostics logger.
        /// </summary>
        /// <param name="factory">The extension method argument</param>
        [Obsolete("This method is obsolete and will be removed in a future version. The recommended alternative is to call the Microsoft.Extensions.Logging.AddAzureWebAppDiagnostics() extension method on the Microsoft.Extensions.Logging.LoggerFactory instance.")]
        public static ILoggerFactory AddAzureWebAppDiagnostics(this ILoggerFactory factory)
        {
            return AddAzureWebAppDiagnostics(factory, new AzureAppServicesDiagnosticsSettings());
        }

        /// <summary>
        /// <para>
        /// This method is obsolete and will be removed in a future version. The recommended alternative is to call the Microsoft.Extensions.Logging.AddAzureWebAppDiagnostics() extension method on the Microsoft.Extensions.Logging.LoggerFactory instance.
        /// </para>
        /// Adds an Azure Web Apps diagnostics logger.
        /// </summary>
        /// <param name="factory">The extension method argument</param>
        /// <param name="settings">The setting object to configure loggers.</param>
        [Obsolete("This method is obsolete and will be removed in a future version. The recommended alternative is to call the Microsoft.Extensions.Logging.AddAzureWebAppDiagnostics() extension method on the Microsoft.Extensions.Logging.LoggerFactory instance.")]
        public static ILoggerFactory AddAzureWebAppDiagnostics(this ILoggerFactory factory, AzureAppServicesDiagnosticsSettings settings)
        {
            if (WebAppContext.Default.IsRunningInAzureWebApp)
            {
                // Only add the provider if we're in Azure WebApp. That cannot change once the apps started
                factory.AddProvider(new AzureAppServicesDiagnosticsLoggerProvider(WebAppContext.Default, settings));
            }
            return factory;
        }
    }
}
