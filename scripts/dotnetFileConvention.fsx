#!/usr/bin/env -S dotnet fsi

open System
open System.IO
open System.Text.RegularExpressions

#r "nuget: Mono.Unix, Version=7.1.0-final.1.21458.1"
#r "nuget: Fsdk, Version=0.6.0--date20230821-0702.git-5488853"

open Fsdk
open Fsdk.Process

#load "../src/FileConventions/Helpers.fs"
#load "../src/FileConventions/Library.fs"
open FileConventions
open Helpers

let args = Misc.FsxOnlyArguments()

if args.Length > 1 then
    Console.Error.WriteLine
        "Usage: dotnetFileConvention.fsx [projectFolder(optional)]"

    Environment.Exit 1

let rootDir = FileInfo args.[0]

// DefiningEmptyStringsWithDoubleQuotes
let allSourceFiles = GetFiles (DirectoryInfo rootDir.FullName) "*.fs"
printfn "%A" (String.Join("\n", allSourceFiles))
let allProjFiles = ReturnAllProjectSourceFile rootDir ["*.csproj"; "*.fsproj"]

//for sourceFile in allSourceFiles do
//    let isStringEmpty = DefiningEmptyStringsWithDoubleQuotes (FileInfo sourceFile)
//    if isStringEmpty then
//        failwith (sprintf "%s file: Contains empty strings specifed with \"\" , you should use String.Empty()" sourceFile)


// ProjFilesNamingConvention

//for projfile in allProjFiles do
//    let isWrongProjFile = ProjFilesNamingConvention(FileInfo projfile)
//    if isWrongProjFile then
//        failwith (sprintf "%s file: Project file or Project directory is incorrect!\n
//        Fix: use same name on .csproj/.fsproj on parrent project directory" projfile)

//// NotFollowingNamespaceConvention 
//for sourceFile in allSourceFiles do
//    let isWrongNamespace = NotFollowingNamespaceConvention (FileInfo sourceFile)
//    if isWrongNamespace then
//        failwith (sprintf "%s file: has wrong namespace!" sourceFile)

// NotFollowingConsoleAppConvention
for projfile in allProjFiles do
    let isWrongConsoleApplication = NotFollowingConsoleAppConvention projfile
    printfn "%A" projfile 
    if isWrongConsoleApplication then
        failwith (sprintf "%s project: Should not contain console methods or printf" projfile.FullName)
