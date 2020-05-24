#!/bin/bash

if [[ "${RUNSERVER}" == "True" ]] ; then
    if [[ "${RUNTEST}" == "True" ]] ; then
        dotnet test
    fi
    dotnet publish -c "Release" "Mashup.Api"
    dotnet "Mashup.Api/bin/Release/netcoreapp3.1/publish/Mashup.Api.dll"
    echo "app died, will now wait forever"
else
    echo "app not started, will now wait forever"
fi

sleep infinity
