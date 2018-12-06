using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_03_Part_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputClaims = File.ReadAllLines(@"input.txt");

            var claims = ParseClaims(inputClaims);
            
            var claimCombinationsToCheck = GetClaimCombinations(claims);

            int totalIntersect = GetTotalIntersect(claimCombinationsToCheck);

            Console.WriteLine("Advent of Code - Day 3 - Part 1: {0}", totalIntersect);
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

        public static int GetTotalIntersect(HashSet<(Claim primaryClaim, Claim secondaryClaim)> claimCombinationsToCheck)
        {
            var isInTwoOrMoreClaims = new HashSet<(int x, int y)>();

            foreach (var claimTuple in claimCombinationsToCheck) {
                var primaryClaim = claimTuple.Item1;
                var secondaryClaim = claimTuple.Item2;

                if (!ClaimsCollide(primaryClaim, secondaryClaim))
                {
                    continue;
                }

                var primaryClaimCoordinates = GetClaimCoordinates(primaryClaim);
                var secondaryClaimCoordinates = GetClaimCoordinates(secondaryClaim);

                foreach (var primaryCoord in primaryClaimCoordinates)
                {
                    if (secondaryClaimCoordinates.Contains(primaryCoord))
                    {
                        isInTwoOrMoreClaims.Add(primaryCoord);
                    }
                }
            }

            return isInTwoOrMoreClaims.Count;

            bool ClaimsCollide(Claim claimOne, Claim claimTwo)
            {
                return claimOne.DistanceFromLeft < claimTwo.DistanceFromLeft + claimTwo.Width &&
                       claimOne.DistanceFromLeft + claimOne.Width > claimTwo.DistanceFromLeft &&
                       claimOne.DistanceFromTop < claimTwo.DistanceFromTop + claimTwo.Height &&
                       claimOne.Height + claimOne.DistanceFromTop > claimTwo.DistanceFromTop;
            }
        }

        public static HashSet<(Claim primaryClaim, Claim secondaryClaim)> GetClaimCombinations(Claim[] claims)
        {
            var alreadyCounted = new HashSet<(int claimIdOne, int claimIdTwo)>();
            var claimsToCheck = new HashSet<(Claim primaryClaim, Claim secondaryClaim)>();

            foreach (var primaryClaim in claims) 
            {
                foreach (var secondaryClaim in claims) 
                {
                    var claimPairIdentification = GetClaimTuple(primaryClaim, secondaryClaim);

                    if (primaryClaim.Id == secondaryClaim.Id)
                    {
                        continue;
                    }

                    alreadyCounted.Add(claimPairIdentification);
                    claimsToCheck.Add((primaryClaim, secondaryClaim));
                }
            }

            return claimsToCheck;

            (int claimIdOne, int claimIdTwo) GetClaimTuple(Claim claimOne, Claim claimTwo)
            {
                if (claimOne.Id < claimTwo.Id)
                {
                    return (claimOne.Id, claimTwo.Id);
                }

                return (claimTwo.Id, claimOne.Id);
            }
        }

        public static HashSet<(int x, int y)> GetClaimCoordinates(Claim claim)
        {
            var coordinates = new HashSet<(int x, int)>();

            for (var i = claim.DistanceFromLeft + 1; i < claim.DistanceFromLeft + claim.Width + 1; i++)
            {
                for (var j = claim.DistanceFromTop + 1; j < claim.DistanceFromTop + claim.Height + 1; j++)
                {
                    coordinates.Add((i, j));
                }
            }

            return coordinates;
        }
    }
}
