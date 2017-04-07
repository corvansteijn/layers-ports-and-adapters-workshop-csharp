#!/bin/bash
cd /app/src/Infrastructure/ScheduleMeetupConsole
dotnet restore
dotnet run $@