// ################################################################################
// Part of PowerShell module : GenXdev.Console
// Original cmdlet filename  : Stop-TextToSpeech.cs
// Original author           : René Vaessen / GenXdev
// Version                   : 3.3.2026
// ################################################################################
// Copyright (c)  René Vaessen / GenXdev
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ################################################################################



using System.Management.Automation;

namespace GenXdev.Console
{
    /// <summary>
    /// <para type="synopsis">
    /// Immediately stops any ongoing text-to-speech output.
    /// </para>
    ///
    /// <para type="description">
    /// Halts all active and queued speech synthesis by canceling both standard and
    /// customized speech operations. This provides an immediate silence for any ongoing
    /// text-to-speech activities.
    /// </para>
    ///
    /// <example>
    /// <para>Example description</para>
    /// <para>Detailed explanation of the example.</para>
    /// <code>
    /// Stop-TextToSpeech
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example description</para>
    /// <para>Detailed explanation of the example.</para>
    /// <code>
    /// say "Hello world"; sst
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet("Stop", "TextToSpeech")]
    [Alias("sst")]
    [OutputType(typeof(void))]
    public class StopTextToSpeechCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// Begin processing - write verbose message about initiating speech cancellation
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose("Initiating speech cancellation request");
        }

        /// <summary>
        /// Process record - cancel all speech operations if confirmed
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                // Check if the operation should proceed
                if (ShouldProcess("Text-to-speech output", "Stop"))
                {
                    // Cancel all pending standard speech operations
                    GenXdev.Helpers.Misc.Speech.SpeakAsyncCancelAll();

                    // Cancel all pending customized speech operations
                    GenXdev.Helpers.Misc.SpeechCustomized.SpeakAsyncCancelAll();

                    WriteVerbose("Successfully cancelled all speech operations");
                }
            }
            catch
            {
                // Silently handle any speech cancellation errors to match original behavior
                WriteVerbose("Error occurred while attempting to cancel speech");
            }
        }

        /// <summary>
        /// End processing - empty implementation matching original PowerShell function
        /// </summary>
        protected override void EndProcessing()
        {
            // Empty implementation
        }
    }
}