<p align="center">
  <a href="https://github.com/caiomarruda/RediSearchCore">
    <img src="https://user-images.githubusercontent.com/7254083/67167919-8ac01980-f396-11e9-9477-955f82d3000d.png">
  </a>
</p>

# RediSearchCore
[![Build Status](https://travis-ci.org/caiomarruda/RediSearchCore.svg?branch=master)](https://travis-ci.org/caiomarruda/RediSearchCore) <a href="https://github.com/caiomarruda/RediSearchCore/actions"><img alt="GitHub Actions status" src="https://github.com/caiomarruda/redisearchcore/workflows/Tests/badge.svg"></a>

RediSearchCore is a .Net Core project for easly integration with RediSearch.


## Installation

Firstly, Redis and RediSearch need to be installed.

You can download Redis from https://redis.io/download, and check out
installation instructions
[here](https://github.com/antirez/redis#installing-redis). Alternatively, on
macOS or Linux you can install via Homebrew.

To install RediSearch check out,
[https://oss.redislabs.com/redisearch/Quick_Start.html](https://oss.redislabs.com/redisearch/Quick_Start.html).
Once you have RediSearch built, if you are not using Docker, you can update your
redis.conf file to always load the RediSearch module with
`loadmodule /path/to/redisearch.so`. (On macOS the redis.conf file can be found
at `/usr/local/etc/redis.conf`)

```bash
docker run -p 6379:6379 redislabs/redisearch:latest
```

After Redis and RediSearch are up and running, you can choose your best option to run this project:

### Via Docker (recomended)
You can pull direcly from Docker Hub (https://hub.docker.com/r/caioarruda/redisearchcore)
```docker
docker run -e redisConnection="redisearch server" -p 80:80 caioarruda/redisearchcore:latest
```

## How to build
You need to install .Net Core 3.1 SDK:
```url
https://dotnet.microsoft.com/download/dotnet-core
```

And then, run this command in RediSearchCore sub-folder:
```bash
dotnet restore
dotnet publish -c Release
```

To build this project in docker, run this command in solution folder:
```docker
docker build -t caiomarruda/redisearchcore:latest .
```


## Usage

Use the ports 80 or 443(SSL) to run this project. All API documentation are available in Swagger UI.

You can also run this project using [Play With Docker](https://labs.play-with-docker.com/) for free.


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Open Source Projects in Use
StackExchange.Redis by Marc Gravell

NRediSearch by Marc Gravell

Newtonsoft.Json by James Newton-King

Swashbuckle (Swagger) by SmartBear Software

## License
[MIT](https://choosealicense.com/licenses/mit/)
