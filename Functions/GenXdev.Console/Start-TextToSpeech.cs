// ################################################################################
// Part of PowerShell module : GenXdev.Console
// Original cmdlet filename  : Start-TextToSpeech.cs
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



using System.Globalization;
using System.Management.Automation;

namespace GenXdev.Console
{
    /// <summary>
    /// <para type="synopsis">
    /// Converts text to speech using the Windows Speech API.
    /// </para>
    ///
    /// <para type="description">
    /// Uses the Windows Speech API to convert text to speech. This cmdlet provides
    /// flexible text-to-speech capabilities with support for different voices, locales,
    /// and synchronous/asynchronous playback options. It can handle both single strings
    /// and arrays of text, with error handling for speech synthesis failures.
    /// </para>
    ///
    /// <para type="description">
    /// PARAMETERS
    /// </para>
    ///
    /// <para type="description">
    /// -Lines &lt;String[]&gt;<br/>
    /// The text to be spoken. Accepts single string or array of strings. Each line will
    /// be processed sequentially for speech synthesis.<br/>
    /// - <b>Position</b>: 0<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Locale &lt;String&gt;<br/>
    /// The language locale ID to use (e.g., 'en-US', 'es-ES'). When specified, the
    /// function will attempt to use a voice matching this locale.<br/>
    /// - <b>Default</b>: null<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -VoiceName &lt;String&gt;<br/>
    /// The specific voice name to use for speech synthesis. If specified, the function
    /// will attempt to find and use a matching voice from installed voices.<br/>
    /// - <b>Default</b>: null<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -PassThru &lt;SwitchParameter&gt;<br/>
    /// When specified, outputs the text being spoken to the pipeline, allowing for
    /// text processing while speaking.<br/>
    /// - <b>Aliases</b>: pt<br/>
    /// </para>
    ///
    /// <para type="description">
    /// -Wait &lt;SwitchParameter&gt;<br/>
    /// When specified, waits for speech to complete before continuing execution.
    /// Useful for synchronous operations.<br/>
    /// </para>
    ///
    /// <example>
    /// <para>Example 1: Speak text synchronously with specific locale</para>
    /// <para>Converts the specified text to speech using the en-US locale and waits for completion.</para>
    /// <code>
    /// Start-TextToSpeech -Lines "Hello World" -Locale "en-US" -Wait
    /// </code>
    /// </example>
    ///
    /// <example>
    /// <para>Example 2: Use pipeline input</para>
    /// <para>Pipes text to the cmdlet for speech synthesis.</para>
    /// <code>
    /// "Hello World" | Start-TextToSpeech
    /// </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TextToSpeech")]
    [Alias("say")]
    [OutputType(typeof(string))]
    public class StartTextToSpeechCommand : PSGenXdevCmdlet
    {
        /// <summary>
        /// The text to be spoken. Accepts single string or array of strings. Each line will
        /// be processed sequentially for speech synthesis.
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromRemainingArguments = false,
            ParameterSetName = "strings",
            HelpMessage = "Text to be spoken")]
        [AllowEmptyString]
        [AllowEmptyCollection]
        public string[] Lines { get; set; }

        /// <summary>
        /// The language locale ID to use (e.g., 'en-US', 'es-ES'). When specified, the
        /// function will attempt to use a voice matching this locale.
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "The language locale id to use, e.g. 'en-US'")]
        public string Locale { get; set; }

        /// <summary>
        /// The specific voice name to use for speech synthesis. If specified, the function
        /// will attempt to find and use a matching voice from installed voices.
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Name of the voice to use for speech")]
        public string VoiceName { get; set; }

        /// <summary>
        /// When specified, outputs the text being spoken to the pipeline, allowing for
        /// text processing while speaking.
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Output the text being spoken to the pipeline")]
        [Alias("pt")]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// When specified, waits for speech to complete before continuing execution.
        /// Useful for synchronous operations.
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Wait for speech to complete before continuing")]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// Begin processing - initialization logic
        /// </summary>
        protected override void BeginProcessing()
        {
            WriteVerbose($"Initializing text-to-speech with Locale: {Locale}, Voice: {VoiceName}");
        }

        /// <summary>
        /// Process record - main cmdlet logic
        /// </summary>
        protected override void ProcessRecord()
        {
            // Iterate through each line of text for processing
            foreach (var line in Lines ?? new string[0])
            {
                // Skip null or empty strings
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string text = line;

                try
                {
                    // Output text to pipeline if passthru is enabled
                    if (PassThru.ToBool())
                    {
                        WriteObject(text);
                    }

                    // Normalize text by removing newlines and tabs
                    text = text.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
                    WriteVerbose($"Processing text: {text}");

                    // Handle case when no locale is specified
                    if (string.IsNullOrWhiteSpace(Locale))
                    {
                        if (string.IsNullOrWhiteSpace(VoiceName))
                        {
                            // Use default voice with wait option
                            if (Wait.ToBool())
                            {
                                WriteVerbose("Speaking synchronously with default voice");

                                if (ShouldProcess(text, "Speak"))
                                {
                                    GenXdev.Helpers.Misc.Speech.Speak(text);
                                }
                                continue;
                            }

                            WriteVerbose("Speaking asynchronously with default voice");

                            if (ShouldProcess(text, "Speak asynchronously"))
                            {
                                GenXdev.Helpers.Misc.Speech.SpeakAsync(text);
                            }
                            continue;
                        }

                        // Attempt to use specified voice without locale
                        try
                        {
                            var voices = GenXdev.Helpers.Misc.SpeechCustomized.GetInstalledVoices();
                            var voice = voices
                                .Where(v => string.IsNullOrWhiteSpace(VoiceName) ||
                                           v.VoiceInfo.Name.Contains($" {VoiceName} "))
                                .Select(v => v.VoiceInfo.Name)
                                .FirstOrDefault();

                            if (!string.IsNullOrEmpty(voice) && ShouldProcess($"Voice: {voice}", "Select voice"))
                            {
                                GenXdev.Helpers.Misc.SpeechCustomized.SelectVoice(voice);
                            }
                        }
                        catch
                        {
                            WriteWarning($"Could not set voice: {VoiceName}");
                        }

                        if (ShouldProcess(text, "Speak with selected voice"))
                        {
                            GenXdev.Helpers.Misc.SpeechCustomized.Speak(text);
                        }
                        continue;
                    }

                    // Attempt to use voice with specified locale
                    try
                    {
                        var culture = new CultureInfo(Locale);
                        var voices = GenXdev.Helpers.Misc.SpeechCustomized.GetInstalledVoices(culture);
                        var voice = voices
                            .Where(v => string.IsNullOrWhiteSpace(VoiceName) ||
                                       v.VoiceInfo.Name.Contains($" {VoiceName} "))
                            .Select(v => v.VoiceInfo.Name)
                            .FirstOrDefault();

                        if (!string.IsNullOrEmpty(voice) && ShouldProcess($"Voice: {voice}", "Select voice"))
                        {
                            GenXdev.Helpers.Misc.SpeechCustomized.SelectVoice(voice);
                        }
                    }
                    catch
                    {
                        WriteWarning($"Could not set voice for locale: {Locale}");
                    }

                    if (ShouldProcess(text, "Speak with locale-specific voice"))
                    {
                        GenXdev.Helpers.Misc.SpeechCustomized.Speak(text);
                    }
                }
                catch (Exception ex)
                {
                    WriteError(new ErrorRecord(ex, "SpeechSynthesisFailed", ErrorCategory.InvalidOperation, text));

                    if (ShouldProcess(text, "Speak with default voice (fallback)"))
                    {
                        GenXdev.Helpers.Misc.Speech.Speak(text);
                    }
                }
            }
        }

        /// <summary>
        /// End processing - cleanup logic
        /// </summary>
        protected override void EndProcessing()
        {
            // No cleanup needed
        }
    }
}