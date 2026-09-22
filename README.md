# Bank Account Program

This is a standalone console application. The `.vscode` folder only contains local editor settings and is excluded from GitHub.

## Run locally

```text
dotnet run
```

## Development builds

Build the project without running it:

```text
dotnet build
```

Development files are created in `bin/Debug/net10.0/`. This folder normally contains the compiled application, including `BankAccountProject.dll`, `BankAccountProject.exe`, and `BankAccountProject.pdb`.

The `.pdb` file contains debugging symbols. It helps Visual Studio Code set breakpoints, step through the source code, and show useful source file and line numbers in error messages. It is useful during development but is not required to run the program.

`dotnet run` builds the project when necessary and then runs the development version. Use `dotnet build` when you only want to check that the code compiles.

## Publish an independent Windows program

Run this command from the project folder:

```text
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish
```

The independent executable will be created in `publish/BankAccountProject.exe`. Copy the complete `publish` folder to another Windows computer and run the executable. No .NET installation is required.

Session files are saved in a `sessions` folder beside the executable.

## Build output and GitHub

The `bin/`, `obj/`, `publish/`, and `.vscode/` folders are excluded by `.gitignore`:

- `bin/` contains local development and release build output.
- `obj/` contains temporary files used while compiling.
- `publish/` contains generated files for sharing the application.
- `.vscode/` contains local editor settings.

Commit the source files, the `.csproj` file, `.gitignore`, and this README. Do not commit generated `.exe`, `.dll`, or `.pdb` files. Anyone with the .NET SDK can recreate the build or publish output from the source code.
