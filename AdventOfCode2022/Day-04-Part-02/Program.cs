var assignmentsContainingAnOverlap = File.ReadAllLines("./input.txt")
    .Select(ParseSectionAssignment)
    .Where(SectionAssignmentContainsAnOverlap)
    .ToArray();

Console.WriteLine($"Day 4 - Part 2: {assignmentsContainingAnOverlap.Count()}");

(SectionAssignment FirstElf, SectionAssignment SecondElf) ParseSectionAssignment(string sectionAssignment)
{
    SectionAssignment GetAssignmentFromInput(string input)
    {
        var sectionParts = input.Split('-');

        var partOne = Convert.ToInt32(sectionParts[0]);
        var partTwo = Convert.ToInt32(sectionParts[1]);

        return new SectionAssignment(partOne, partTwo);
    }

    var assignmentParts = sectionAssignment.Split(',');

    return (GetAssignmentFromInput(assignmentParts[0]), GetAssignmentFromInput(assignmentParts[1]));
}

bool SectionAssignmentContainsAnOverlap((SectionAssignment FirstElf, SectionAssignment SecondElf) sectionAssignments)
{
    bool SectionCompletelyContainsOtherSection(SectionAssignment sectionOne, SectionAssignment sectionTwo) =>
        (sectionOne.LowerBound <= sectionTwo.LowerBound && sectionOne.UpperBound >= sectionTwo.LowerBound) ||
        (sectionOne.LowerBound <= sectionTwo.UpperBound && sectionOne.UpperBound >= sectionTwo.UpperBound);

    return SectionCompletelyContainsOtherSection(sectionAssignments.FirstElf, sectionAssignments.SecondElf) ||
        SectionCompletelyContainsOtherSection(sectionAssignments.SecondElf, sectionAssignments.FirstElf);
}

record SectionAssignment(int LowerBound, int UpperBound);
