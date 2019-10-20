<p align="center">
  <a href="https://github.com/caiomarruda/RediSearchCore">
    <img src="https://user-images.githubusercontent.com/7254083/67168079-47ff4100-f398-11e9-94b8-1ed042c90374.png">
  </a>
</p>

# RediSearchCore [![Build Status](https://travis-ci.org/caiomarruda/RediSearchCore.svg?branch=master)](https://travis-ci.org/caiomarruda/RediSearchCore)

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

After Redis and RediSearch are up and running, you need to install .Net Core 2.2 or more to run this project.

## Usage

To run this project in docker, run this command:
```docker
dotnet clean
dotnet publish -c Release
docker build -t caiomarruda/redisearchcore:latest .
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
