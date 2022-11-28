# Advent of Code init
Simple VS Code extension to create the base structure of an Advent of Code project for each day using the current date.

Currently supporting `dotnet` projects.  

## Packaging and Installation

To create a new packaged extension run `vsce package`.

To install the packaged extension run `code --install-extension .\adventofcodeinit-x.y.z.vsix` where x.y.z is the extension version.

## Usage

1. <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>P</kbd>
2. Select "AoC dotnet init"
3. Enter the base file path. This is where the two sub folders Day-x-Part-y will be created
