﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Microsoft.Framework.CodeGeneration.EntityFramework
{
    public class AddModelResult
    {
        public bool Added { get; set; }

        public SyntaxTree OldTree { get; set; }

        public SyntaxTree NewTree { get; set; }
    }
}
