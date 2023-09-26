namespace SubtleBlazor
{
    public class JWK
    {
        public string alg { get; set; }

        public bool ext { get; set; }

        public string k { get; set; }

        public string[] key_ops { get; set; }

        public string kty { get; set; }

        /// <summary>
        /// concat alg + ext + k + key_ops + kty and compute SHA-256 hash.
        /// </summary>
        public string fingerprint { get; set; }
    }
}