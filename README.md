# FaceDetectionViaImmich

A simple C# HTTP client for interacting with the Immich face recognition Docker container via its internal Web API.

## Overview

This client allows .NET applications to communicate with the machine learning service used by [Immich](https://github.com/immich-app/immich) for face detection and comparison.  
It sends image data to the container and retrieves face embeddings and similarity results via HTTP.

## Features

- Detect faces in images
- Extract face embeddings
- Compare faces for similarity
- Lightweight and easy to integrate

## Start Docker Container immich Machine-learning

```
docker run -p 3003:3003 ghcr.io/immich-app/immich-machine-learning:release
```

## Sources
- https://github.com/immich-app
- https://github.com/deepinsight/insightface/tree/master/model_zoo
