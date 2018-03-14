
#start test database
docker run --name parish-db \
 --mount src=dbdata,dst=/var/lib/postgresql/data/pdata \
 -p 5432:5432 \
 -e POSTGRES_USER=user1 \
 -e POSTGRES_PASSWORD=password1 \
 -e POSTGRES_DB=parish \
 -e PGDATA=/var/lib/postgresql/data/pdata/volume \
 -d postgres:10-alpine

 #start app container
docker run --name app -d -p 8081:80 \
 -e CONNECTION_STRING="Server=192.168.1.1;Port=5432;Database=parish;User Id=user1;Password=password1;" \
 -e STATE_CACHE_TTL=14400 \
 -e TRANSLATION_CACHE_TTL=14400 \
 parish-frm:latest

 