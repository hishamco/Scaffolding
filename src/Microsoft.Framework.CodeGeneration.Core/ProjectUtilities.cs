// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.Dnx.Compilation;
using Microsoft.Dnx.Compilation.CSharp;
using Microsoft.Dnx.Runtime;

namespace Microsoft.Framework.CodeGeneration
{
    public static class ProjectUtilities
    {
        private static string[] _frameworkProjectNames = new[]
        {
            "Microsoft.Framework.CodeGeneration",
            "Microsoft.Framework.CodeGeneration.Core",
            "Microsoft.Framework.CodeGeneration.Templating",
            "Microsoft.Framework.CodeGeneration.Sources",
            "Microsoft.Framework.CodeGenerators.Mvc",
        };

        public static CompilationReference GetProject(
            [NotNull]this ILibraryExporter libraryExporter,
            [NotNull]IApplicationEnvironment environment)
        {
            var export = libraryExporter.GetExport(environment.ApplicationName);

            var project = export.MetadataReferences
                .OfType<IMetadataProjectReference>()
                .OfType<IRoslynMetadataReference>()
                .Select(reference => reference.MetadataReference as CompilationReference)
                .FirstOrDefault();

            Contract.Assert(project != null);
            return project;
        }

        public static IEnumerable<CompilationReference> GetProjectsInApp(
            [NotNull]this ILibraryExporter libraryExporter,
            [NotNull]IApplicationEnvironment environment)
        {
            var export = libraryExporter.GetAllExports(environment.ApplicationName);

            return export.MetadataReferences
                .OfType<IMetadataProjectReference>()
                .OfType<IRoslynMetadataReference>()
                .Select(reference => reference.MetadataReference as CompilationReference)
                .Where(compilation => !_frameworkProjectNames.Contains(compilation.Compilation.AssemblyName));
        }
    }
}
