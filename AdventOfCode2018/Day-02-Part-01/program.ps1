Param (
    [Parameter(Mandatory=$True)][String]$Filepath
)

Set-StrictMode -Version Latest;

Function Read-IdsFromFile($InputFilepath) {
    Return [String[]](Get-Content -Path $InputFilepath);
}

Function Get-CheckSumOfIds($IdsToProcess) {
    $CountOfIdsWithALetterThatAppearsTwice = 0;
    $CountOfIdsWithALetterThatAppearsThreeTimes = 0;

    ForEach ($Id In $IdsToProcess) {
        $CharactersWhichHaveBeenChecked = New-Object System.Collections.Generic.HashSet[Char]
        $HaveFoundALetterThatAppearsTwice = $False;
        $HaveFoundALetterThatAppearsThreeTimes = $False;

        ForEach ($Char In [Char[]]$Id) {
            If ($CharactersWhichHaveBeenChecked.Contains($Char)) {
                Continue;
            }

            $CharacterCount = ($Id | Select-String -Pattern $Char -AllMatches).Matches.Count;

            If (($CharacterCount -Eq 2) -And $HaveFoundALetterThatAppearsTwice -Eq $False) {
                $CountOfIdsWithALetterThatAppearsTwice += 1;
                $HaveFoundALetterThatAppearsTwice = $True;
            } ElseIf ($CharacterCount -Eq 3 -And $HaveFoundALetterThatAppearsThreeTimes -Eq $False) {
                $CountOfIdsWithALetterThatAppearsThreeTimes += 1;
                $HaveFoundALetterThatAppearsThreeTimes = $True;
            }

            $CharactersWhichHaveBeenChecked.Add($Char) > $Null;
        }
    }

    Return ($CountOfIdsWithALetterThatAppearsTwice * $CountOfIdsWithALetterThatAppearsThreeTimes);
}

$InputIds = Read-IdsFromFile($Filepath);
$Checksum = Get-CheckSumOfIds($InputIds);
Write-Host ("Advent of Code - Day 2 - Part 1: {0}" -F $Checksum);