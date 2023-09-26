namespace SubtleBlazor
{
    public static class Constants
    {
        public static readonly string JSLibrary_FilePath = "js/subtle-blazor.js";

        public static readonly string JSMethod_Encrypt = "subtleBlazorEncrypt";
        public static readonly string JSMethod_Decrypt = "subtleBlazorDecrypt";
        public static readonly string JSMethod_DeriveKey = "subtleBlazorDeriveKey";
        public static readonly string JSMethod_ComputeHash = "subtleBlazorDigest";
        public static readonly string JSMethod_GenerateBytes = "subtleBlazorGenerateBytes";
    }
}