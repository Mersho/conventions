#!/usr/bin/env -S dotnet fsi

open System
open System.IO

#r "nuget: Mono.Unix, Version=7.1.0-final.1.21458.1"
#r "nuget: Fsdk, Version=0.6.0--date20230821-0702.git-5488853"

#load "../src/FileConventions/Library.fs"
#load "../src/FileConventions/Helpers.fs"

let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "..") |> DirectoryInfo

let invalidFiles =
    Helpers.GetInvalidFiles
        rootDir
        "*.fsx"
        (fun fileInfo -> not(FileConventions.HasCorrectShebang fileInfo))

Helpers.AssertNoInvalidFiles
    invalidFiles
    "The following files should have the correct shebang:"
