docker stop locallangui
docker build -t fedark/locallangweb -f Dockerfile .
docker run -td --rm --name locallangui -p 7666:7666 fedark/locallangweb