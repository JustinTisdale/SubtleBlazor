using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace SubtleBlazor.Tests
{
    public class EmbeddedResourceTests
    {
        [Fact]
        public async Task FetchFile_ValidPath_Works()
        {
            var test = await EmbeddedResource.FetchAsStringAsync("js/subtle-blazor.js");
            Assert.True(!string.IsNullOrWhiteSpace(test));
        }

        [Fact]
        public async Task FetchFile_InvalidPath_ThrowsExpectedException()
        {

            await Assert.ThrowsAsync<FileNotFoundException>(async () =>
            {
                var test = await EmbeddedResource.FetchAsStringAsync("js/subtle-blazor-does-not-exist.js");
            });
        }
    }

    public class DotNetCompatibilityTests
    {
        [Fact]
        public async Task YourDotNetCode_ValidCiphertextAndKey_Decrypts()
        {
            // .Net code to work with data encrypted by SubtleBlazor is not provided.
            // This is an example of how you might accomplish that, but it is not intended to be a complete solution.
            // Here be dragons.

            // Key material: 1234
            // Salt: 1234567890
            JWK jwk = new JWK()
            {
                alg = "A256GCM",
                ext = true,
                k = "e-M9BV6bZZyZW2svJnZeNcFzwNKxI7WzpQSfbPKbRTg",
                key_ops = new string[] { "encrypt", "decrypt" },
                kty = "oct"
            };

            // Plaintext: Hello, world!
            var cipherText = "8KuprxfOP8RG9QrpEA6UyW9b1jO1QWcPMExorI8=";
            var iv = "05qAjK/t1zixbr6A";

            // TODO: Decrypt the ciphertext using the given key and IV, using AES-GCM

            // Base64Url decode the key
            byte[] keyBytes = Base64UrlEncoder.DecodeBytes(jwk.k);

            // Base64 decode the ciphertext and IV
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            byte[] ivBytes = Convert.FromBase64String(iv);
            byte[] tagBytes = cipherTextBytes.Skip(cipherTextBytes.Length - 16).Take(16).ToArray();
            byte[] untaggedCipherTextBytes = cipherTextBytes.SkipLast(16).ToArray();

            // Create a buffer for the plaintext
            byte[] plainTextBytes = new byte[untaggedCipherTextBytes.Length];

            // Decrypt
            using var aes = new AesGcm(keyBytes);
            aes.Decrypt(ivBytes, untaggedCipherTextBytes, tagBytes, plainTextBytes);

            // Convert plain bytes back into string
            string plainText = Encoding.UTF8.GetString(plainTextBytes);

            // Print out the plaintext
            Console.WriteLine(plainText);
            Assert.Equal("Hello, world!", plainText);
        }
    }
}