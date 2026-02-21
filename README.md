# Longest Increasing Subsequence (LIS)

A C# .NET 8.0 solution that finds the **longest increasing subsequence** from a whitespace-separated string of integers.

## Problem

Given a string of integers separated by single whitespace, the program outputs the longest strictly increasing subsequence. If multiple subsequences share the longest length, the earliest one is returned.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- (Optional) [Docker](https://www.docker.com/) for containerisation

## Getting Started

### Build

```bash
dotnet build
```

### Run

```bash
echo "6 1 5 9 2" | dotnet run --project src/LIS/LIS.csproj
```

**Expected output:** `1 5 9`

### Run Tests

```bash
dotnet test --verbosity normal
```

### Run with Code Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Docker

### Build the Docker image

```bash
docker build -t lis-app .
```

### Run the container

```bash
echo "6 1 5 9 2" | docker run -i lis-app
```

### How the Multi-Stage Build Works

The `Dockerfile` uses a **two-stage build** to keep the final image small and secure:

| | Stage 1: Build (~700 MB) | Stage 2: Runtime (~100 MB) |
|---|---|---|
| **Base image** | `dotnet/sdk:8.0` (full SDK) | `dotnet/runtime:8.0` (runtime only) |
| **Purpose** | Compile C# source code | Run the compiled app |
| **Contains** | SDK, compiler, NuGet packages, source code | Only the compiled `LIS.dll` + runtime |
| **Kept?** | Discarded after build | This becomes the final image |

```
Stage 1 (build)                      Stage 2 (runtime)
┌──────────────────────┐             ┌──────────────────────┐
│ .NET 8.0 SDK         │             │ .NET 8.0 Runtime     │
│ C# Compiler          │             │ LIS.dll              │
│ NuGet packages       │             │ LIS.runtimeconfig    │
│ Source code          │   → copy    │                      │
│ LIS.dll (compiled)   │  compiled   │ Nothing else!        │
│ Tests                │   output    │                      │
│ Everything           │     only    │ ENTRYPOINT: run app  │
└──────────────────────┘             └──────────────────────┘
      DISCARDED                             FINAL IMAGE
```

## Project Structure

```
├── LIS.sln                          # Solution file
├── src/LIS/                         # Main application
│   ├── Program.cs                   # Entry point
│   └── SubsequenceFinder.cs         # Core LIS algorithm
├── tests/LIS.Tests/                 # Unit tests
│   ├── SubsequenceFinderTests.cs    # All test cases
│   ├── TestData.cs                  # Large test input data
│   └── TestData/                    # External test data files
├── Dockerfile                       # Multi-stage Docker build
├── .editorconfig                    # Code style / linting rules
├── .github/workflows/ci.yml        # GitHub Actions CI
└── README.md                        # This file
```

## Algorithm

The solution uses an **O(n)** single-pass scan to find the longest contiguous increasing run:

1. Parse the input string into an integer array
2. Walk through the array, tracking the current increasing run
3. If the next number is greater than the previous, extend the run
4. If not, check if the current run is the longest so far, then start a new run
5. Return the earliest longest run found

## CI/CD

GitHub Actions will automatically run the following pipeline on every push to `main`:

```
You: git push origin main
         │
         ▼
    GitHub receives code
         │
         ▼
    ┌─────────────────────────────────┐
    │   JOB 1: build-and-test         │
    │                                 │
    │   1. Clone code to fresh VM     │
    │   2. Install .NET 8.0           │
    │   3. dotnet restore             │
    │   4. dotnet build               │
    │   5. dotnet test (18 tests)     │
    │   6. Upload coverage report     │
    │                                 │
    │   Result:  PASSED               │
    └────────────┬────────────────────┘
                 │ (only if passed)
                 ▼
    ┌─────────────────────────────────┐
    │   JOB 2: docker                 │
    │                                 │
    │   1. Clone code to fresh VM     │
    │   2. Login to ghcr.io           │
    │   3. Lowercase repo name        │
    │   4. docker build               │
    │   5. docker push                │
    │                                 │
    │   Result:  Image published!     │
    └─────────────────────────────────┘
```

