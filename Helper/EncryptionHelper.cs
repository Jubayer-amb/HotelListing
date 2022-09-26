using BCryptNet = BCrypt.Net.BCrypt;
using BCryptHash = BCrypt.Net.HashType;
namespace HotelListing.Helper;

public class EncryptionHelper
{
    public static string GenerateHash(string password)
    {
        return BCryptNet.EnhancedHashPassword(password, BCryptHash.SHA512);
    }
    public static bool VerifyHash(string password, string hashedPassword)
    {
        return BCryptNet.EnhancedVerify(password, hashedPassword, hashType: BCryptHash.SHA512);
    }
}