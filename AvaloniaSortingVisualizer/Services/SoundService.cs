namespace AvaloniaSortingVisualizer.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using NFluidsynth;
    using SDL2;

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
        private static readonly string SoundfontPath = Path.Combine(
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
            this.settings = this.ConfigureSettings();
            this.synth = this.ConigureSynth();
            this.audioDriver = new AudioDriver(this.settings, this.synth);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SoundService"/> class.
        /// Finalizes the <see cref="SoundService"/> instance by disposing resources.
        /// </summary>
        ~SoundService()
        {
            this.audioDriver.Dispose();
            this.synth.Dispose();
            this.settings.Dispose();
            SDL.SDL_QuitSubSystem(SDL.SDL_INIT_AUDIO);
        }

        public MidiNotes CalculateNote(double value, int maxValue) =>
            (MidiNotes)((value * (HighestNote - LowestNote) / maxValue) + LowestNote);

        public async Task PlayNoteAsync(MidiNotes note, int duration)
        {
            this.synth.NoteOn(MidiChannel1, (int)note, MidiVelocity);
            await Task.Delay(duration);
            this.synth.NoteOff(MidiChannel1, (int)note);
        }

        public async Task PlayNotesAsync(MidiNotes note1, MidiNotes note2, int duration)
        {
            this.synth.NoteOn(MidiChannel1, (int)note1, MidiVelocity);
            this.synth.NoteOn(MidiChannel2, (int)note2, MidiVelocity);
            await Task.Delay(duration);
            this.synth.NoteOff(MidiChannel1, (int)note1);
            this.synth.NoteOff(MidiChannel2, (int)note2);
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
            Synth synth = new Synth(this.settings);

            synth.LoadSoundFont(SoundfontPath, false);

            synth.SoundFontSelect(MidiChannel1, 0);
            synth.ProgramChange(MidiChannel1, MidiInstrument);

            synth.SoundFontSelect(MidiChannel2, 0);
            synth.ProgramChange(MidiChannel2, MidiInstrument);

            synth.SoundFontSelect(MidiChannel3, 0);
            synth.ProgramChange(MidiChannel3, MidiInstrument);

            return synth;
        }

        public async Task PlayNotesAsync(
            MidiNotes note1,
            MidiNotes note2,
            MidiNotes note3,
            int duration
        )
        {
            this.synth.NoteOn(MidiChannel1, (int)note1, MidiVelocity);
            this.synth.NoteOn(MidiChannel2, (int)note2, MidiVelocity);
            this.synth.NoteOn(MidiChannel3, (int)note3, MidiVelocity);
            await Task.Delay(duration);
            this.synth.NoteOff(MidiChannel1, (int)note1);
            this.synth.NoteOff(MidiChannel2, (int)note2);
            this.synth.NoteOff(MidiChannel3, (int)note3);
        }
    }
}
