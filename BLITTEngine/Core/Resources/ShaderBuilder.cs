using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace BLITTEngine.Core.Resources
{
    public struct ShaderBuildResult
    {
        public readonly byte[] VsBytes;
        public readonly byte[] FsBytes;
        public readonly string[] Samplers;
        public readonly string[] Params;

        public ShaderBuildResult(byte[] vs_bytes, byte[] fs_bytes, string[] samplers, string[] _params)
        {
            this.VsBytes = vs_bytes;
            this.FsBytes = fs_bytes;
            this.Samplers = samplers;
            this.Params = _params;
        }
            
    }
    
    public static class ShaderBuilder
    {
        private const string COMPILER_PATH = "shaderc.exe";
        private const string INCLUDE_PATH = "shader_includes";
        private const string SAMPLER_REGEX_VAR = "sampler";
        private const string SAMPLER_REGEX = @"SAMPLER2D\s*\(\s*(?<sampler>\w+)\s*\,\s*(?<index>\d+)\s*\)\s*\;";
        private const string PARAM_REGEX_VAR = "param";
        private const string VEC_PARAM_REGEX = @"uniform\s+vec4\s+(?<param>\w+)\s*\;";
        
        public static ShaderBuildResult Build(string vs_src_path, string fs_src_path)
        {
            var directory = Path.GetDirectoryName(vs_src_path);

            Process proc_vs;
            Process proc_fs;

            string temp_vs_bin_output = string.Empty;
            string temp_fs_bin_output = string.Empty;

            string vs_build_result = string.Empty;
            string fs_build_result = string.Empty;

            var process_info = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = COMPILER_PATH
            };


            try
            {
                var vs_args = "--platform windows -p vs_4_0 -O 3 --type vertex -f $path -o $output -i $include";

                vs_args = vs_args.Replace("$path", vs_src_path);

                temp_vs_bin_output = Path.Combine(directory, "dx_" + Path.GetFileName(vs_src_path));

                vs_args = vs_args.Replace("$output", temp_vs_bin_output);

                vs_args = vs_args.Replace("$include", INCLUDE_PATH);

                process_info.Arguments = vs_args;
                
                proc_vs = Process.Start(process_info);

                proc_vs?.WaitForExit();
                
                var output = proc_vs?.ExitCode ?? -1;

                if (output != 0 && output != -1)
                {
                    using (var reader = proc_vs?.StandardError)
                    {
                        vs_build_result = reader?.ReadToEnd();
                        
                    }
                }

            }
            catch(Exception) {}

            try
            {
                var fs_args = "--platform windows -p ps_4_0 -O 3 --type fragment -f $path -o $output -i $include";

                fs_args = fs_args.Replace("$path", fs_src_path);

                temp_fs_bin_output = Path.Combine(directory, "dx_" + Path.GetFileName(fs_src_path));

                fs_args = fs_args.Replace("$output", temp_fs_bin_output);

                fs_args = fs_args.Replace("$include", INCLUDE_PATH);
                
                process_info.Arguments = fs_args;
                
                proc_fs = Process.Start(process_info);

                proc_fs?.WaitForExit();
                
                var output = proc_fs?.ExitCode ?? -1;
                
                if (output != 0 && output != -1)
                {
                    using (var reader = proc_fs?.StandardError)
                    {
                        fs_build_result = reader?.ReadToEnd();
                        
                    }
                }
                
            }
            catch(Exception) {}

            bool vs_ok = File.Exists(temp_vs_bin_output);
            bool fs_ok = File.Exists(temp_fs_bin_output);

            if (vs_ok && fs_ok)
            {
                var vs_bytes = File.ReadAllBytes(temp_vs_bin_output);
                var fs_bytes = File.ReadAllBytes(temp_fs_bin_output);

                var fs_stream = File.OpenRead(fs_src_path);
                
                ParseUniforms(fs_stream, out var samplers, out var _params);
                
                var result = new ShaderBuildResult(vs_bytes, fs_bytes, samplers, _params);
                
                File.Delete(temp_vs_bin_output);
                File.Delete(temp_fs_bin_output);

                return result;
            }
            else
            {
                if (vs_ok)    
                {
                    File.Delete(temp_vs_bin_output);
                }

                if (fs_ok)
                {
                    File.Delete(temp_fs_bin_output);
                }
                
                if (!vs_ok)
                {
                    throw new Exception("Error building vertex shader on " + vs_src_path + " : " + vs_build_result);
                }
                
                throw new Exception("Error building fragment shader on " + fs_src_path + " : " + fs_build_result);
            }
        }

        public static void ParseUniforms(Stream fs_stream, out string[] samplers, out string[] _params)
        {
            string line;
            
            Regex sampler_regex = new Regex(SAMPLER_REGEX);
            Regex param_regex = new Regex(VEC_PARAM_REGEX);
            
            var samplers_list = new List<string>();
            var params_list = new List<string>();
            
            using (var reader = new StreamReader(fs_stream))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    Match sampler_match = sampler_regex.Match(line);
                    
                    
                    if (sampler_match.Success)
                    {
                        string sampler_name = sampler_match.Groups[SAMPLER_REGEX_VAR].Value;
                        samplers_list.Add(sampler_name);
                    }
                    else
                    {
                        Match param_match = param_regex.Match(line);

                        if (param_match.Success)
                        {
                            string param_name = param_match.Groups[PARAM_REGEX_VAR].Value;
                            
                            params_list.Add(param_name);
                        }
                    }
                }
            }

            samplers = samplers_list.Count > 0 ? samplers_list.ToArray() : new string[] {};

            _params = params_list.Count > 0 ? params_list.ToArray() : new string[] {};
            
        }
    }
}