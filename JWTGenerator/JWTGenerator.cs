using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;


public class PemKeyReader
{
    public static string ReadKeyFromPemFile(string pemFile){
        if (!File.Exists(pemFile)){
            throw new FileNotFoundException($"The file {pemFile} does not exists.");
        }
        var pem = File.ReadAllText(pemFile);
        var rsa = RSA.Create();
        rsa.ImportFromPem(pem.ToCharArray());
        return Convert.ToBase64String(rsa.ExportRSAPrivateKey());
    }
}
public class JwtGenerator
{
  // use a key from a secure source
    private const string Issuer = "your-app-issuer";
    private const string Audience = "your-app-audience";

    public static string GenerateToken(string username)
    {
        // To generate private_key.pem user 
        // openssl genpkey -algorithm RSA -out private_key.pem -pkeyopt rsa_keygen_bits:2048
        string SecretKey = PemKeyReader.ReadKeyFromPemFile("private_key.pem"); 
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("username", "Mohan")
        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

