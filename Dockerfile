FROM ubuntu:18.04

RUN apt-get update
RUN apt-get install --yes software-properties-common
RUN add-apt-repository universe
RUN apt-get update
RUN apt-get install --yes \
    apt-transport-https \
    wget
RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb \
    -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install --yes \
    aspnetcore-runtime-3.1 \
    dotnet-runtime-3.1 \
    dotnet-sdk-3.1

RUN mkdir /app

WORKDIR /app
RUN true
