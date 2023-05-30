using System;
using System.IO;
using System.Threading.Tasks;
using NFluidsynth;
using SDL2;
using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.Services
{
    /// <summary>
    /// Service for playing MIDI notes using FluidSynth and SDL2.
    /// </summary>
    public class SoundService : ISoundService
    {
        private const int LowestNote = (int)MidiNotes.Note0;
        private const int HighestNote = (int)MidiNotes.Note127;
        private const int MidiChannel1 = 1;
        private const int MidiChannel2 = 2;
        private const int MidiChannel3 = 3;
        private const int MidiInstrument = 1;
        private const int MidiVelocity = 100;
        private static readonly string soundfontPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Soundfonts",
            "CalculatorSF-balanced2.sf2"
        );
        private readonly Settings settings;
        private readonly Synth synth;
        private readonly AudioDriver audioDriver;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundService"/> class.
        /// </summary>
        public SoundService()
        {
            SDL.SDL_InitSubSystem(SDL.SDL_INIT_AUDIO);
            settings = ConfigureSettings();
            synth = ConigureSynth();
            audioDriver = new AudioDriver(settings, synth);
        }

        /// <summary>
        /// Creates and configures an instance of <see cref="Settings"/>.
        /// </summary>
        /// <returns>The created object.</returns>
        private Settings ConfigureSettings()
        {
            Settings settings = new Settings();

            settings[ConfigurationKeys.AudioDriver].StringValue = "sdl2";
            settings[ConfigurationKeys.SynthAudioChannels].IntValue = 3;
            settings[ConfigurationKeys.SynthChorusActive].IntValue = 0;
            settings[ConfigurationKeys.SynthMinNoteLength].IntValue = 0;
            settings[ConfigurationKeys.SynthReverbActive].IntValue = 0;

            return settings;
        }

        /// <summary>
        /// Creates and configures an instance of <see cref="Synth"/>.
        /// </summary>
        /// <returns>The created object.</returns>
        private Synth ConigureSynth()
        {
            Synth synth = new Synth(settings);

            synth.LoadSoundFont(soundfontPath, false);

            synth.SoundFontSelect(MidiChannel1, 0);
            synth.ProgramChange(MidiChannel1, MidiInstrument);

            synth.SoundFontSelect(MidiChannel2, 0);
            synth.ProgramChange(MidiChannel2, MidiInstrument);

            synth.SoundFontSelect(MidiChannel3, 0);
            synth.ProgramChange(MidiChannel3, MidiInstrument);

            return synth;
        }

        public MidiNotes CalculateNote(double value, int maxValue) =>
            (MidiNotes)(value * (HighestNote - LowestNote) / maxValue + LowestNote);

        public async Task PlayNoteAsync(MidiNotes note, int duration)
        {
            synth.NoteOn(MidiChannel1, (int)note, MidiVelocity);
            await Task.Delay(duration);
            synth.NoteOff(MidiChannel1, (int)note);
        }

        public async Task PlayNotesAsync(MidiNotes note1, MidiNotes note2, int duration)
        {
            synth.NoteOn(MidiChannel1, (int)note1, MidiVelocity);
            synth.NoteOn(MidiChannel2, (int)note2, MidiVelocity);
            await Task.Delay(duration);
            synth.NoteOff(MidiChannel1, (int)note1);
            synth.NoteOff(MidiChannel2, (int)note2);
        }

        public async Task PlayNotesAsync(
            MidiNotes note1,
            MidiNotes note2,
            MidiNotes note3,
            int duration
        )
        {
            synth.NoteOn(MidiChannel1, (int)note1, MidiVelocity);
            synth.NoteOn(MidiChannel2, (int)note2, MidiVelocity);
            synth.NoteOn(MidiChannel3, (int)note3, MidiVelocity);
            await Task.Delay(duration);
            synth.NoteOff(MidiChannel1, (int)note1);
            synth.NoteOff(MidiChannel2, (int)note2);
            synth.NoteOff(MidiChannel3, (int)note3);
        }

        /// <summary>
        /// Finalizes the <see cref="SoundService"/> instance by disposing resources.
        /// </summary>
        ~SoundService()
        {
            audioDriver.Dispose();
            synth.Dispose();
            settings.Dispose();
            SDL.SDL_QuitSubSystem(SDL.SDL_INIT_AUDIO);
        }
    }
}
