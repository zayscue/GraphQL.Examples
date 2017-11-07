echo off
title Code Coverage Report Generation Script
:: See the ttile at the top.
echo Starting...
mkdir .\coverage\unit
OpenCover.Console.exe -target:"dotnet.exe" -targetargs:"test -f netcoreapp2.0 -c Debug ./StarWars.Tests.Unit.csproj" -hideskipped:File -output:./coverage/unit/coverage.xml -oldStyle -filter:"+[StarWars*]* -[StarWars.Tests*]* -[StarWars.Api]*Program -[StarWars.Api]*Startup -[StarWars.Data]*EntityFramework.Workaround.Program -[StarWars.Data]*EntityFramework.Migrations* -[StarWars.Data]*EntityFramework.Seed*" -searchdirs:"./bin/Debug/netcoreapp2.0" -register:user
ReportGenerator.exe -reports:./coverage/unit/coverage.xml -targetdir:coverage/unit -verbosity:Error
start .\coverage\unit\index.htm
echo Finished...