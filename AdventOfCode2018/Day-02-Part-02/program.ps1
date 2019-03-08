Param (
    [Parameter(Mandatory=$True)][String]$Filepath
)

Set-StrictMode -Version Latest;

Function Read-IdsFromFile($InputFilepath) {
    Return [String[]](Get-Content -Path $InputFilepath);
}

Function Get-IdWhichOnlyDiffersByOneCharacter($IdsToProcess) {
    $IdSize = $IdsToProcess[0].Length;

    $Result = "";

    ForEach ($PrimaryId In $IdsToProcess) {
        ForEach ($SecondaryId In $IdsToProcess) {
            $CountOfCharactersWhichAreTheSame = 0;

            $CandidateString = "";
            $PrimaryIdSplit = [Char[]]$PrimaryId.ToCharArray();
            $SecondaryIdSplit = [Char[]]$SecondaryId.ToCharArray();

            For ($I = 0; $I -Lt $IdSize; $I++) {
                If ($PrimaryIdSplit[$I] -Eq $SecondaryIdSplit[$I]) {
                    $CandidateString = -Join($CandidateString, $PrimaryIdSplit[$I]); 
                    $CountOfCharactersWhichAreTheSame += 1;
                }
            }

            If ($CountOfCharactersWhichAreTheSame -Eq ($IdSize - 1)) {
                $Result = $CandidateString;
                Break;
            }
        }

        If ($Result.Length -Gt 0) {
            Break;
        }
    }

    Return $Result;
}

$InputIds = Read-IdsFromFile($Filepath);
$Checksum = Get-IdWhichOnlyDiffersByOneCharacter($InputIds);
Write-Host ("Advent of Code - Day 2 - Part 2: {0}" -F $Checksum);