namespace SubtleBlazor
{
    public class JWK
    {
        public string alg { get; set; }

        public bool ext { get; set; }

        public string k { get; set; }

        public string[] key_ops { get; set; }

        public string kty { get; set; }
    }
}