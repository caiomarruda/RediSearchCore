Invoke-Expression -Command (aws ecr get-login --no-include-email --region eu-west-2)
dotnet clean
dotnet publish -c Release
docker rmi -f 065343645040.dkr.ecr.eu-west-2.amazonaws.com/airports:latest
docker build -t 065343645040.dkr.ecr.eu-west-2.amazonaws.com/airports:latest .
docker push 065343645040.dkr.ecr.eu-west-2.amazonaws.com/airports:latest

