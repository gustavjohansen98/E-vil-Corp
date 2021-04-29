$serviceName = "api"

# parse version number for deployment
$versionPath = "appsettings.json"
$json = Get-Content -Raw -Path $versionPath | ConvertFrom-Json
$version = $json.version.Split("-")[0]

# tag docker image with the updated application version
$imageName = "evilapi:$version"

# build image
if (docker images -q $imageName) {
    "Image $imageName exists"
    return 
} else {
    docker build -t $imageName -f "TokenGen.dockerfile" .
}

# create service 
# docker service create --publish 5010:5010 --name $serviceName --replicas 3 --update-delay 10s $imageName

# update service in swarm 
docker service update --image $imageName $serviceName

# remove unused containers, images and such to free up space 
docker system prune -a --force

