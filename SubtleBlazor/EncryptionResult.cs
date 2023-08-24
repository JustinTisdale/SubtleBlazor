namespace SubtleBlazor
{
    public class EncryptionResult : ResultBase
    {
        public string CipherText { get; set; }

        public string IV { get; set; }
    }
}