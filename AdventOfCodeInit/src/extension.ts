import * as cp from "child_process";
import * as fs from 'node:fs';
import * as vscode from 'vscode';
import { window } from 'vscode';

export function activate(context: vscode.ExtensionContext) {
	let disposable = vscode.commands.registerCommand('adventofcodeinit.AocDotnetInit', async () => {
		const currentDay = new Date().getDate();
		
		let formattedDay = currentDay.toString();
		if (currentDay < 10)
		{
			formattedDay = "0" + formattedDay;
		}

		let guaranteedPath = "";
		const projectPath = await window.showInputBox({
			placeHolder: 'Enter project absolute base path e.g. "C:/Repos"'
		});

		if (projectPath == null || projectPath === '')
		{
			vscode.window.showErrorMessage('Project path is required');
			return;
		}
		else
		{
			guaranteedPath = projectPath.toString();
		}

		if (!fs.existsSync(guaranteedPath))
		{
			fs.mkdirSync(guaranteedPath, { recursive: true });
		}

		await execShell(`dotnet new console -o "${guaranteedPath}/Day-${formattedDay}-Part-01"`);
		await execShell(`dotnet new console -o "${guaranteedPath}/Day-${formattedDay}-Part-02"`);

		fs.writeFileSync(`${guaranteedPath}/Day-${formattedDay}-Part-01/input.txt`, "");
		fs.writeFileSync(`${guaranteedPath}/Day-${formattedDay}-Part-02/input.txt`, "");

		fs.writeFileSync(`${guaranteedPath}/Day-${formattedDay}-Part-01/Program.cs`, getFormattedDotnetProgram(currentDay.toString(), "1"));
		fs.writeFileSync(`${guaranteedPath}/Day-${formattedDay}-Part-02/Program.cs`, getFormattedDotnetProgram(currentDay.toString(), "2"));
	
		vscode.window.showInformationMessage(`Created dotnet projects "Day-${formattedDay} parts 1 and 2 in ${guaranteedPath}`);
	
		vscode.env.openExternal(vscode.Uri.parse(`${guaranteedPath}`));
	});

	context.subscriptions.push(disposable);
}

const execShell = (cmd: string) =>
    new Promise<string>((resolve, reject) => {
        cp.exec(cmd, (err: any, out: string) => err ? reject(err) : resolve(out));
    });

const getFormattedDotnetProgram = (day: string, part: string) => 
	`var input = File.ReadAllLines("./input.txt");

Console.WriteLine("Day ${day} - Part ${part}");`;


export function deactivate() {}
