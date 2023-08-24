# SubtleBlazor

## Disclaimer

SubtleBlazor is in development. The API may change without warning and I may find security issues that necessitate changes. Use at your own risk.

## What is SubtleBlazor?

SubtleBlazor is a Blazor wrapper around a subset of the features of the [Subtle Crypto](https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto) javascript API.

It currently supports a limited number of scenarios:

- Blazor WebAssembly in .Net 6+
- Key derivation: PBKDF2 with SHA-256
- Encryption: AES-GCM mode
- Decryption: AES-GCM mode

If you'd like additional scenarios supported, feel free to submit a PR.

## Setup

1. Install the SubtleBlazor nuget package

2. Modify your startup file to include the following

```csharp
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        ...
        builder.Services.AddSubtleBlazor();
        ...
        await builder.Build().RunAsync();
    }
```

3. Inject SubtleBlazorService into your pages

```csharp
    [Inject]
    public SubtleBlazorService _crypto { get; set; }
```

See the SubtleBlazor.ExampleApp project for more.

## Usage

### Derive a key

```csharp
    JWK result = await _crypto.DeriveKeyAsync(_pass, "1234567890");
    string jwk = JsonSerializer.Serialize(result);
```

Derive a key from the given derivation material (a password, whatever) and some salt.

Returns a JWK object, which is a .Net wrapper around a limited feature set of the JWK standard.

### Encrypt

```csharp
    EncryptionResult result = await _crypto.EncryptAsync(jwk, _plaintext);
    _ciphertext = JsonSerializer.Serialize(result);
```

Accepts a JWK object and the string plaintext. Returns an EncryptionResult, which includes the ciphertext and randomly-generated IV.

### Decrypt

```csharp
    EncryptionResult ciphertext = JsonSerializer.Deserialize<EncryptionResult>(_ciphertext);
    DecryptionResult decryptionResult = await _crypto.DecryptAsync(jwk, ciphertext.IV, ciphertext.CipherText);
```

Accepts a JWK object, an IV, and ciphertext. Returns a DecryptionResult, which includes the resulting plaintext.