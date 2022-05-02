to debug in docker compose, run docker compose, but currently identityserver can not work with docker locally so 

need debug in Selfhost,

1- run mongo via docker - docker -d -p 27017:27017 --name mongdb mongo
2- run identityserver selfhost
3- run group api locally - without docker