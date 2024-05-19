## System.ComandLine :: cli

- [add reporting](#add-reporting)
  - [add nuget package](#add-nuget-package)
  - [add report generating tool](#add-report-generating-tool)
  - [collect report data](#collect-report-data)
  - [generate final report](#generate-final-report)

### add reporting

#### add nuget package

```bash
dotnet add package coverlet.collector
```

#### add report generating tool

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

#### collect report data

```bash
dotnet test --collect:"XPlat code coverage"
```

#### generate final report

```bash
reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:result.coverage
```
