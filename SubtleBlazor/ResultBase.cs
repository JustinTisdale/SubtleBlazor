namespace SubtleBlazor
{
    public abstract class ResultBase
    {
        public bool Succeeded { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}