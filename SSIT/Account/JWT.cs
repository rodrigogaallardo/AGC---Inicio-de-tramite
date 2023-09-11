using Newtonsoft.Json;
using SSIT.Account;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

public class JWT
{
    private readonly string _SecretKey = ConfigurationManager.AppSettings["keyPrivada"];


    private string SecretKey { get; set; }

    private string token { get; set; }

    private string decodedPayload { get; set; }

    public JWT(string token)
    {
        this.token = token;
        SecretKey = _SecretKey;
    }

    public bool ValidateJwt()
    {
        string[] jwtComponents = this.token.Split('.');

        if (jwtComponents.Length != 3) throw new Exception("El token no tiene el formato correcto");

        string encodedHeader = jwtComponents[0];
        string encodedPayload = jwtComponents[1];
        string signature = jwtComponents[2];

        string decodedHeader = Base64UrlDecode(encodedHeader);
        decodedPayload = Base64UrlDecode(encodedPayload);

        dynamic headerJson = Newtonsoft.Json.JsonConvert.DeserializeObject(decodedHeader);
        string algorithm = headerJson.alg;

        byte[] secretBytes = Encoding.UTF8.GetBytes(SecretKey);
        byte[] payloadBytes = Encoding.UTF8.GetBytes(encodedHeader + "." + encodedPayload);

        if (algorithm == "HS512") return ValidateSignatureHS512(secretBytes, payloadBytes, signature);
        else if (algorithm == "HS256") return ValidateSignatureHS256(secretBytes, payloadBytes, signature);

        throw new NotImplementedException("El algoritmo solicitado no está implementado");
    }

    public DatosMiBA GetTokenPayload()
    {
        try
        {
            DatosMiBA payload = JsonConvert.DeserializeObject<DatosMiBA>(this.decodedPayload);
            return payload;
        }
        catch (Exception)
        {
            throw new Exception("El payload no es el correcto");
        }
    }

    private bool ValidateSignatureHS512(byte[] secretBytes, byte[] payloadBytes, string signature)
    {
        using (HMACSHA512 hmac = new HMACSHA512(secretBytes))
        {
            byte[] calculatedSignature = hmac.ComputeHash(payloadBytes);
            string calculatedSignatureBase64 = Base64UrlEncode(calculatedSignature);

            return signature == calculatedSignatureBase64;
        }
    }

    private bool ValidateSignatureHS256(byte[] secretBytes, byte[] payloadBytes, string signature)
    {
        using (HMACSHA256 hmac = new HMACSHA256(secretBytes))
        {
            byte[] calculatedSignature = hmac.ComputeHash(payloadBytes);
            string calculatedSignatureBase64 = Base64UrlEncode(calculatedSignature);

            return signature == calculatedSignatureBase64;
        }
    }

    private string Base64UrlDecode(string input)
    {
        string base64 = input.Replace('-', '+').Replace('_', '/');
        while (base64.Length % 4 != 0)
        {
            base64 += "=";
        }

        byte[] bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }

    private string Base64UrlEncode(byte[] input)
    {
        string base64 = Convert.ToBase64String(input);
        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }
}