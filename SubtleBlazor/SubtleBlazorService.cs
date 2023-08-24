using Microsoft.JSInterop;

namespace SubtleBlazor
{
    /// <summary>
    /// Simple IJSRuntime wrapper around the Subtle Crypto javascript API.
    /// </summary>
    public class SubtleBlazorService
    {
        private readonly IJSRuntime _jsRuntime;
        private bool _libraryInjected = false;

        public SubtleBlazorService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Adds the subtle-blazor.js library to the DOM.
        /// </summary>
        public async Task InjectLibraryJSAsync()
        {
            // Try to ensure we're only injecting the library once in the lifetime of this object, not on every call.
            if(_libraryInjected)
                return;

            /*
             * The library is being injected this way to provide some convenience for the developer while also being easily auditable. 
             * The subtle-blazor.js file is not obfuscated or minified.
             */

            string js = await EmbeddedResource.FetchAsStringAsync(Constants.JSLibrary_FilePath);
            await _jsRuntime.InvokeVoidAsync("eval", js);
        }

        /// <summary>
        /// Encrypt the plaintext using the given key.
        /// </summary>
        /// <param name="key">A JWK representing the key to be used for decryption.</param>
        /// <param name="plaintext">The text to be encrypted</param>
        /// <returns><see cref="EncryptionResult"/></returns>
        public async Task<EncryptionResult> EncryptAsync(JWK key, string plaintext)
        {
            await InjectLibraryJSAsync();
            var result = await _jsRuntime.InvokeAsync<EncryptionResult>(Constants.JSMethod_Encrypt, key, plaintext);
            return result;
        }

        /// <summary>
        /// Decrypt the ciphertext using the given key and IV.
        /// </summary>
        /// <param name="key">A JWK representing the key to be used for decryption.</param>
        /// <param name="iv">Base64-encoded IV</param>
        /// <param name="ciphertext">Base64-encoded ciphertext</param>
        /// <returns><see cref="DecryptionResult"/></returns>
        public async Task<DecryptionResult> DecryptAsync(JWK key, string iv, string ciphertext)
        {
            DecryptionResult result = new DecryptionResult();

            try
            {
                await InjectLibraryJSAsync();
                string jsResult = await _jsRuntime.InvokeAsync<string>(Constants.JSMethod_Decrypt, key, iv, ciphertext);
                result.PlainText = jsResult;
            }
            catch (Exception err)
            {
                result.Succeeded = false;
                result.ErrorMessage = err.Message;
            }
            
            return result;
        }

        /// <summary>
        /// Derives a key from the given key material and salt.
        /// </summary>
        /// <param name="keyMaterial">The key material to derive from (a password, etc.)</param>
        /// <param name="salt">Salt to add to the key material</param>
        /// <returns>The generated key in JWK format</returns>
        /// <seealso cref="https://datatracker.ietf.org/doc/html/rfc7517"/>
        public async Task<JWK> DeriveKeyAsync(string keyMaterial, string salt)
        {
            try
            {
                await InjectLibraryJSAsync();
                var result = await _jsRuntime.InvokeAsync<JWK>(Constants.JSMethod_DeriveKey, keyMaterial, salt);
                return result;
            }
            catch (Exception err)
            {
                return null;
            }
        }
    }
}