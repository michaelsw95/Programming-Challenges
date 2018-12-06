using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_03_Part_02
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputClaims = File.ReadAllLines(@"input.txt");

            var claims = ParseClaims(inputClaims);
            Array.Clear(inputClaims, 0, inputClaims.Length);

            var claimWithNoIntersect = GetClaimWithNoIntersect(claims);

            Console.WriteLine("Advent of Code - Day 3 - Part 2: {0}", claimWithNoIntersect.Id);
        }

        public static Claim[] ParseClaims(string[] inputClaims)
        {
            var claims = new Claim[inputClaims.Length];

            const int idIndex = 0;
            const int distanceFromLeftIndex = 1;
            const int distanceFromTopIndex = 2;
            const int widthIndex = 3;
            const int heightIndex = 4;

            for (var i = 0; i < inputClaims.Length; i++)
            {
                var claim = Regex.Replace(inputClaims[i], @"(\s+|#|@|:|,|x)", " ")
                    .Split(' ')
                    .Where(o => o != string.Empty)
                    .Select(o => int.Parse(o))
                    .ToArray();

                var newClaim = new Claim(claim[idIndex], claim[distanceFromLeftIndex], claim[distanceFromTopIndex], claim[widthIndex], claim[heightIndex]);

                claims[i] = newClaim;
            }

            return claims;
        }

        public static Claim GetClaimWithNoIntersect(Claim[] claims)
        {
            var doesNotCollide = claims
                .Where(o => !claims.Any(p => o.Id != p.Id && ClaimsCollide(o, p)))
                .SingleOrDefault();

            return doesNotCollide;

            bool ClaimsCollide(Claim claimOne, Claim claimTwo)
            {
                return claimOne.DistanceFromLeft < claimTwo.DistanceFromLeft + claimTwo.Width &&
                       claimOne.DistanceFromLeft + claimOne.Width > claimTwo.DistanceFromLeft &&
                       claimOne.DistanceFromTop < claimTwo.DistanceFromTop + claimTwo.Height &&
                       claimOne.Height + claimOne.DistanceFromTop > claimTwo.DistanceFromTop;
            }
        }
    }
}
