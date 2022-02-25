from mcr.microsoft.com/dotnet/sdk:6.0 as build

workdir /app

copy *.sln ./
copy ShopAPI/*.csproj ShopAPI/
copy ShopBL/*.csproj ShopBL/
copy ShopDL/*.csproj ShopDL/
copy ShopModel/*.csproj ShopModel/
copy ShopTest/*.csproj ShopTest/

copy . ./

run dotnet publish -c Release -o publish

from mcr.microsoft.com/dotnet/sdk:6.0 as runtime

workdir /app
copy --from=build app/publish ./

cmd ["dotnet", "ShopAPI.dll"]

expose 80