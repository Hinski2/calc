# Simple C# Calculator

A lightweight mathematical expression evaluator built from scratch in C#. It uses a custom Lexer, a Recursive Descent Parser, and an Interpreter to process math equations.

## Features
- **Lexer**: Tokenizes strings into meaningful math symbols.
- **Parser**: Builds an Abstract Syntax Tree (AST) respecting operator precedence.
- **Interpreter**: Evaluates the AST to produce a final result.
- **File Support**: Can read expressions from a file or run in interactive mode.

## How to Run

#### 1. Interactive Mode
Run the program and type your expressions directly into the console:
```bash
dotnet run
```
#### 2. File Mode

```back
dotnet run test.in
```

## Expample 
```
> 2 + 2 * (3 - 1)
6
> 10 / 2 + 5
10
```