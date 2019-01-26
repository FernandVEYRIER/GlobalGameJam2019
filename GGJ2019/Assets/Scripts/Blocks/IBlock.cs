namespace Assets.Scripts.Blocks
{
    /// <summary>
    /// Interface for any block.
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// Width of the block.
        /// </summary>
        /// <returns></returns>
        float GetWidth();

        /// <summary>
        /// Height of the block.
        /// </summary>
        /// <returns></returns>
        float GetHeight();
    }
}