$s = 0
$e = 0

Write-Output ""
Write-Output "--------------------"
Write-Output "Compiling Shaders..."
Write-Output "--------------------"

Function Compile ($filter, $type, $profile, $glProfile) {
	foreach ($file in (ls -Path $dir -Filter $filter)) {
		$path = $file | Resolve-Path -Relative
        $outname = [io.path]::ChangeExtension($file.Name, "bin")
		
		Write-Output ("Compiling {0}..." -f $path)
		
		&..\..\Tools\shaderc.exe --platform linux -p $glProfile --type $type -f "$path" -o ".\bin\glsl\$outname" -i ..\
		
		Write-Output ("ExitCode GLSL: {0}" -f $LastExitCode)
		
		if ($LastExitCode -eq 0) { $Global:s++ } Else { $Global:e++ }
		
        &..\..\Tools\shaderc.exe --platform windows -p $profile -O 3 --type $type -f "$path" -o ".\bin\hlsl\$outname" -i ..\
		
		Write-Output ("ExitCode HLSL: {0}" -f $LastExitCode)
		
		if ($LastExitCode -eq 0) { $Global:s++ } Else { $Global:e++ }
	}
}

foreach ($dir in (ls -Directory)) {
	Compile "vs_*.sc" "vertex" "vs_4_0" "120"
	Compile "fs_*.sc" "fragment" "ps_4_0" "120"
}

Write-Output ""
Write-Output ("Shader Build: {0} succeeded, {1} failed" -f $s,$e)