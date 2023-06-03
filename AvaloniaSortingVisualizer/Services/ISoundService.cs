namespace AvaloniaSortingVisualizer.Services
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Plays musical notes to help with the visualizations.
    /// </summary>
    public interface ISoundService
    {
        /// <summary>
        /// Calculates the note number of a value relative to another value.
        /// </summary>
        /// <param name="value">The value to get the note number.</param>
        /// <param name="maxValue">A value to compare, that represents the highest note.</param>
        /// <returns>A <see cref="MidiNotes"/>representing the note number.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Any of the arguments is negative.</exception>
        /// <example>
        /// In an array with all natural numbers, 1 is the
        /// lowest value, and that equals <see cref="MidiNotes.Note0"/>, while the
        /// highest number is 500, which equals <see cref="MidiNotes.Note127"/>.
        /// A value between those two gets calculated proportionally.
        /// <code>
        /// CalculateNote(1, 500); // returns MidiNotes.Note0 because it is the minimum value
        /// CalculateNote(500, 500); // returns MidiNotes.Note127 because it is the maximum value
        /// CalculateNote(250, 500); // returns MidiNotes.Note63 beacause it the me mid value
        /// </code>
        /// </example>
        public MidiNotes CalculateNote(double value, int maxValue);

        /// <summary>
        /// Plays a musical note asyncrouslly.
        /// </summary>
        /// <param name="note">Note to play.</param>
        /// <param name="duration">Duration of the note in miliseconds.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public Task PlayNoteAsync(MidiNotes note, int duration);

        /// <summary>
        /// Plays two musical notes asyncrouslly.
        /// </summary>
        /// <param name="note1">First note to play.</param>
        /// <param name="note2">Second note to play.</param>
        /// <param name="duration">Duration of the note in miliseconds.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public Task PlayNotesAsync(MidiNotes note1, MidiNotes note2, int duration);

        /// <summary>
        /// Plays three musical notes asyncrouslly.
        /// </summary>
        /// <param name="note1">First note to play.</param>
        /// <param name="note2">Second note to play.</param>
        /// <param name="note3">Second note to play.</param>
        /// <param name="duration">Duration of the note in miliseconds.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public Task PlayNotesAsync(MidiNotes note1, MidiNotes note2, MidiNotes note3, int duration);
    }
}
