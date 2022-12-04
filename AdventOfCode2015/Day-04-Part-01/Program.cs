var input = "bgvyzdsv";

var candidateHashed = string.Empty;
var numberToAppend = 0;

using var md5 = System.Security.Cryptography.MD5.Create();
do
{
    numberToAppend++;

    var toHash = $"{input}{numberToAppend}";

    var inputBytes = System.Text.Encoding.ASCII.GetBytes(toHash);
    var hashBytes = md5.ComputeHash(inputBytes);
    candidateHashed = Convert.ToHexString(hashBytes);
} while (!candidateHashed.StartsWith("00000"));

Console.WriteLine($"Day 4 - Part 1: {numberToAppend}");
