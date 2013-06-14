// include Fake lib
#r @"packages\FAKE.1.74.196.0\tools\FakeLib.dll"
open Fake 

// Properties
let buildDir = @".\Build\"

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
)
 
Target "BuildApp" (fun _ ->
    !! @"InjectableSettings\**\*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

// Dependencies
"Clean"
  ==> "BuildApp"

// start build
Run "BuildApp"