// include Fake lib
#r @"packages\FAKE.1.74.196.0\tools\FakeLib.dll"
open Fake 

// Properties
let buildDir = @".\Build\"
let nugetDir = @".\NuGet\"

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
    CleanDir nugetDir
)
 
Target "BuildApp" (fun _ ->
    !! @"InjectableSettings\**\*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildNuGet" (fun _ -> 
    let nugetLibDir = nugetDir @@ "lib/net20"
        
    XCopy buildDir nugetLibDir

    NuGet (fun p -> 
            { p with
                OutputPath = nugetDir
                Version = "1.0.0"
                Publish = false })  "InjectableSettings.nuspec"
)

// Dependencies
"Clean"
  ==> "BuildApp"
  ==> "BuildNuGet"

// start build
Run "BuildNuGet"
