docker stop langweb
docker build -t fedark/locallangweb -f Dockerfile .
docker run -td --rm --name langweb -p 7666:7666 fedark/locallangweb